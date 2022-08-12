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
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<AvailabilityRequestCommandForCustomer>> GetForCustomer(int id)
        {
            try
            {
                List<AvailabilityRequest> requests = await _requestRepository.GetByUserId(id);
                if (requests == null)
                    return null;
                List<AvailabilityRequestCommandForCustomer> commandForCustomers = new List<AvailabilityRequestCommandForCustomer>();
                foreach (AvailabilityRequest request in requests)
                {
                    commandForCustomers.Add(RequestCommandConverter.AvailabilityRequestEntityConvertToAvailabilityRequestCommandForCustomer(request, 
                        await GetPhoneForCustomer(request.AvailableTimes[0].AdvertId), await GetLandLordName(request.AvailableTimes[0].AdvertId)));
                }
                return commandForCustomers;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AvailabilityRequestCommandForLandlord>> GetForLandlord(int id)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetByUserId(id);
                if (adverts == null)
                    return null;
                List<AvailabilityRequest> requests = await GetByAdvertId(adverts);
                if (requests == null)
                    return null;
                List<AvailabilityRequestCommandForLandlord> commandForLandlords = new List<AvailabilityRequestCommandForLandlord>();
                foreach(AvailabilityRequest request in requests)
                {
                    commandForLandlords.Add(RequestCommandConverter.AvailabilityRequestEntityConvertToAvailabilityRequestCommandForLandlord(request,
                        await GetPhoneForLandlord(request.UserId), await GetCustomerName(request.UserId)));
                }
                return commandForLandlords;
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
            User user = await _accountRepository.GetById(advert.Id);
            return user.Phone;
        }

        public async Task<string> GetLandLordName(int id)
        {
            Advert advert = await _advertRepository.GetById(id);
            User user = await _accountRepository.GetById(advert.Id);
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

        public async Task<List<AvailabilityRequest>> GetByAdvertId(List<Advert> adverts)
        {
            List<AvailabilityRequest> requests = new List<AvailabilityRequest>();
            foreach(Advert advert in adverts)
            {
                requests.Concat(await _requestRepository.GetByAdvertId(advert.Id)).ToList();
            }
            return requests;
        }
    }
}
