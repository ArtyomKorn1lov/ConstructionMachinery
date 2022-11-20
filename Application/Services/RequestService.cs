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

        public RequestService(IRequestRepository requestRepository, IAccountRepository accountRepository , IAdvertRepository advertRepository, IImageRepository imageRepository)
        {
            _requestRepository = requestRepository;
            _accountRepository = accountRepository;
            _advertRepository = advertRepository;
            _imageRepository = imageRepository;
        }

        public async Task<bool> Confirm(int id, int stateId)
        {
            try
            {
                AvailabilityRequest request = await _requestRepository.GetById(id);
                int availabilityStateId = 3;
                if (stateId == 1)
                    availabilityStateId = 2;
                if (stateId == 2)
                    availabilityStateId = 1;
                request.RequestStateId = stateId;
                for (int count = 0; count < request.AvailableTimes.Count; count++)
                    request.AvailableTimes[count].AvailabilityStateId = availabilityStateId;
                await _requestRepository.Confirm(request);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Create(AvailabilityRequestCommandCreate request)
        {
            try
            {
                if(request != null)
                {
                    AvailabilityRequest entityRequest = RequestCommandConverter.AvailabilityRequestCommandCreateConvertToAvailabilityRequestEntity(request);
                    await _requestRepository.Create(entityRequest);
                    return true;
                }
                return false;
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
                if (requestId == 0)
                    return false;
                if(times != null)
                {
                    foreach (AvailableTimeCommandForCreateRequest time in times)
                        await _requestRepository.UpdateTime(time.Id, requestId, 3);
                    return true;
                }
                return false;
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
                AvailabilityRequest request = await _requestRepository.GetById(id);
                if (request == null)
                    return null;
                if (request.UserId != userId)
                    return null;
                AvailabilityRequestCommandForCustomer commandForCustomer = RequestCommandConverter.AvailabilityRequestEntityConvertToAvailabilityRequestCommandForCustomer(request,
                    await _imageRepository.GetByAdvertId(request.AvailableTimes[0].AdvertId), await GetAdvertNameById(request.AvailableTimes[0].AdvertId), await GetPhoneForCustomer(request.AvailableTimes[0].AdvertId), await GetLandLordName(request.AvailableTimes[0].AdvertId));
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
                AvailabilityRequest request = await _requestRepository.GetById(id);
                request.IsAvailable = true;
                if (request == null)
                    return null;
                if (await GetUserIdByAdvertId(request.AvailableTimes[0].AdvertId) != userId)
                    return null;
                if (request.RequestStateId != 3)
                    return null;
                AvailabilityRequestCommandForLandlord commandForLandlord = RequestCommandConverter.AvailabilityRequestEntityConvertToAvailabilityRequestCommandForLandlord(request,
                    await _imageRepository.GetByAdvertId(request.AvailableTimes[0].AdvertId), await GetAdvertNameById(request.AvailableTimes[0].AdvertId), await GetPhoneForLandlord(request.UserId), await GetCustomerName(request.UserId));
                return commandForLandlord;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AvailabilityRequestListCommand>> GetListForCustomer(int id, int userId, int count)
        {
            try
            {
                List<AvailabilityRequest> requests = await _requestRepository.GetByAdvertIdUserIdForCustomer(id, userId, count);
                if (requests == null)
                    return null;
                List<AvailabilityRequestListCommand> commands = new List<AvailabilityRequestListCommand>();
                foreach (AvailabilityRequest request in requests)
                {
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

        public async Task<List<AvailabilityRequestListCommand>> GetListForLandlord(int id, int userId, int count)
        {
            try
            {
                List<AvailabilityRequest> requests = await _requestRepository.GetByAdvertIdUserIdForLandlord(id, userId, count);
                if (requests == null)
                    return null;
                List<AvailabilityRequestListCommand> commands = new List<AvailabilityRequestListCommand>();
                foreach (AvailabilityRequest request in requests)
                {
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

        public async Task<List<AvailableTimeCommand>> GetAvailableTimesByAdvertId(int id, int userId)
        {
            try
            {
                Advert advert = await _advertRepository.GetById(id);
                if (advert.UserId == userId)
                    return null;
                List<AvailableTime> times = await _requestRepository.GetTimesForRequestByAdvertId(id);
                if (advert == null)
                    return null;
                List<AvailableTimeCommand> commands = times.Select(time => RequestCommandConverter.EntityConvertToAvailableTimeCommand(time)).ToList();
                return commands;
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
                AvailabilityRequest request = await _requestRepository.GetById(id);
                if (request.UserId != userId)
                    return false;
                if (request.AvailableTimes != null)
                {
                    foreach (AvailableTime time in request.AvailableTimes)
                        await _requestRepository.UpdateTime(time.Id, request.Id, 1);
                    await _requestRepository.Remove(id);
                    return true;
                }
                return false;
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
