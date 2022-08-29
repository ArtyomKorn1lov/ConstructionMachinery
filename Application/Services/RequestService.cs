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

        public RequestService(IRequestRepository requestRepository, IAccountRepository accountRepository , IAdvertRepository advertRepository)
        {
            _requestRepository = requestRepository;
            _accountRepository = accountRepository;
            _advertRepository = advertRepository;
        }

        public async Task<bool> Confirm(int id, int stateId)
        {
            try
            {
                AvailabilityRequest request = await _requestRepository.GetById(id);
                request.RequestStateId = stateId;
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
                    foreach (AvailableTimeCommandForCreateRequest time in request.AvailableTimeCommandForCreateRequests)
                        await _requestRepository.UpdateTime(time.Id, time.AvailabilityStateId);
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
                    await GetAdvertNameById(request.AvailableTimes[0].AdvertId), await GetPhoneForCustomer(request.AvailableTimes[0].AdvertId), await GetLandLordName(request.AvailableTimes[0].AdvertId));
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
                List<Advert> adverts = await _advertRepository.GetByUserId(id);
                AvailabilityRequest request = await _requestRepository.GetById(id);
                if (request == null)
                    return null;
                if (await GetUserIdByAdvertId(request.AvailableTimes[0].AdvertId) != userId)
                    return null;
                if (request.RequestStateId != 3)
                    return null;
                AvailabilityRequestCommandForLandlord commandForLandlord = RequestCommandConverter.AvailabilityRequestEntityConvertToAvailabilityRequestCommandForLandlord(request,
                       await GetAdvertNameById(request.AvailableTimes[0].AdvertId), await GetPhoneForLandlord(request.UserId), await GetCustomerName(request.UserId));
                return commandForLandlord;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AvailabilityRequestListCommand>> GetListForCustomer(int id)
        {
            try
            {
                List<AvailabilityRequest> requests = await _requestRepository.GetByUserId(id);
                if (requests == null)
                    return null;
                List<AvailabilityRequestListCommand> commands = new List<AvailabilityRequestListCommand>();
                foreach (AvailabilityRequest request in requests)
                {
                    commands.Add(RequestCommandConverter.EntityConvertToAvailabilityRequestListCommand(request,
                        await GetAdvertNameById(request.AvailableTimes[0].AdvertId)));
                }
                return commands;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AvailabilityRequestListCommand>> GetListForLandlord(int id)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetByUserId(id);
                if (adverts == null)
                    return null;
                List<AvailabilityRequest> requests = await GetByAdvertId(adverts);
                if (requests == null)
                    return null;
                List<AvailabilityRequestListCommand> commands = new List<AvailabilityRequestListCommand>();
                foreach (AvailabilityRequest request in requests)
                {
                    commands.Add(RequestCommandConverter.EntityConvertToAvailabilityRequestListCommand(request,
                        await GetAdvertNameById(request.AvailableTimes[0].AdvertId)));
                }
                return commands;
            }
            catch
            {
                return null;
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

        public async Task<bool> Remove(int id)
        {
            try
            {
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

        public async Task<List<AvailabilityRequest>> GetByAdvertId(List<Advert> adverts)
        {
            List<AvailableTime> times = new List<AvailableTime>();
            foreach(Advert advert in adverts)
            {
                times = times.Concat(await _requestRepository.GetTimesByAdvertId(advert.Id)).ToList();
            }
            List<AvailabilityRequest> requests = new List<AvailabilityRequest>();
            foreach (AvailableTime time in times)
            {
                requests.Add(await _requestRepository.GetById((int)time.AvailabilityRequestId));
            }
            return requests;
        }
    }
}
