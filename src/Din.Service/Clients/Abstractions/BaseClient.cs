﻿using System.Text;

namespace Din.Service.Clients.Abstractions
{
    public abstract class BaseClient
    {
        protected string BuildUrl(params string[] p)
        {
            var sb = new StringBuilder();

            foreach (var i in p)
            {
                sb.Append(i);
            }

            return sb.ToString();
        }
    }
}