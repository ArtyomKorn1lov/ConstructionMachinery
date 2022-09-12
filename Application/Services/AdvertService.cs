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

        public List<AvailableTime> FillAvailableTime(DateTime startDate, DateTime endDate, int startTime, int endTime)
        {
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

        public async Task<List<AdvertCommandList>> GetAllWithoutUserId(int id)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetAllWithoutUserId(id);
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

        public async Task<List<AdvertCommandList>> GetByNameWithoutUserId(string name, int id)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetByNameWithoutUserId(name, id);
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

        public async Task<List<AdvertCommandForRequest>> GetForRequestCustomer(int id)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetUserAdvertsWithPendingConfirmationForCustomer(id);
                List<AdvertCommandForRequest> commands = adverts.Select(advert => AdvertCommandConverter.AdvertConvertToAdvertCommandForRequest(advert)).ToList();
                return commands;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdvertCommandForRequest>> GetForRequestLandlord(int id)
        {
            try
            {
                List<Advert> adverts = await _advertRepository.GetUserAdvertsWithPendingConfirmationForLandlord(id);
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
                AdvertCommandUpdate advert = AdvertCommandConverter.AdvertEntityConvertToAdvertCommandUpdate(await _advertRepository.GetForUpdate(id));
                return advert;
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
