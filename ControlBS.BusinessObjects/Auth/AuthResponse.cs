namespace ControlBS.BusinessObjects.Auth
{
    public class AuthResponse
    {
        public int id { get; set; }
        public string? names { get; set; }
        public string? userName { get; set; }
        public string token { get; set; }

        public AuthResponse(CTPERS oCTPERS, string token)
        {
            id = oCTPERS.PERSIDEN;
            names = oCTPERS.PERSNAME;
            userName = oCTPERS.PERSNMUS;
            this.token = token;

        }
    }
}