using Parcial_SaraMelissaMarroquinVega.DAL.Entities;

namespace Parcial_SaraMelissaMarroquinVega.DAL
{
    public class SeederDb
    {
        private readonly DataBaseContext _context;
        public SeederDb(DataBaseContext context)
        {
            _context = context;
        }

        public async Task SeederAsync()
        {
            await _context.Database.EnsureCreatedAsync(); //Esta línea me ayuda a crear mi BD de forma automática
            await PopulateTicketsAsync();

            await _context.SaveChangesAsync();
        }
        private async Task PopulateTicketsAsync()
        {
            if (!_context.Tickets.Any())
            {
                for (int i = 0; i < 200; i++)
                {
                    _context.Tickets.Add(
                        new Ticket
                        {
                            UseDate = null,
                            IsUsed = false,
                            EntranceGate = null
                        });
                }
            }
        }
    }
}
