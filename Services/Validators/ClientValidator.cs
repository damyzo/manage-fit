using Entities.Client.Model;
using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validators
{
    public static class ClientValidator
    {
        public static Result<Client> Validate(Client client)
        {
            if (client == null 
                || string.IsNullOrEmpty(client.Name) 
                || string.IsNullOrEmpty(client.Email))
            {
                return new Result<Client>(
                    value: client,
                    isSuccess: false, 
                    message: "Client info is required.");
            }

            if (client.Name.Length <= 0)
            {
                return new Result<Client>(
                    value: client,
                    isSuccess: false,
                    message: "Client name shoud have more than 0 characters.");
            }

            if (client.Email.Length <= 0)
            {
                return new Result<Client>(
                    value: client,
                    isSuccess: false,
                    message: "Client name shoud have more than 0 characters.");
            }

            return new Result<Client>(
                value: client,
                isSuccess: true,
                message: "Valid Data");
        }
    }
}
