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
                if(advert != null)
                {
                    advert.PublishDate = DateTime.Now;
                    advert.EditDate = advert.PublishDate;
                    Advert advertEntity = AdvertCommandConverter.AdvertCommandCreateConvertToAdvertEntity(advert);
                    List<AvailableTime> availableTimes = FillAvailableTime(advert.StartDate, advert.EndDate, advert.StartTime, advert.EndTime);
                    availableTimes.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
                    advertEntity.AvailableTimes = availableTimes;
                    await _advertRepository.Create(advertEntity);
                    return true;
                }
                return false;
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
                if (advertCommand != null)
                {
                    advertCommand.EditDate = DateTime.Now;
                    List<AvailableTime> times = await _requestRepository.GetTimesForRemoveRequestByAdvertId(advertCommand.Id);
                    Advert advert = AdvertCommandConverter.AdvertCommandUpdateConvertToAdvertEntity(advertCommand);
                    List<AvailableTime> newAvailableTime = FillAvailableTime(advertCommand.StartDate, advertCommand.EndDate, advertCommand.StartTime, advertCommand.EndTime);
                    newAvailableTime = await RemoveOldRequests(newAvailableTime, times);
                    advert.AvailableTimes = newAvailableTime;
                    await _advertRepository.Update(advert);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private List<AvailableTime> FillAvailableTime(DateTime startDate, DateTime endDate, int startTime, int endTime)
        {
            startDate = startDate.ToLocalTime();
            endDate = endDate.ToLocalTime();
            List<DateTime> dates = new List<DateTime>();
            DateTime date = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            while(date <= endDate)
            {
                dates.Add(date);
                date = date.AddDays(1);
            }
            List<AvailableTime> availableTimes = new List<AvailableTime>();
            int currentHour = startTime;
            for(int count = 0; count < dates.Count; count++)
            {
                while(currentHour <= endTime)
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

        public async Task<List<AvailableTime>> RemoveOldRequests(List<AvailableTime> newAvilableTime, List<AvailableTime> oldAvailableTime)
        {
            for(int count_new_time = 0; count_new_time < newAvilableTime.Count; count_new_time++)
            {
                for(int count_old_time = 0; count_old_time < oldAvailableTime.Count; count_old_time++)
                {
                    if(oldAvailableTime[count_old_time] != null)
                    {
                        if (newAvilableTime[count_new_time].Date == oldAvailableTime[count_old_time].Date)
                        {
                            newAvilableTime[count_new_time] = oldAvailableTime[count_old_time];
                            oldAvailableTime[count_old_time] = null;
                        }
                    }
                }
            }
            foreach(AvailableTime time in oldAvailableTime)
            {
                if(time != null)
                {
                    await _requestRepository.Remove((int)time.AvailabilityRequestId);
                }
            }
            return newAvilableTime;
        }

        private double GetAverageRating(List<Review> reviews)
        {
            int rating = 0;
            double result = 0;
            if(reviews == null)
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
            
        public async Task<List<AdvertCommandList>> GetAll(int count)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetAll(count);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetAllWithoutUserId(int id, int count)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetAllWithoutUserId(id, count);
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
                Advert advert = await _advertRepository.GetById(id);
                User user = await _accountRepository.GetById(advert.UserId);
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
                Advert advert = await _advertRepository.GetById(id);
                User user = await _accountRepository.GetById(advert.UserId);
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

        public async Task<List<AdvertCommandList>> GetByName(string name, int count)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetByName(name, count);
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
                List<Advert> adverts = await _advertRepository.GetByNameWithoutUserId(name, id, count);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetByUserId(int id, int count)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetByUserId(id, count);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandForRequest>> GetForRequestCustomer(int id, int count)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetUserAdvertsWithPendingConfirmationForCustomer(id, count);
                List<AdvertCommandForRequest> commands = adverts.Select(advert => AdvertCommandConverter.AdvertConvertToAdvertCommandForRequest(advert)).ToList();
                return commands;
            }
            catch
            {
                return null;
            }
        }

        public async Task<AdvertCommandUpdate> GetForUpdate(int id)
        {
            try
            {
                Advert advert = await _advertRepository.GetForUpdate(id);
                AdvertCommandUpdate advertCommand = AdvertCommandConverter.AdvertEntityConvertToAdvertCommandUpdate(advert);
                if(advert.AvailableTimes.Count != 0)
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
                Advert advert = await _advertRepository.GetById(id);
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
                List<AvailableTime> times = await _requestRepository.GetTimesForRemoveRequestByAdvertId(id);
                if(times.Count != 0)
                {
                    foreach(AvailableTime time in times)
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

        public async Task<List<AdvertCommandList>> GetSortByPriceMax(int count)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByPriceMax(count);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMin(int count)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByPriceMin(count);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMaxWithoutUserId(int count, int id)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByPriceMaxWithoutUserId(count, id);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMinWithoutUserId(int count, int id)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByPriceMinWithoutUserId(count, id);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMax(int count)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByRatingMax(count);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMin(int count)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByRatingMin(count);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMaxWithoutUserId(int count, int id)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByRatingMaxWithoutUserId(count, id);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMinWithoutUserId(int count, int id)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByRatingMinWithoutUserId(count, id);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByDateMin(int count)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByDateMin(count);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByDateMinWithoutUserId(int count, int id)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByDateMinWithoutUserId(count, id);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMaxByName(int count, string name)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByPriceMaxByName(count, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMinByName(int count, string name)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByPriceMinByName(count, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMaxWithoutUserIdByName(int count, int id, string name)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByPriceMaxWithoutUserIdByName(count, id, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMinWithoutUserIdByName(int count, int id, string name)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByPriceMinWithoutUserIdByName(count, id, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMaxByName(int count, string name)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByRatingMaxByName(count, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMinByName(int count, string name)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByRatingMinByName(count, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMaxWithoutUserIdByName(int count, int id, string name)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByRatingMaxWithoutUserIdByName(count, id, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMinWithoutUserIdByName(int count, int id, string name)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByRatingMinWithoutUserIdByName(count, id, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByDateMinByName(int count, string name)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByDateMinByName(count, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByDateMinWithoutUserIdByName(int count, int id, string name)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByDateMinWithoutUserIdByName(count, id, name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMaxByUserId(int id, int count)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByPriceMaxByUserId(id, count);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByPriceMinByUserId(int id, int count)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByPriceMinByUserId(id, count);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMaxByUserId(int id, int count)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByRatingMaxByUserId(id, count);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByRatingMinByUserId(int id, int count)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByRatingMinByUserId(id, count);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetSortByDateMinByUserId(int id, int count)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetSortByDateMinByUserId(id, count);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert, GetAverageRating(advert.Reviews))).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }
    }
}
