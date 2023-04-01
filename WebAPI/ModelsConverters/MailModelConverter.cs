using WebAPI.Models;
using Application.Commands;

namespace WebAPI.ModelsConverters
{
    public static class MailModelConverter
    {
        public static MailCommandCreate ModelConvertToMailCommandCreate(MailModelCreate model)
        {
            if (model == null)
                return null;
            return new MailCommandCreate
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Description = model.Description
            };
        }
    }
}
