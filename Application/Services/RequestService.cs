using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using Application.IServices;
using Application.CommandsConverters;
using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services
{
    public class RequestService : IRequestService
    {
        private IRequestRepository _requestRepository;
        private IAccountRepository _accountRepository;
        private IAdvertRepository _advertRepository;
        private IImageRepository _imageRepository;
        private IAdvertService _advertService;

        public RequestService(IRequestRepository requestRepository, IAccountRepository accountRepository , IAdvertRepository advertRepository, 
            IImageRepository imageRepository, IAdvertService advertService)
        {
            _requestRepository = requestRepository;
            _accountRepository = accountRepository;
            _advertRepository = advertRepository;
            _imageRepository = imageRepository;
            _advertService = advertService;
        }

        public async Task<bool> Confirm(int id, int stateId, int userId)
        {
            try
            {
                if (id <= 0 || stateId <= 0)
                    return false;
                AvailabilityRequest request = await _requestRepository.GetById(id);
                if (request == null)
                    return false;
                if (request.Id != id)
                    return false;
                if (request.AvailableTimes.Count <= 0)
                    return false;
                int requestByAdvertUserId = await _advertService.GetUserIdByAdvert(request.AvailableTimes[0].AdvertId);
                if (requestByAdvertUserId != userId)
                    return false;
                int availabilityStateId = 3;
                if (stateId == 1)
                    availabilityStateId = 2;
                if (stateId == 2)
                    availabilityStateId = 1;
                request.RequestStateId = stateId;
                request.IsAvailable = true;
                for (int count = 0; count < request.AvailableTimes.Count; count++)
                {
                    request.AvailableTimes[count].AvailabilityStateId = availabilityStateId;
                }
                await _requestRepository.Confirm(request);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Create(AvailabilityRequestCommandCreate request, int timeId, int count)
        {
            try
            {
                if (request == null)
                    return false;
                if (request.Address == null || request.Address.Trim() == "")
                    return false;
                if (timeId <= 0)
                    return false;
                if (count <= 0)
                    return false;
                Advert advert = await _advertRepository.GetAdvertByTimeId(timeId);
                int sum = advert.Price * count;
                AvailabilityRequest entityRequest = RequestCommandConverter.AvailabilityRequestCommandCreateConvertToAvailabilityRequestEntity(request, sum);
                entityRequest.Created = DateTime.Now;
                entityRequest.Updated = entityRequest.Created;
                await _requestRepository.Create(entityRequest);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateTimes(int requestId, List<AvailableTimeCommandForCreateRequest> times)
        {
            try
            {
                if (requestId <= 0)
                    return false;
                if (times == null)
                    return false;
                foreach (AvailableTimeCommandForCreateRequest time in times)
                {
                    AvailableTime currentElement = await _requestRepository.GetTimeById(time.Id);
                    if (currentElement == null)
                        return false;
                    AvailabilityRequest request = await _requestRepository.GetById(requestId);
                    if (request == null)
                        return false;
                    await _requestRepository.UpdateTime(time.Id, requestId, 3);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<AvailabilityRequestCommandForCustomer> GetForCustomer(int id, int userId)
        {
            try
            {
                if (id <= 0 || userId <= 0)
                    return null;
                AvailabilityRequest request = await _requestRepository.GetById(id);
                if (request == null)
                    return null;
                if (request.UserId != userId)
                    return null;
                if (request.AvailableTimes.Count <= 0)
                    return null;
                request.AvailableTimes.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
                DateTime startRent = request.AvailableTimes[0].Date;
                DateTime endRent = request.AvailableTimes[request.AvailableTimes.Count - 1].Date;
                AvailabilityRequestCommandForCustomer commandForCustomer = RequestCommandConverter.AvailabilityRequestEntityConvertToAvailabilityRequestCommandForCustomer(request,
                    await _imageRepository.GetByAdvertId(request.AvailableTimes[0].AdvertId), await GetAdvertNameById(request.AvailableTimes[0].AdvertId), 
                    await GetPhoneForCustomer(request.AvailableTimes[0].AdvertId), await GetLandLordName(request.AvailableTimes[0].AdvertId), startRent, endRent);
                return commandForCustomer;
            }
            catch
            {
                return null;
            }
        }

        public async Task<AvailabilityRequestCommandForLandlord> GetForLandlord(int id, int userId)
        {
            try
            {
                if (id <= 0 || userId <= 0)
                    return null;
                AvailabilityRequest request = await _requestRepository.GetById(id);
                if (request == null)
                    return null;
                if (await GetUserIdByAdvertId(request.AvailableTimes[0].AdvertId) != userId)
                    return null;
                if (request.RequestStateId != 3)
                    return null;
                if (request.AvailableTimes.Count <= 0)
                    return null;
                request.AvailableTimes.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
                DateTime startRent = request.AvailableTimes[0].Date;
                DateTime endRent = request.AvailableTimes[request.AvailableTimes.Count - 1].Date;
                AvailabilityRequestCommandForLandlord commandForLandlord = RequestCommandConverter.AvailabilityRequestEntityConvertToAvailabilityRequestCommandForLandlord(request,
                    await _imageRepository.GetByAdvertId(request.AvailableTimes[0].AdvertId), await GetAdvertNameById(request.AvailableTimes[0].AdvertId), 
                    await GetPhoneForLandlord(request.UserId), await GetCustomerName(request.UserId), startRent, endRent);
                return commandForLandlord;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AvailabilityRequestListCommand>> GetListForCustomer(int id, int userId, int page)
        {
            try
            {
                if (id <= 0 || userId <= 0 || page < 0)
                    return null;
                List<AvailabilityRequest> requests = await _requestRepository.GetByAdvertIdUserIdForCustomer(id, userId, page);
                if (requests == null)
                    return null;
                List<AvailabilityRequestListCommand> commands = new List<AvailabilityRequestListCommand>();
                foreach (AvailabilityRequest request in requests)
                {
                    request.AvailableTimes.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
                    commands.Add(RequestCommandConverter.EntityConvertToAvailabilityRequestListCommand(request, request.AvailableTimes[0].Date,
                         await GetAdvertNameById(id), await _imageRepository.GetByAdvertId(id)));
                }
                return commands;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AvailabilityRequestListCommand>> GetListForLandlord(int userId, int page)
        {
            try
            {
                if (userId <= 0 || page < 0)
                    return null;
                List<AvailabilityRequest> requests = await _requestRepository.GetByUserIdForLandlord(userId, page);
                if (requests == null)
                    return null;
                List<AvailabilityRequestListCommand> commands = new List<AvailabilityRequestListCommand>();
                foreach (AvailabilityRequest request in requests)
                {
                    request.AvailableTimes.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
                    commands.Add(RequestCommandConverter.EntityConvertToAvailabilityRequestListCommand(request, request.AvailableTimes[0].Date,
                        await GetAdvertNameById(request.AvailableTimes[0].AdvertId), await _imageRepository.GetByAdvertId(request.AvailableTimes[0].AdvertId)));
                }
                return commands;
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> GetLastRequestId()
        {
            try
            {
                return await _requestRepository.GetLastRequestId();
            }
            catch
            {
                return 0;
            }
        }

        public async Task<LeaseRequestCommand> GetAvailableTimesByAdvertId(int id, int userId)
        {
            try
            {
                if (userId <= 0 || id <= 0)
                    return null;
                Advert advert = await _advertRepository.GetById(id);
                if (advert == null)
                    return null;
                if (advert.UserId == userId)
                    return null;
                List<AvailableTime> times = await _requestRepository.GetTimesForRequestByAdvertId(id);
                if (times == null)
                    return null;
                List<AvailableTimeCommand> commands = times.Select(time => RequestCommandConverter.EntityConvertToAvailableTimeCommand(time)).ToList();
                List<AvailiableDayCommand> dayCommands = _advertService.PackageToDayCommands(commands);
                dayCommands = _advertService.SortDateCommmands(dayCommands);
                LeaseRequestCommand leaseCommand = new LeaseRequestCommand
                {
                    Price = advert.Price,
                    AvailiableDayCommands = dayCommands
                };
                return leaseCommand;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Remove(int id, int userId)
        {
            try
            {
                if (userId <= 0 || id <= 0)
                    return false;
                AvailabilityRequest request = await _requestRepository.GetById(id);
                if (request.UserId != userId)
                    return false;
                if (request.AvailableTimes == null)
                    return false;
                foreach (AvailableTime time in request.AvailableTimes)
                    await _requestRepository.UpdateTime(time.Id, request.Id, 1);
                await _requestRepository.Remove(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetPhoneForCustomer(int id)
        {
            Advert advert = await _advertRepository.GetById(id);
            User user = await _accountRepository.GetById(advert.UserId);
            return user.Phone;
        }

        public async Task<string> GetLandLordName(int id)
        {
            Advert advert = await _advertRepository.GetById(id);
            User user = await _accountRepository.GetById(advert.UserId);
            return user.Name;
        }

        public async Task<string> GetPhoneForLandlord(int id)
        {
            User user = await _accountRepository.GetById(id);
            return user.Phone;
        }

        public async Task<string> GetCustomerName(int id)
        {
            User user = await _accountRepository.GetById(id);
            return user.Name;
        }

        public async Task<string> GetAdvertNameById(int id)
        {
            Advert advert = await _advertRepository.GetById(id);
            return advert.Name;
        }

        public async Task<int> GetUserIdByAdvertId(int id)
        {
            Advert advert = await _advertRepository.GetById(id);
            User user = await _accountRepository.GetById(advert.UserId);
            return user.Id;
        }

        public async Task<bool> IsAttention(int userId)
        {
            try
            {
                return await _requestRepository.IsAttention(userId);
            }
            catch
            {
                return false;
            }
        }
    }
}
