namespace Salam.Cms.Web.Features.EnquireProduct.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ISessionService
{
    string Get(string cookie);
    void Set(string cookie, string value);
    void Remove(string cookie);
    void RemoveAllSession();
    void SetObject<T>(string key, T value);
    T GetObject<T>(string key);
}
