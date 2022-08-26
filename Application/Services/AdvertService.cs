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

        public AdvertService(IAdvertRepository advertRepository, IAccountRepository accountRepository)
        {
            _advertRepository = advertRepository;
            _accountRepository = accountRepository;
        }

        public async Task<bool> Create(AdvertCommandCreate advert)
        {
            try
            {
                if(advert != null)
                {
                    for(int count = 0; count < advert.AvailableTimeCommandCreates.Count; count++)
                    {
                        advert.AvailableTimeCommandCreates[count].AvailabilityStateId = 1;
                    }
                    await _advertRepository.Create(AdvertCommandConverter.AdvertCommandCreateConvertToAdvertEntity(advert));
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<AdvertCommandList>> GetAll()
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetAll();
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert)).ToList();
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

        public async Task<List<AdvertCommandList>> GetByName(string name)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetByName(name);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert)).ToList();
                return advertCommandList;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandList>> GetByUserId(int id)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetByUserId(id);
                List<AdvertCommandList> advertCommandList = adverts.Select(advert => AdvertCommandConverter.AdvertEntityConvertToAdvertCommandList(advert)).ToList();
                return advertCommandList;
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
                AdvertCommandUpdate advert = AdvertCommandConverter.AdvertEntityConvertToAdvertCommandUpdate(await _advertRepository.GetForUpdate(id));
                return advert;
            }
            catch
            {
                return null;
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
                await _advertRepository.Remove(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(AdvertCommandUpdate advert)
        {
            try
            {
                if (advert != null)
                {
                    await _advertRepository.Update(AdvertCommandConverter.AdvertCommandUpdateConvertToAdvertEntity(advert));
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
