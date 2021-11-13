using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HospitalManagementWebApi
{
    class WebApiMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var task = new Task<HttpResponseMessage>(() =>
            {
                HttpResponseMessage message = new HttpResponseMessage();
                message.Content = new StringContent("I'm a new response!!");
                return message;
            });

            task.Start();
            return task;
        }
    }
}