using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using Application.IServices;
using Domain.Entities;
using Domain.IRepositories;
using Application.CommandsConverters;

namespace Application.Services
{
    public class AdvertService : IAdvertService
    {
        private IAdvertRepository _advertRepository;
        private IAccountRepository _accountRepository;
        private IRequestRepository _requestRepository;

        public AdvertService(IAdvertRepository advertRepository, IAccountRepository accountRepository, IRequestRepository requestRepository)
        {
            _advertRepository = advertRepository;
            _accountRepository = accountRepository;
            _requestRepository = requestRepository;
        }

        public async Task<bool> Create(AdvertCommandCreate advert)
        {
            try
            {
                if (advert == null)
                    return false;
                if (advert.UserId <= 0)
                    return false;
                if (advert.Name == null || advert.Name.Trim() == "")
                    return false;
                if (advert.PTS == null || advert.PTS.Trim() == "")
                    return false;
                if (advert.VIN == null || advert.VIN.Trim() == "")
                    return false;
                if (advert.Price <= 0)
                    return false;
                if (advert.StartDate > advert.EndDate)
                    return false;
                if (advert.StartTime > advert.EndTime)
                    return false;
                advert.PublishDate = DateTime.Now;
                advert.EditDate = advert.PublishDate;
                Advert advertEntity = AdvertCommandConverter.AdvertCommandCreateConvertToAdvertEntity(advert);
                List<AvailableTime> availableTimes = FillAvailableTime(advert.StartDate, advert.EndDate, advert.StartTime, advert.EndTime);
                availableTimes.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
                advertEntity.AvailableTimes = availableTimes;
                await _advertRepository.Create(advertEntity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(AdvertCommandUpdate advertCommand)
        {
            try
            {
                if (advertCommand == null)
                    return false;
                if (advertCommand.Id <= 0)
                    return false;
                Advert currentAdvert = await _advertRepository.GetById(advertCommand.Id);
                if (currentAdvert == null)
                    return false;
                if (advertCommand.UserId <= 0)
                    return false;
                if (advertCommand.Name == null || advertCommand.Name.Trim() == "")
                    return false;
                if (advertCommand.PTS == null || advertCommand.PTS.Trim() == "")
                    return false;
                if (advertCommand.VIN == null || advertCommand.VIN.Trim() == "")
                    return false;
                if (advertCommand.Price <= 0)
                    return false;
                if (advertCommand.StartDate > advertCommand.EndDate)
                    return false;
                if (advertCommand.StartTime > advertCommand.EndTime)
                    return false;
                advertCommand.EditDate = DateTime.Now;
                List<AvailableTime> times = await _requestRepository.GetTimesForRemoveRequestByAdvertId(advertCommand.Id);
                Advert advert = AdvertCommandConverter.AdvertCommandUpdateConvertToAdvertEntity(advertCommand);
                List<AvailableTime> newAvailableTime = FillAvailableTime(advertCommand.StartDate, advertCommand.EndDate, advertCommand.StartTime, advertCommand.EndTime);
                newAvailableTime = await RemoveOldRequests(newAvailableTime, times);
                advert.AvailableTimes = newAvailableTime;
                await _advertRepository.Update(advert);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<AvailiableDayCommand> PackageToDayCommands(List<AvailableTimeCommand> availableTimeCommands)
        {
            List<AvailiableDayCommand> availiableDayCommands = new List<AvailiableDayCommand>();
            if (availableTimeCommands.Count == 0)
                return availiableDayCommands;
            AvailiableDayCommand bufferCommand = new AvailiableDayCommand();
            for (int count_j = 0; count_j < availableTimeCommands.Count; count_j++)
            {
                if (availableTimeCommands[count_j] != null)
                {
                    bufferCommand = new AvailiableDayCommand
                    {
                        Date = availableTimeCommands[count_j].Date,
                        Times = new List<AvailableTimeCommand>()
                    };
                    for (int count_i = 0; count_i < availableTimeCommands.Count; count_i++)
                    {
                        if (availableTimeCommands[count_i] != null
                            && bufferCommand.Date.Date == availableTimeCommands[count_i].Date.Date)
                        {
                            bufferCommand.Times.Add(availableTimeCommands[count_i]);
                            availableTimeCommands[count_i] = null;
                        }
                    }
                    availiableDayCommands.Add(bufferCommand);
                    bufferCommand = new AvailiableDayCommand();
                }
            }
            return availiableDayCommands;
        }

        public async Task<List<AvailableTime>> RemoveOldRequests(List<AvailableTime> newAvilableTime, List<AvailableTime> oldAvailableTime)
        {
            for (int count_new_time = 0; count_new_time < newAvilableTime.Count; count_new_time++)
            {
                for (int count_old_time = 0; count_old_time < oldAvailableTime.Count; count_old_time++)
                {
                    if (oldAvailableTime[count_old_time] != null)
                    {
                        if (newAvilableTime[count_new_time].Date == oldAvailableTime[count_old_time].Date)
                        {
                            newAvilableTime[count_new_time] = oldAvailableTime[count_old_time];
                            oldAvailableTime[count_old_time] = null;
                        }
                    }
                }
            }
            foreach (AvailableTime time in oldAvailableTime)
            {
                if (time != null)
                {
                    await _requestRepository.Remove((int)time.AvailabilityRequestId);
                }
            }
            return newAvilableTime;
        }

        public async Task<List<AdvertCommandList>> GetAll(FilterCommand filterCommand, string name, string sort, int page)
        {
            try
            {
                if (page < 0)
                    return null;
                if (sort == null || sort.Trim() == "")
                    return null;
                Filter filter = AdvertCommandConverter.FilterCommandConvertToEntity(filterCommand);
                List<Advert> adverts = new List<Advert>();
                switch (sort)
                {
                    case "all":
                        adverts = await _advertRepository.GetAll(filter, name, page);
                        break;
                    case "max_price":
                        adverts = await _advertRepository.GetSortByPriceMax(filter, name, page);
                        break;
                    case "min_price":
                        adverts = await _advertRepository.GetSortByPriceMin(filter, name, page);
                        break;
                    case "max_rating":
                        adverts = await _advertRepository.GetSortByRatingMax(filter, name, page);
                        break;
                    case "min_rating":
                        adverts = await _advertRepository.GetSortByRatingMin(filter, name, page);
                        break;
                    case "max_date":
                        adverts = await _advertRepository.GetAll(filter, name, page);
                        break;
                    case "min_date":
                        adverts = await _advertRepository.GetSortByDateMin(filter, name, page);
                        break;
                    default:
                        adverts = await _advertRepository.GetAll(filter, name, page);
                        break;
                }
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetAllWithoutUserId(FilterCommand filterCommand, string name, string sort, int id, int page)
        {
            try
            {
                if (id <= 0 || page < 0)
                    return null;
                User currentUser = await _accountRepository.GetById(id);
                if (currentUser == null)
                    return null;
                if (currentUser.Id != id)
                    return null;
                if (sort == null || sort.Trim() == "")
                    return null;
                Filter filter = AdvertCommandConverter.FilterCommandConvertToEntity(filterCommand);
                List<Advert> adverts = new List<Advert>();
                switch (sort)
                {
                    case "all":
                        adverts = await _advertRepository.GetAllWithoutUserId(filter, name, id, page);
                        break;
                    case "max_price":
                        adverts = await _advertRepository.GetSortByPriceMaxWithoutUserId(filter, name, page, id);
                        break;
                    case "min_price":
                        adverts = await _advertRepository.GetSortByPriceMinWithoutUserId(filter, name, page, id);
                        break;
                    case "max_rating":
                        adverts = await _advertRepository.GetSortByRatingMaxWithoutUserId(filter, name, page, id);
                        break;
                    case "min_rating":
                        adverts = await _advertRepository.GetSortByRatingMinWithoutUserId(filter, name, page, id);
                        break;
                    case "max_date":
                        adverts = await _advertRepository.GetAllWithoutUserId(filter, name, id, page);
                        break;
                    case "min_date":
                        adverts = await _advertRepository.GetSortByDateMinWithoutUserId(filter, name, page, id);
                        break;
                    default:
                        adverts = await _advertRepository.GetAllWithoutUserId(filter, name, id, page);
                        break;
                }
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<AdvertCommandInfo> GetById(int id)
        {
            try
            {
                if (id <= 0)
                    return null;
                Advert advert = await _advertRepository.GetById(id);
                if (advert == null)
                    return null;
                if (advert.Id != id)
                    return null;
                User user = await _accountRepository.GetById(advert.UserId);
                if (user == null)
                    return null;
                if (user.Id != advert.UserId)
                    return null;
                AdvertCommandInfo advertCommandInfo = AdvertCommandConverter.AdvertEntityConvertToAdvertCommandInfo(advert, user.Name);
                return advertCommandInfo;
            }
            catch
            {
                return null;
            }
        }

        public async Task<AdvertCommandDetail> GetDetailAdvert(int id)
        {
            try
            {
                if (id <= 0)
                    return null;
                Advert advert = await _advertRepository.GetById(id);
                if (advert == null)
                    return null;
                if (advert.Id != id)
                    return null;
                User user = await _accountRepository.GetById(advert.UserId);
                if (user == null)
                    return null;
                if (user.Id != advert.UserId)
                    return null;
                List<AvailiableDayCommand> availiableDayCommands = PackageToDayCommands(
                    AdvertCommandConverter.AvailableTimeEntityConvertToCommand(advert.AvailableTimes));
                availiableDayCommands = SortDateCommmands(availiableDayCommands);
                AdvertCommandDetail advertCommandDetail = AdvertCommandConverter.AdvertEntityConvertToAdvertCommandDetail(
                    advert, user.Name, availiableDayCommands);
                return advertCommandDetail;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetByName(string name, int page)
        {
            try
            {
                if (name == null || name.Trim() == "" || page < 0)
                    return null;
                List<Advert> adverts = await _advertRepository.GetByName(name, page);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetByNameWithoutUserId(string name, int id, int count)
        {
            try
            {
                if (name == null || name.Trim() == "" || count < 0 || id <= 0)
                    return null;
                List<Advert> adverts = await _advertRepository.GetByNameWithoutUserId(name, id, count);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetByUserId(FilterCommand filterCommand, string name, string sort, int id, int page)
        {
            try
            {
                if (id <= 0 || page < 0)
                    return null;
                if (sort == null || sort.Trim() == "")
                    return null;
                Filter filter = AdvertCommandConverter.FilterCommandConvertToEntity(filterCommand);
                List<Advert> adverts = new List<Advert>();
                adverts = await _advertRepository.GetByUserId(filter, name, id, page);
                switch (sort)
                {
                    case "all":
                        adverts = await _advertRepository.GetByUserId(filter, name, id, page);
                        break;
                    case "max_price":
                        adverts = await _advertRepository.GetSortByPriceMaxByUserId(filter, name, id, page);
                        break;
                    case "min_price":
                        adverts = await _advertRepository.GetSortByPriceMinByUserId(filter, name, id, page);
                        break;
                    case "max_rating":
                        adverts = await _advertRepository.GetSortByRatingMaxByUserId(filter, name, id, page);
                        break;
                    case "min_rating":
                        adverts = await _advertRepository.GetSortByRatingMinByUserId(filter, name, id, page);
                        break;
                    case "max_date":
                        adverts = await _advertRepository.GetByUserId(filter, name, id, page);
                        break;
                    case "min_date":
                        adverts = await _advertRepository.GetSortByDateMinByUserId(filter, name, id, page);
                        break;
                    default:
                        adverts = await _advertRepository.GetByUserId(filter, name, id, page);
                        break;
                }
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandForRequest>> GetForRequestCustomer(int id, int page)
        {
            try
            {
                if (id <= 0 || page < 0)
                    return null;
                List<Advert> adverts = await _advertRepository.GetUserAdvertsWithPendingConfirmationForCustomer(id, page);
                List<AdvertCommandForRequest> commands = adverts.Select(advert => AdvertCommandConverter.AdvertConvertToAdvertCommandForRequest(advert)).ToList();
                return commands;
            }
            catch
            {
                return null;
            }
        }

        public async Task<AdvertCommandUpdate> GetForUpdate(int id, int userId)
        {
            try
            {
                if (id <= 0 || userId <= 0)
                    return null;
                Advert advert = await _advertRepository.GetForUpdate(id);
                if (advert == null)
                    return null;
                if (advert.Id != id)
                    return null;
                if (userId != advert.UserId)
                    return null;
                AdvertCommandUpdate advertCommand = AdvertCommandConverter.AdvertEntityConvertToAdvertCommandUpdate(advert);
                if (advert.AvailableTimes.Count > 0)
                    advertCommand = FillRangeTime(advertCommand, advert);
                return advertCommand;
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> GetLastAdvertId()
        {
            try
            {
                return await _advertRepository.GetLastAdvertId();
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> GetUserIdByAdvert(int id)
        {
            try
            {
                if (id <= 0)
                    return 0;
                Advert advert = await _advertRepository.GetById(id);
                if (advert == null)
                    return 0;
                return advert.UserId;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                if (id <= 0)
                    return false;
                List<AvailableTime> times = await _requestRepository.GetTimesForRemoveRequestByAdvertId(id);
                if (times.Count != 0)
                {
                    foreach (AvailableTime time in times)
                    {
                        await _requestRepository.Remove((int)time.AvailabilityRequestId);
                    }
                }
                await _advertRepository.Remove(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMax(int page)
        {
            try
            {
                if (page < 0)
                    return null;
                List<Advert> adverts = new List<Advert>();
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMin(int page)
        {
            try
            {
                if (page < 0)
                    return null;
                List<Advert> adverts = new List<Advert>();
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMaxWithoutUserId(int page, int id)
        {
            try
            {
                if (page < 0 || id <= 0)
                    return null;
                List<Advert> adverts = new List<Advert>();
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMinWithoutUserId(int page, int id)
        {
            try
            {
                if (page < 0 || id <= 0)
                    return null;
                List<Advert> adverts = new List<Advert>();
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMax(int page)
        {
            try
            {
                if (page < 0)
                    return null;
                List<Advert> adverts = new List<Advert>();
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMin(int page)
        {
            try
            {
                if (page < 0)
                    return null;
                List<Advert> adverts = new List<Advert>();
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMaxWithoutUserId(int page, int id)
        {
            try
            {
                if (page < 0 || id <= 0)
                    return null;
                List<Advert> adverts = new List<Advert>();
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMinWithoutUserId(int page, int id)
        {
            try
            {
                if (page < 0 || id <= 0)
                    return null;
                List<Advert> adverts = new List<Advert>();
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByDateMin(int page)
        {
            try
            {
                if (page < 0)
                    return null;
                List<Advert> adverts = new List<Advert>();
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByDateMinWithoutUserId(int page, int id)
        {
            try
            {
                if (page < 0 || id <= 0)
                    return null;
                List<Advert> adverts = new List<Advert>();
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMaxByName(int page, string name)
        {
            try
            {
                if (page < 0 || name == null || name.Trim() == "")
                    return null;
                List<Advert> adverts = await _advertRepository.GetSortByPriceMaxByName(page, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMinByName(int page, string name)
        {
            try
            {
                if (page < 0 || name == null || name.Trim() == "")
                    return null;
                List<Advert> adverts = await _advertRepository.GetSortByPriceMinByName(page, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMaxWithoutUserIdByName(int page, int id, string name)
        {
            try
            {
                if (page < 0 || id <= 0 || name == null || name.Trim() == "")
                    return null;
                List<Advert> adverts = await _advertRepository.GetSortByPriceMaxWithoutUserIdByName(page, id, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMinWithoutUserIdByName(int page, int id, string name)
        {
            try
            {
                if (page < 0 || id <= 0 || name == null || name.Trim() == "")
                    return null;
                List<Advert> adverts = await _advertRepository.GetSortByPriceMinWithoutUserIdByName(page, id, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMaxByName(int page, string name)
        {
            try
            {
                if (page < 0 || name == null || name.Trim() == "")
                    return null;
                List<Advert> adverts = await _advertRepository.GetSortByRatingMaxByName(page, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMinByName(int page, string name)
        {
            try
            {
                if (page < 0 || name == null || name.Trim() == "")
                    return null;
                List<Advert> adverts = await _advertRepository.GetSortByRatingMinByName(page, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMaxWithoutUserIdByName(int page, int id, string name)
        {
            try
            {
                if (page < 0 || id <= 0 || name == null || name.Trim() == "")
                    return null;
                List<Advert> adverts = await _advertRepository.GetSortByRatingMaxWithoutUserIdByName(page, id, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMinWithoutUserIdByName(int page, int id, string name)
        {
            try
            {
                if (page < 0 || id <= 0 || name == null || name.Trim() == "")
                    return null;
                List<Advert> adverts = await _advertRepository.GetSortByRatingMinWithoutUserIdByName(page, id, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByDateMinByName(int page, string name)
        {
            try
            {
                if (page < 0 || name == null || name.Trim() == "")
                    return null;
                List<Advert> adverts = await _advertRepository.GetSortByDateMinByName(page, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByDateMinWithoutUserIdByName(int page, int id, string name)
        {
            try
            {
                if (page < 0 || id <= 0 || name == null || name.Trim() == "")
                    return null;
                List<Advert> adverts = await _advertRepository.GetSortByDateMinWithoutUserIdByName(page, id, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMaxByUserId(int id, int page)
        {
            try
            {
                if (page < 0 || id <= 0)
                    return null;
                List<Advert> adverts = new List<Advert>();
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMinByUserId(int id, int page)
        {
            try
            {
                if (page < 0 || id <= 0)
                    return null;
                List<Advert> adverts = new List<Advert>();
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMaxByUserId(int id, int page)
        {
            try
            {
                if (page < 0 || id <= 0)
                    return null;
                List<Advert> adverts = new List<Advert>();
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMinByUserId(int id, int page)
        {
            try
            {
                if (page < 0 || id <= 0)
                    return null;
                List<Advert> adverts = new List<Advert>();
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByDateMinByUserId(int id, int page)
        {
            try
            {
                if (page < 0 || id <= 0)
                    return null;
                List<Advert> adverts = new List<Advert>();
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public List<AvailiableDayCommand> SortDateCommmands(List<AvailiableDayCommand> availiableDayCommands)
        {
            if (availiableDayCommands.Count == 0)
                return availiableDayCommands;
            AvailiableDayCommand bufferCommand = new AvailiableDayCommand();
            int index = 0;
            while (index < availiableDayCommands.Count)
            {
                for (int count = 0; count < availiableDayCommands.Count - index - 1; count++)
                {
                    if (availiableDayCommands[count].Date.Date > availiableDayCommands[count + 1].Date.Date)
                    {
                        bufferCommand = availiableDayCommands[count];
                        availiableDayCommands[count] = availiableDayCommands[count + 1];
                        availiableDayCommands[count + 1] = bufferCommand;
                    }
                }
                index++;
            }
            index = 0;
            while (index < availiableDayCommands.Count)
            {
                availiableDayCommands[index].Times = SortTimeCommands(availiableDayCommands[index].Times);
                index++;
            }
            return availiableDayCommands;
        }

        private List<AvailableTimeCommand> SortTimeCommands(List<AvailableTimeCommand> availableTimeCommands)
        {
            AvailableTimeCommand bufferCommand = new AvailableTimeCommand();
            int index = 0;
            while (index < availableTimeCommands.Count)
            {
                for (int count = 0; count < availableTimeCommands.Count - index - 1; count++)
                {
                    if (availableTimeCommands[count].Date.Hour > availableTimeCommands[count + 1].Date.Hour)
                    {
                        bufferCommand = availableTimeCommands[count];
                        availableTimeCommands[count] = availableTimeCommands[count + 1];
                        availableTimeCommands[count + 1] = bufferCommand;
                    }
                }
                index++;
            }
            return availableTimeCommands;
        }

        private List<AvailableTime> FillAvailableTime(DateTime startDate, DateTime endDate, int startTime, int endTime)
        {
            startDate = startDate.ToLocalTime();
            endDate = endDate.ToLocalTime();
            List<DateTime> dates = new List<DateTime>();
            DateTime date = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            while (date <= endDate)
            {
                dates.Add(date);
                date = date.AddDays(1);
            }
            List<AvailableTime> availableTimes = new List<AvailableTime>();
            int currentHour = startTime;
            for (int count = 0; count < dates.Count; count++)
            {
                while (currentHour <= endTime)
                {
                    dates[count] = dates[count].AddHours(currentHour);
                    availableTimes.Add(new AvailableTime
                    {
                        Date = dates[count],
                        AvailabilityStateId = 1
                    });
                    dates[count] = dates[count].AddHours(-currentHour);
                    currentHour++;
                }
                currentHour = startTime;
            }
            return availableTimes;
        }

        private AdvertCommandUpdate FillRangeTime(AdvertCommandUpdate command, Advert advert)
        {
            advert.AvailableTimes.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            command.StartDate = advert.AvailableTimes[0].Date;
            command.EndDate = advert.AvailableTimes[advert.AvailableTimes.Count - 1].Date;
            string start = advert.AvailableTimes[0].Date.ToString("HH");
            string end = advert.AvailableTimes[advert.AvailableTimes.Count - 1].Date.ToString("HH");
            command.StartTime = Int32.Parse(start);
            command.EndTime = Int32.Parse(end);
            return command;
        }

        private double GetAverageRating(List<Review> reviews)
        {
            int rating = 0;
            double result = 0;
            if (reviews == null)
                return result;
            if (reviews.Count == 0)
                return result;
            foreach (Review review in reviews)
            {
                rating = rating + review.ReviewStateId;
            }
            if (rating > 0)
            {
                result = rating / reviews.Count;
                result = Math.Round(result, 2);
            }
            return result;
        }
    }
}
