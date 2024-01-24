namespace AssemblyAI;

public class EntityType
{
    public static readonly EntityType OCCUPATION = new EntityType(Value.OCCUPATION, "occupation");

    public static readonly EntityType DATE_OF_BIRTH = new EntityType(Value.DATE_OF_BIRTH, "date_of_birth");

    public static readonly EntityType MEDICAL_CONDITION = new EntityType(Value.MEDICAL_CONDITION, "medical_condition");

    public static readonly EntityType POLITICAL_AFFILIATION = new EntityType(Value.POLITICAL_AFFILIATION, "political_affiliation");

    public static readonly EntityType LOCATION = new EntityType(Value.LOCATION, "location");

    public static readonly EntityType DRIVERS_LICENSE = new EntityType(Value.DRIVERS_LICENSE, "drivers_license");

    public static readonly EntityType MEDICAL_PROCESS = new EntityType(Value.MEDICAL_PROCESS, "medical_process");

    public static readonly EntityType LANGUAGE = new EntityType(Value.LANGUAGE, "language");

    public static readonly EntityType INJURY = new EntityType(Value.INJURY, "injury");

    public static readonly EntityType PERSON_NAME = new EntityType(Value.PERSON_NAME, "person_name");

    public static readonly EntityType DRUG = new EntityType(Value.DRUG, "drug");

    public static readonly EntityType MONEY_AMOUNT = new EntityType(Value.MONEY_AMOUNT, "money_amount");

    public static readonly EntityType CREDIT_CARD_EXPIRATION = new EntityType(Value.CREDIT_CARD_EXPIRATION, "credit_card_expiration");

    public static readonly EntityType EVENT = new EntityType(Value.EVENT, "event");

    public static readonly EntityType PASSWORD = new EntityType(Value.PASSWORD, "password");

    public static readonly EntityType NATIONALITY = new EntityType(Value.NATIONALITY, "nationality");

    public static readonly EntityType ORGANIZATION = new EntityType(Value.ORGANIZATION, "organization");

    public static readonly EntityType RELIGION = new EntityType(Value.RELIGION, "religion");

    public static readonly EntityType CREDIT_CARD_NUMBER = new EntityType(Value.CREDIT_CARD_NUMBER, "credit_card_number");

    public static readonly EntityType DATE = new EntityType(Value.DATE, "date");

    public static readonly EntityType BLOOD_TYPE = new EntityType(Value.BLOOD_TYPE, "blood_type");

    public static readonly EntityType EMAIL_ADDRESS = new EntityType(Value.EMAIL_ADDRESS, "email_address");

    public static readonly EntityType PHONE_NUMBER = new EntityType(Value.PHONE_NUMBER, "phone_number");

    public static readonly EntityType TIME = new EntityType(Value.TIME, "time");

    public static readonly EntityType URL = new EntityType(Value.URL, "url");

    public static readonly EntityType PERSON_AGE = new EntityType(Value.PERSON_AGE, "person_age");

    public static readonly EntityType BANKING_INFORMATION = new EntityType(Value.BANKING_INFORMATION, "banking_information");

    public static readonly EntityType CREDIT_CARD_CVV = new EntityType(Value.CREDIT_CARD_CVV, "credit_card_cvv");

    public static readonly EntityType US_SOCIAL_SECURITY_NUMBER = new EntityType(Value.US_SOCIAL_SECURITY_NUMBER, "us_social_security_number");

    
    private readonly Value value;
    private readonly string str;

    private EntityType(Value value, string str)
    {
        this.value = value;
        this.str = str;
    }

    public enum Value
    {
        BANKING_INFORMATION,
        BLOOD_TYPE,
        CREDIT_CARD_CVV,
        CREDIT_CARD_EXPIRATION,
        CREDIT_CARD_NUMBER,
        DATE,
        DATE_OF_BIRTH,
        DRIVERS_LICENSE,
        DRUG,
        EMAIL_ADDRESS,
        EVENT,
        INJURY,
        LANGUAGE,
        LOCATION,
        MEDICAL_CONDITION,
        MEDICAL_PROCESS,
        MONEY_AMOUNT,
        NATIONALITY,
        OCCUPATION,
        ORGANIZATION,
        PASSWORD,
        PERSON_AGE,
        PERSON_NAME,
        PHONE_NUMBER,
        POLITICAL_AFFILIATION,
        RELIGION,
        TIME,
        URL,
        US_SOCIAL_SECURITY_NUMBER,
        UNKNOWN
    }
}

