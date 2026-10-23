namespace SchoolAttendance.Application.Welcome;
public class WelcomeService : IWelcomeService
{
    public string GetMessage()
    {
        return "School Attendance API is running";
    }

}