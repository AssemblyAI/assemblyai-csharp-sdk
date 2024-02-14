namespace AssemblyAI;

public class EntityType
{
    public static readonly EntityType Occupation = new EntityType(Value.Occupation, "occupation");

    public static readonly EntityType DateOfBirch = new EntityType(Value.DateOfBirth, "date_of_birth");

    public static readonly EntityType MedicalCondition = new EntityType(Value.MedicalCondition, "medical_condition");

    public static readonly EntityType PoliticalAffiliation = new EntityType(Value.PoliticalAffiliation, "political_affiliation");

    public static readonly EntityType Location = new EntityType(Value.Location, "location");

    public static readonly EntityType DriversLicense = new EntityType(Value.DriversLicense, "drivers_license");

    public static readonly EntityType MedicalProcess = new EntityType(Value.MedicalProcess, "medical_process");

    public static readonly EntityType Language = new EntityType(Value.Language, "language");

    public static readonly EntityType Injury = new EntityType(Value.Injury, "injury");

    public static readonly EntityType PersonName = new EntityType(Value.PersonName, "person_name");

    public static readonly EntityType Drug = new EntityType(Value.Drug, "drug");

    public static readonly EntityType MoneyAmount = new EntityType(Value.MoneyAmount, "money_amount");

    public static readonly EntityType CreditCardExpiration = new EntityType(Value.CreditCardExpiration, "credit_card_expiration");

    public static readonly EntityType Event = new EntityType(Value.Event, "event");

    public static readonly EntityType Password = new EntityType(Value.Password, "password");

    public static readonly EntityType Nationality = new EntityType(Value.Nationality, "nationality");

    public static readonly EntityType Organization = new EntityType(Value.Organization, "organization");

    public static readonly EntityType Religion = new EntityType(Value.Religion, "religion");

    public static readonly EntityType CreditCardNumber = new EntityType(Value.CreditCardNumber, "credit_card_number");

    public static readonly EntityType Date = new EntityType(Value.Date, "date");

    public static readonly EntityType BloodType = new EntityType(Value.BloodType, "blood_type");

    public static readonly EntityType EmailAddress = new EntityType(Value.EmailAddress, "email_address");

    public static readonly EntityType PhoneNumber = new EntityType(Value.PhoneNumber, "phone_number");

    public static readonly EntityType Time = new EntityType(Value.Time, "time");

    public static readonly EntityType Url = new EntityType(Value.Url, "url");

    public static readonly EntityType PersonAge = new EntityType(Value.PersonAge, "person_age");

    public static readonly EntityType BankingInformation = new EntityType(Value.BankingInformation, "banking_information");

    public static readonly EntityType CreditCardCvv = new EntityType(Value.CreditCardCvv, "credit_card_cvv");

    public static readonly EntityType UsSocialSecurityNumber = new EntityType(Value.UsSocialSecurityNumber, "us_social_security_number");
    
    private readonly Value value;
    private readonly string str;

    private EntityType(Value value, string str)
    {
        this.value = value;
        this.str = str;
    }

    public enum Value
    {
        BankingInformation,
        BloodType,
        CreditCardCvv,
        CreditCardExpiration,
        CreditCardNumber,
        Date,
        DateOfBirth,
        DriversLicense,
        Drug,
        EmailAddress,
        Event,
        Injury,
        Language,
        Location,
        MedicalCondition,
        MedicalProcess,
        MoneyAmount,
        Nationality,
        Occupation,
        Organization,
        Password,
        PersonAge,
        PersonName,
        PhoneNumber,
        PoliticalAffiliation,
        Religion,
        Time,
        Url,
        UsSocialSecurityNumber,
        Unknown
    }
}

