using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyArchitectV2VSIXProject.ClassesDef
{
    /// <summary>
    /// 取得領域邏輯類別描述定義
    /// </summary>
    public class DomainClassDef
    {
        /// <summary>
        /// 取得領域邏輯介面描述定義
        /// </summary>
        public static string GetDomainInterfaceTemplate
        {
            get
            {
                return @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $(NAMESPACE_DEF)$
{
    /// <summary>
    /// 車輛介面
    /// </summary>
    public interface $(INTERFACE_NAME)$
    {
        /// <summary>
        /// 計算車輛租金
        /// </summary>
        /// <param name=""daysRented""></param>
        /// <returns></returns>
        int CalculateRentalCost(int daysRented);
        /// <summary>
        /// 選擇租車時間
        /// </summary>
        /// <param name=""start""></param>
        /// <param name=""end""></param>
        /// <returns></returns>
        TimeSpan ChoiseRentalTime(DateTime start, DateTime end);
        /// <summary>
        /// 取得車輛類型
        /// </summary>
        /// <returns></returns>
        VehicleType GetVehicleType();
        /// <summary>
        /// 車種
        /// </summary>
        ModelType Model { get; set; }
        /// <summary>
        /// 排氣量
        /// </summary>
        string CC { get; set; }
    }
    /// <summary>
    /// 車輛類型
    /// </summary>
    public enum VehicleType { Car, RV }
    /// <summary>
    /// 車輛型號
    /// </summary>
    public enum ModelType { Toyota = 100, Lexus = 150 }
}

";
            }
        }

        /// <summary>
        /// 取得領域邏輯類別描述定義
        /// </summary>
        public static string GetDomainClassTemplate
        {
            get
            {
                return @"namespace $(NAMESPACE_DEF)$
{
    public class $(DOMAIN_CLASS)$ : $(INTERFACE_NAME)$
    {
        private ModelType _modelName;
        public ModelType Model { get => _modelName; set => value = _modelName; }
        public string CC { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public $(DOMAIN_CLASS)$(ModelType modelName)
        {
            _modelName = modelName;
        }
        /// <summary>
        /// 計算租車費用
        /// </summary>
        /// <param name=""daysRented""></param>
        /// <returns></returns>
        public int CalculateRentalCost(int daysRented)
        {
            return daysRented * (int)_modelName; // 假設為美元
        }

        /// <summary>
        /// 選擇租車時間
        /// </summary>
        /// <param name=""start""></param>
        /// <param name=""end""></param>
        /// <returns></returns>
        public TimeSpan ChoiseRentalTime(DateTime start, DateTime end)
        {
            return end - start;
        }
        /// <summary>
        /// 取得車輛類型
        /// </summary>
        /// <returns></returns>
        public VehicleType GetVehicleType() => VehicleType.Car;
    }
}
";
            }
        }

        public static string GetAggregateRootTemplate
        {
            get
            {
                return @"namespace $(NAMESPACE_DEF)$
{
    public interface IAggregateRoot
    {
    }
}";

            }
        }

        public static string GetEntityTemplate
        {
            get
            {
                return @"namespace $(NAMESPACE_DEF)$
{
    public class Entity
    {
    }
}";

            }
        }

        public static string GetValueObjectTemplate
        {
            get
            {
                return @"namespace $(NAMESPACE_DEF)$
{
    public abstract class ValueObject
    {
    }
}
";
            }
        }

        public static string GetTicketTemplate
        {
            get
            {
                return @"namespace $(NAMESPACE_DEF)$
{
    public class Ticket: Entity, IAggregateRoot
    {
        private SeatReservation _seatReservation;
        public SeatReservation SeatReservationInfo => _seatReservation;
        public Guid Id { get; set; }
        public string ReservatName { get; protected set;}
        // 確認購票
        public int SaveTicket()
        {
            return 0;
        }
        // 建立票卷實體
        public static Ticket Create(SeatReservation reserve)
        {
            return new Ticket() { Id = Guid.NewGuid(), _seatReservation = reserve };
        }
    }
}
";
            }
        }

        public static string GetShowTimeTemplate
        {
            get
            {
                return @"namespace $(NAMESPACE_DEF)$
{
    public class ShowTime: ValueObject
    {
        public ShowTime(DateTime? startShowTime, DateTime? endShowTime) 
        {
            if(!startShowTime.HasValue || !endShowTime.HasValue)
            {
                throw new ShowTimeNotDefinedException(@""票種的開始時間 startShowTime 與 結束時間 endShowTime 不可為空白！"");
            }
            _startShowTime = startShowTime;
            _endShowTime = endShowTime;
        }
        private DateTime? _startShowTime;
        public DateTime? GetStartShowTime()
        {
            return _startShowTime;
        }
        private DateTime? _endShowTime;
        public DateTime? GetEndShowTime()
        {
            return _endShowTime;
        }
    }
}
";
            }
        }

        public static string GetSeatReservationTemplate
        {
            get
            {
                return @"namespace $(NAMESPACE_DEF)$
{
    public class SeatReservation: Entity
    {
        public Guid Id { get; protected set; }
        public string ReserveName { get; protected set; }
        public ConcertVenue ReserveConcertVenue { get; protected set; }
        public ShowTime ShowTime { get; set; }
        // 確認與預訂票卷
        public Ticket SetReservat(Ticket ticket)
        {
            return ticket;
        }
        public string[] GetReserveByDate(TimeSpan reserveRange)
        {
            return new string[] {};
        }
        public static SeatReservation Create(string ReserveName, ShowTime ReserveShowTime)
        {
            return new SeatReservation() { Id = Guid.NewGuid(), ShowTime = ReserveShowTime };
        }
        // 檢核購票時間、並檢核選擇票種是否還有位子？（如預定是空位，則傳回：true; 否則傳回：false）
        public bool CheckVenueIsExist()
        {
            return true;
        }
    }
}
";
            }
        }

        public static string GetShowTimeNotDefinedExceptionTemplate
        {
            get
            {
                return @"namespace $(NAMESPACE_DEF)$
{
    // 票種場次未定義的自訂錯誤 Exception
    public class ShowTimeNotDefinedException: Exception
    {
        public ShowTimeNotDefinedException(string message) : base(message) {}
    }
}
";
            }
        }

        public static string GetConcertVenueTemplate
        {
            get
            {
                return @"namespace $(NAMESPACE_DEF)$
{
    // 演唱會場次
    public class ConcertVenue: ValueObject
    {
        public int ShowTimeNum { get; set; }
        public string ShowTimeName {get;set;}

    }
}
";
            }
        }
    }
}
