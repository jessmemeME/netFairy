using System.ComponentModel.DataAnnotations;

namespace FairyBE.Models.EventModels

{
    public class Event
    {
        public long Id { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public long ClientId { get; set; }

        [Required]
        public long CreatedUserId { get; set; }

        [Required]
        public long UpdatedUserId { get; set; }

        [Required]
        public DateTime EventStartDate { get; set; }

        [Required]
        public DateTime EventEndDate { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? EventState { get; set; }

        [Required]
        public string? EventPhotos { get; set; }

        public string? Note { get; set; }

    } //1

    public class CeremonyType //18
    {
        public long Id { get; set; }

        [Required]
        public long CreatedUserId { get; set; }

        [Required]
        public long UpdatedUserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Icono { get; set; }

        public string? PhotoIcono { get; set; }
    }
    public class EventCeremonyType
    {
        public long Id { get; set; }

        [Required]
        public long UpdatedUserId { get; set; }

        [Required]
        public long EventLocationId { get; set; }

        [Required]
        public long EventLocationTypeId { get; set; }

        [Required]
        public long LocationContactId { get; set; }

        [Required]
        public decimal ReservationPrice { get; set; }

        [Required]
        public DateTime EventCeremonyStartDate { get; set; }

        [Required]
        public DateTime EventCeremonyEndDate { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public long CeremonyTypeId { get; set; }

        [Required]
        public long CreatedUserId { get; set; }

        [Required]
        public long CurrencyId { get; set; }

        [Required]
        public long EventId { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Note { get; set; }

        public string? PhotoEventCeremony { get; set; }

    }//2


    public class EventCeremonyTypeEmployeeResponsableOfEvent
    {
        public long Id { get; set; }

        [Required]
        public long EventCeremonyTypeId { get; set; }

        [Required]
        public long VendorEmployeeId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public long CreatedUserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public long UpdatedUserId { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }
    }//3


    public class EventCeremonyTypeGuestResponsableOfEventCeremony
    {
        public long Id { get; set; }

        [Required]
        public long EventCeremonyTypeId { get; set; }

        [Required]
        public long GuestRoleEventId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public long CreatedUserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public long UpdatedUserId { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }
    }//4

    public class EventCeremonyTypeRequirementType
    {
        public long Id { get; set; }

        [Required]
        public long EventCeremonyTypeId { get; set; }

        [Required]
        public long RequirementTypeId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public long CreatedUserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public long UpdatedUserId { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }
    }//5

    public class EventCeremonyTypeRequirements
    {
        public long Id { get; set; }

        [Required]
        public long EventCeremonyTypeId { get; set; }

        [Required]
        public long RequirementId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public long CreatedUserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public long UpdatedUserId { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }
    }//6

    public class EventEmployeeResponsableOfEvent
    {
        public long Id { get; set; }

        [Required]
        public long EventId { get; set; }

        [Required]
        public long VendorEmployeeId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public long CreatedUserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public long UpdatedUserId { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }
    }//7

    public class EventEventType
    {
        public long Id { get; set; }

        [Required]
        public long EventId { get; set; }

        [Required]
        public long EventTypeId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public long CreatedUserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public long UpdatedUserId { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }
    }//8

    public class EventGuestResponsableOfEvent
    {
        public long Id { get; set; }

        [Required]
        public long EventId { get; set; }

        [Required]
        public long GuestRoleEventId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public long CreatedUserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public long UpdatedUserId { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }
    }//9

    public class EventType
    {
        public long Id { get; set; }

        [Required]
        public long CreatedUserId { get; set; }

        [Required]
        public long UpdatedUserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }
    }//10


    public class GuestRoleEvent
        {
            public long Id { get; set; }

            [Required]
            public long CreatedUserId { get; set; }

            [Required]
            public long UpdatedUserId { get; set; }

            [Required]
            public DateTime CreatedDate { get; set; }

            [Required]
            public DateTime UpdatedDate { get; set; }

            [Required]
            public bool IsActive { get; set; }

            [Required]
            public string? Name { get; set; }

            public string? Description { get; set; }

            public string? Icono { get; set; }

            public string? PhotoIcono { get; set; }
        }//11


    public class EventMoment
    {
        public long Id { get; set; }

        [Required]
        public long CreatedUserId { get; set; }

        [Required]
        public long UpdatedUserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Icono { get; set; }

        public string? PhotoIcono { get; set; }
    }//12

    public class Requirement
    {
        public long Id { get; set; }

        [Required]
        public long CreatedUserId { get; set; }

        [Required]
        public long UpdatedUserId { get; set; }

        [Required]
        public long CurrencyId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }
    }//13

    public class RequirementRequirementType
    {
        public long Id { get; set; }

        [Required]
        public long RequirementId { get; set; }

        [Required]
        public long RequirementTypeId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public long CreatedUserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public long UpdatedUserId { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }
    }//14

    public class RequirementType
    {
        public long Id { get; set; }

        [Required]
        public long CreatedUserId { get; set; }

        [Required]
        public long UpdatedUserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }
    }//15

    public class EventTask
    {
        public long Id { get; set; }

        [Required]
        public long CreatedUserId { get; set; }

        [Required]
        public long UpdatedUserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Icono { get; set; }

        public string? PhotoIcono { get; set; }
    }//16

    public class EventTaskResponsableOfTask
    {
        public long Id { get; set; }

        [Required]
        public long TaskId { get; set; }

        [Required]
        public long GuestRoleEventId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public long CreatedUserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public long UpdatedUserId { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }
    }//17

    public class AuditLog
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string EntityName { get; set; } = string.Empty;

        [Required]
        public long RecordId { get; set; }

        [Required]
        public string Action { get; set; } = string.Empty;

        [Required]
        public long UserId { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        public string? Changes { get; set; }
    }

}
