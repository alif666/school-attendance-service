namespace SchoolAttendance.Api.Models.Sections;

public record SectionResponse(
    Guid Id,
    Guid SchoolClassId,
    string Name
);