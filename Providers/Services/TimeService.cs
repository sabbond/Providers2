namespace Providers.Services;

public class TimeService: ITimeService {
    public DateTime Now(){
        return DateTime.Now;
    }
}