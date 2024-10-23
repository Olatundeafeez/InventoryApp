﻿using Microsoft.AspNetCore.DataProtection;

namespace InventoryAPI.Service
{
    public class BaseSetup
    {
        public string Sender { get; set; }
        public int Port { get; set; }
        public string Server { get; set; }
        public string DisplayName {  get; set; }
        public string Password { get; set; }
        public string SecretKey {get; set; }
        public string Issuer { get; set; }
    }
}
