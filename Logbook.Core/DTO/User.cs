using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core.DTO
{
    public class User
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Status { get; set; }

    //    [UserId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    //[Email] NVARCHAR(255) NOT NULL,
    //[PasswordHash] BINARY(32)  NULL, 
    //[PasswordSalt] BINARY(32)  NULL, 
    //[Name] NVARCHAR(255) NULL, 
    //[Location] NVARCHAR(255) NOT NULL,
    //[Status] NVARCHAR(255) NOT NULL
    }
}
