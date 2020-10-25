using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SPapi.NET.Exceptions;
using SPapi.NET.Entities;
using SPapi.NET.Enums;
using SPapi.NET.Events;
using Timer = System.Timers.Timer;
using Weather = SPapi.NET.Entities.Weather;

namespace SPapi.NET
{
    public class SpClient
    {
        public SpClient()
        {
            Timer aTimer = new Timer();
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Interval = 3000;
            aTimer.Enabled = true;
        }


        public AsyncEvent<MessageAddEventArgs> MessageAdd;
        private DateTime _lastMessageTime = DateTime.Now;

        public AsyncEvent<TimeTicksUpdateEventArgs> TimeTicksUpdate;
        private int _lastTicks;

        public AsyncEvent<WeatherUpdateEventArgs> WeatherUpdate;
        private string _lastWeather;

        public async Task<Players> GetOnlinePlayersAsync()
        {
            var response = await Request.Get("online");

            JObject o = JObject.Parse(response);

            if (bool.Parse(o["error"].ToString()))
            {
                throw new BadRequestException("Request error occured");
            }

            var players = JsonConvert.DeserializeObject<Players>(response);

            return players;
        }

        public async Task<List<Message>> GetChatMessagesAsync()
        {
            var response = await Request.Get("chat");

            JObject o = JObject.Parse(response);

            if (bool.Parse(o["error"].ToString()))
            {
                throw new BadRequestException("Request error occured");
            }

            var messages = JsonConvert.DeserializeObject<Messages>(response);

            return messages.PlayerMessages;
        }

        public async Task<Time> GetDayTimeAsync()
        {
            var response = await Request.Get("time");

            JObject o = JObject.Parse(response);

            if (bool.Parse(o["error"].ToString()))
            {
                throw new BadRequestException("Request error occured");
            }

            var daytime = JsonConvert.DeserializeObject<Time>(response);

            return daytime;
        }

        public async Task<Enums.Weather> GetWeatherAsync()
        {
            var response = await Request.Get("weather");

            JObject o = JObject.Parse(response);

            if (bool.Parse(o["error"].ToString()))
            {
                throw new BadRequestException("Request error occured");
            }

            var weather = JsonConvert.DeserializeObject<Weather>(response);

            return weather.WorldWeather;
        }

        private async void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            try
            {

                if (MessageAdd != null)
                {
                    var response = await Request.Get("chat");

                    JObject o = JObject.Parse(response);

                    if (bool.Parse(o["error"].ToString()))
                    {
                        throw new BadRequestException("Request error occured");
                    }

                    var time = UnixTimeConverter.UnixTimeStampToDateTime(Convert.ToDouble(o["messages"][0]["time"]));

                    if (_lastMessageTime < time)
                    {
                        await MessageAdd.InvokeAsync(this, new MessageAddEventArgs
                        {
                            Nickname = o["messages"][0]["name"].ToString(),
                            Time = time,
                            Uuid = o["messages"][0]["uuid"].ToString(),
                            Content = o["messages"][0]["message"].ToString()
                        });
                        _lastMessageTime = time;
                    }
                }

                if (TimeTicksUpdate != null)
                {
                    var response = await Request.Get("time");

                    JObject o = JObject.Parse(response);

                    if (bool.Parse(o["error"].ToString()))
                    {
                        throw new BadRequestException("Request error occured");
                    }

                    var ticks = (int) o["ticks"];

                    if (_lastTicks != ticks)
                    {
                        await TimeTicksUpdate.InvokeAsync(this, new TimeTicksUpdateEventArgs
                        {
                            DayTime = o["time"].ToString() == "DAY" ? DayTime.Day : DayTime.Night,
                            Ticks = ticks
                        });
                        _lastTicks = ticks;
                    }
                }

                if (WeatherUpdate != null)
                {
                    var response = await Request.Get("weather");

                    JObject o = JObject.Parse(response);

                    if (bool.Parse(o["error"].ToString()))
                    {
                        throw new BadRequestException("Request error occured");
                    }

                    var weather = o["weather"].ToString();

                    if (_lastWeather != weather)
                    {
                        await WeatherUpdate.InvokeAsync(this, new WeatherUpdateEventArgs
                        {
                            Weather = weather switch
                            {
                                "CLEAR" => Enums.Weather.Clear,
                                "RAIN" => Enums.Weather.Rain,
                                "THUNDER" => Enums.Weather.Thunder,
                                _ => Enums.Weather.Clear
                            },
                            Before = _lastWeather switch
                            {
                                "CLEAR" => Enums.Weather.Clear,
                                "RAIN" => Enums.Weather.Rain,
                                "THUNDER" => Enums.Weather.Thunder,
                                _ => Enums.Weather.Clear
                            }
                        });
                        _lastWeather = weather;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RateLimitHitException("RateLimit hit", ex);
            }
        }
        
    }
}
