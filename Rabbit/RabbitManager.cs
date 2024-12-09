using UrFUCoworkingsAPIGateway.Rabbit.Services;

namespace UrFUCoworkingsAPIGateway.Rabbit
{
    public class RabbitManager
    {
        public Coworkings Coworkings { get; set; }
        public CoworkingSettings CoworkingSettings { get; set; }
        public Reservations Reservations { get; set; }
        public ReservationTimes ReservationTimes { get; set; }
        public Zones Zones { get; set; }
        public Places Places { get; set; }
        public MessageProvider MessageProvider {  get; set; } 
        public RabbitManager(IServiceProvider provider) 
        {
            Coworkings = new(provider);
            CoworkingSettings = new(provider);
            Reservations = new(provider);
            ReservationTimes = new(provider);
            Zones = new(provider);
            MessageProvider = new(provider);
        }
    }
}
