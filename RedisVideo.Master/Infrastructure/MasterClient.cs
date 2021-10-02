﻿using Microsoft.Extensions.Logging;
using RedisVideo.Master.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisVideo.Master.Infrastructure
{
    public class MasterClient : IMasterClient
    {
        private readonly HttpClient _HttpClient;
        private readonly ILogger _logger;

        public MasterClient(ILogger logger, HttpClient httpClient)
        {
            _logger = logger;
            _HttpClient = httpClient;
        }

        public async Task SendReplicationRequestAsync(string slaveUrl, string key, string value, CancellationToken cancellationToken)
        {
            try
            {
                var url = $"{slaveUrl}/master/replication?key={key}";
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    var content = new StringContent(value, Encoding.UTF8, "application/json");
                    requestMessage.Content = content;

                    using(var response=await _HttpClient.SendAsync(requestMessage,cancellationToken))
                    {
                        if (!response.IsSuccessStatusCode)
                            throw new Exception($"Cannot replicate, got status code: {response.StatusCode}. Body: {await response.Content.ReadAsStringAsync()}");
                    }

                }
                

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Cannot synchronize with slave with {slaveUrl} url. Error: {ex.Message}");
                throw;
            }
        }
    }
}
