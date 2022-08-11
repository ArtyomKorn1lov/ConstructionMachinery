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

        public RequestService(IRequestRepository requestRepository, IAccountRepository accountRepository)
        {
            _requestRepository = requestRepository;
            _accountRepository = accountRepository;
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
                List<AvailabilityRequestCommandForCustomer> commandForCustomers = new List<AvailabilityRequestCommandForCustomer>();
                foreach (AvailabilityRequest request in requests)
                {
                    commandForCustomers.Add(RequestCommandConverter.AvailabilityRequestEntityConvertToAvailabilityRequestCommandForCustomer(request, await GetPhoneByUser(request.UserId)));
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
                return null
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

        public async Task<string> GetPhoneByUser(int id)
        {
            User user = await _accountRepository.GetById(id);
            return user.Phone;
        }
    }
}
