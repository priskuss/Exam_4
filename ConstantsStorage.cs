class Constant
{
    //API
    public const string Properties = "properties";
    public const string Timeseries = "timeseries";
    public const string Data = "data";
    public const string Instant = "instant";
    public const string Details = "details";
    public const string LogFile = "log.json";
    public const string BaseUrl = "https://api.met.no/weatherapi/nowcast/2.0/complete?lat=59.9333&lon=10.7166";
    public const string UserAgent = "MyTestApp/0.1";
    public const string AirTemperature = "air_temperature";
    public const string Json = "application/json";



    //UserInteraction

    public const string UserTemperature = "User temperature: {0}";
    public const string YRTemperature = "YR temperature: {0}";
    public const string UserHigher = "Your temperature is higher than YR temperature.";
    public const string UserLower = "Your temperature is lower than YR temperature.";
    public const string UserSame = "Your temperature is the same as YR temperature.";
    public const string TemperatureDifference = "Temperature difference: {0} degrees.";
    public const string InvalidInput = "Invalid input. Please enter a valid temperature.";
    public const string EnterTemperature = "Enter your temperature measurement: ";
    public const string Prediction = "Prediction: ";
    public const string DataIsAlreadySavedNoNewData = "Data for today has already been saved. You cannot enter new data.";
    public const string LuckyToGuess = "Feeling lucky? Guess the current temperature in Oslo for a prediction for today!";
    public const string SaveData = "Interested in seeing how your guess compares to reality? Save this data along with the actual weather? (yes/no)";
    public const string Note = "Note: It is impossible to enter data twice per one same day.";
    public const string Yes = "yes";
    public const string DataSaved = "Data has been saved.";



    //Predictions

    public const string Positive = "positive";
    public const string Funny = "funny";
    public const string Neutral = "neutral";
    public const string Warning = "warning";
    public const string PositivePredictionOne = "The stars are aligned! Today holds the promise of success and positive experiences.";
    public const string PositivePredictionTwo = "A chance encounter could spark a new opportunity. Be open to unexpected connections.";
    public const string PositivePredictionThree = "Your intuition seems particularly sharp today. Trust your gut feeling: give one random student 35/35!";
    public const string PositivePredictionFour = "The compiler gods are smiling upon you. Expect lightning-fast compile times and minimal errors.";
    public const string PositivePredictionFive = "That nagging bug that's been plaguing you will finally reveal itself in a glorious moment of clarity.";
    public const string PositivePredictionSix = "You'll discover a new library or framework that perfectly fits your project, saving you hours of development time.";
    public const string FunnyPredictionOne = "Warning: May contain a surge of productivity (or a serious case of the munchies).";
    public const string FunnyPredictionTwo = "Our highly scientific prediction system says you might win the lottery...or maybe just find some good coffee.";
    public const string FunnyPredictionThree = "Brace yourself for a day that's as unpredictable as your morning weather forecast!";
    public const string FunnyPredictionFour = "//This prediction seems legit (maybe)";
    public const string FunnyPredictionFive = "Your inbox will be empty for the entire day. Enjoy the blissful silence (until tomorrow).";
    public const string NeutralPredictionOne = "An interesting day unfolds! Buckle up for a mix of challenges and triumphs.";
    public const string NeutralPredictionTwo = "The day seems balanced - perfect for taking things at your own pace.";
    public const string NeutralPredictionThree = "Change is on the horizon! Embrace the unexpected and see where it leads.";
    public const string NeutralPredictionFour = "Stack Overflow will have the answer... eventually.";
    public const string NeutralPredictionFive = "Feeling stuck? Take a break and explain your code to a rubber duck. They're excellent listeners (and don't judge bad variable names).";
    public const string NeutralPredictionSix = "The coding gods are neutral today. Expect a steady stream of challenges and small victories.";
    public const string NeutralPredictionSeven = "Don't expect a coding marathon today. Take breaks to avoid burnout and maintain focus.";
    public const string WarningPredictionOne = "Beware of hidden obstacles. Patience and perseverance will be your allies today.";
    public const string WarningPredictionTwo = "A misunderstanding could arise. Choose your words carefully and practice active listening.";
    public const string WarningPredictionThree = "Don't be afraid to ask for help if you need it. A helping hand may be closer than you think.";
    public const string WarningPredictionFour = "If your name is Christian or Tony, you will have a great day, if you buy your friend a muffin. If not, proceed with caution.";
    public const string WarningPredictionFive = "Beware of angry students, do not open Discrod today.";
    public const string WarningPredictionSix = "The internet will be slow and unreliable. Local file backups are strongly advised.";
    public const string OpeningLine = "The winds whisper secrets... are you ready to hear a cryptic message for your day?";



    //InsteadOfMagicNumbers

    public const int NumberOne = 1;
    public const int NumberThree = 3;
    public const int NumberFive = 5;
    public const int NumberSeven = 7;
    public const int NumberTwentyNine = 29;
    public const int NumberHundred = 100;



    //Report

    public const string DailyReport = "1";
    public const string WeeklyReport = "2";
    public const string MonthlyReport = "3";
    public const string Exit = "4";
    public const string InvalidChoice = "Invalid choice. No report will be generated.";
    public const string NotEnoughDataWeekly = "Not enough data for weekly report";
    public const string NotEnoughDataMonthly = "Not enough data for monthly report";
    public const string TodayIs = "Today is {0}";
    public const string UserTemperatureActualTemperature = "User Temperature: {0}\nActual Temperature: {1}\nTemperature Difference: {2}";
    public const string AverageTemperatureReport = "Average User Temperature: {0}\nAverage YR Temperature: {1}\nAverage Temperature Difference: {2}";
    public const string DailyReportTitle = "Daily report:";
    public const string WeeklyReportTitle = "Weekly report:";
    public const string MonthlyReportTitle = "Monthly report:";
    public const string DoYouWantToSeeTheReport = "For a day report enter 1. For a week report enter 2. For a month report enter 3. To exit enter 4.";



    //ErrorHandling

    public const string Error = "Error: {0}, {1}";
    public const string ErrorHandlingAPI = "An error occurred while getting weather data: {0}";
    public const string ErrorReadingFile = "An error occurred while reading the file: {0}";
    public const string ErrorInteractingUser = "An error occurred while interacting with the user: {0}";
    public const string ErrorSavingData = "An error occurred while saving data: {0}";
    public const string ErrorGettingPrediction = "An error occurred while getting a prediction: {0}";
    public const string ErrorGeneratingReport = "An error occurred while generating the report: {0}";
    public const string ErrorSavingReport = "An error occurred while saving the report: {0}";
    public const string ErrorDisplayingDailyReport = "An error occurred while displaying daily report: {0}";
    public const string ErrorDisplayingWeeklyReport = "An error occurred while displaying weekly report: {0}";
    public const string ErrorDisplayingMonthlyReport = "An error occurred while displaying monthly report: {0}";
    public const string ErrorDisplayingDailyData = "An error occurred while displaying daily data: {0}";
    public const string ErrorDisplayingAverageData = "An error occurred while displaying average data: {0}";
    public const string ErrorReadingData = "An error occurred while reading data: {0}";



    //Tests

    public const string TestCalculateTemperatureDifferencePassed = ":) TestCalculateTemperatureDifference passed.";
    public const string TestCalculateTemperatureDifferenceFailed = ":( TestCalculateTemperatureDifference failed. Expected: {0}, but got: {1}";
    public const string TestCalculateTemperatureDifferenceWithNegativeValuesPassed = ":) TestCalculateTemperatureDifferenceWithNegativeValues passed.";
    public const string TestCalculateTemperatureDifferenceWithNegativeValuesFailed = ":( TestCalculateTemperatureDifference failed. Expected: {0}, but got: {1}";
    public const string TestCreateDataObjectPassed = ":) TestCreateDataObject passed.";
    public const string TestCreateDataObjectFailed = ":( TestCreateDataObject failed.";
}
