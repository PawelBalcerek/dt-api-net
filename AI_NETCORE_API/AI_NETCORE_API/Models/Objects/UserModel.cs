using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI_NETCORE_API.Models.Objects
{
    public class UserModel
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// User Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// User Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User Cash
        /// </summary>
        public decimal Cash { get; set; }
        
    }
}
