using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace Salam.Cms.Web.Features.EnquireProduct.Services
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Get(string key)
        {
            return _httpContextAccessor.HttpContext?.Session.GetString(key);
        }

        public void Set(string key, string value)
        {
            _httpContextAccessor.HttpContext?.Session.SetString(key, value);
        }

        public void Remove(string key)
        {
            _httpContextAccessor.HttpContext?.Session.Remove(key);
        }

        public void RemoveAllSession()
        {
            _httpContextAccessor.HttpContext?.Session.Clear();
        }

        public void SetObject<T>(string key, T value)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(value, settings);
            _httpContextAccessor.HttpContext?.Session.SetString(key, json);
        }

        public T GetObject<T>(string key)
        {
            var json = _httpContextAccessor.HttpContext?.Session.GetString(key);
            if (string.IsNullOrEmpty(json))
                return default;
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
