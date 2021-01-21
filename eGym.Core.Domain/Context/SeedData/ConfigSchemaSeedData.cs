namespace eGym.Core.Domain.Context.SeedData
{
    internal static class ConfigSchemaSeedData
    {
        public static Sport_Division[] Sport_Division = new Sport_Division[]
        {
            new Sport_Division() { SD_ID = 1, SD_Name = "StrawWeight", SD_Description = null },
            new Sport_Division() { SD_ID = 2, SD_Name = "FlyWeight", SD_Description = null },
            new Sport_Division() { SD_ID = 3, SD_Name = "BantamWeight", SD_Description = null },
            new Sport_Division() { SD_ID = 4, SD_Name = "FeatherWeight", SD_Description = null },
            new Sport_Division() { SD_ID = 5, SD_Name = "LightWeight", SD_Description = null },
            new Sport_Division() { SD_ID = 6, SD_Name = "WelterWeight", SD_Description = null },
            new Sport_Division() { SD_ID = 7, SD_Name = "MiddleWeight", SD_Description = null },
            new Sport_Division() { SD_ID = 8, SD_Name = "LightHeavyWeight", SD_Description = null },
            new Sport_Division() { SD_ID = 9, SD_Name = "HeavyWeight", SD_Description = null }
        };

        public static Sport_DivisionLocalized[] Sport_DivisionLocalized = new Sport_DivisionLocalized[]
        {
            new Sport_DivisionLocalized() { SDL_ID = 1, SDL_DivisionID = 1, SDL_Culture = "it", SDL_Name = "Paglia", SDL_Description = null, SDL_MinWeight = null, SDL_MaxWeight = 52.2, SDL_UnitOfMeasureID = EN_UnitOfMeasure.Kilograms.ID },
            new Sport_DivisionLocalized() { SDL_ID = 2, SDL_DivisionID = 2, SDL_Culture = "it", SDL_Name = "Mosca", SDL_Description = null, SDL_MinWeight = 52.2, SDL_MaxWeight = 56.7, SDL_UnitOfMeasureID = EN_UnitOfMeasure.Kilograms.ID },
            new Sport_DivisionLocalized() { SDL_ID = 3, SDL_DivisionID = 3, SDL_Culture = "it", SDL_Name = "Gallo", SDL_Description = null, SDL_MinWeight = 56.7, SDL_MaxWeight = 61.2, SDL_UnitOfMeasureID = EN_UnitOfMeasure.Kilograms.ID },
            new Sport_DivisionLocalized() { SDL_ID = 4, SDL_DivisionID = 4, SDL_Culture = "it", SDL_Name = "Piuma", SDL_Description = null, SDL_MinWeight = 61.2, SDL_MaxWeight = 65.8, SDL_UnitOfMeasureID = EN_UnitOfMeasure.Kilograms.ID },
            new Sport_DivisionLocalized() { SDL_ID = 5, SDL_DivisionID = 5, SDL_Culture = "it", SDL_Name = "Leggeri", SDL_Description = null, SDL_MinWeight = 65.8, SDL_MaxWeight = 70.3, SDL_UnitOfMeasureID = EN_UnitOfMeasure.Kilograms.ID },
            new Sport_DivisionLocalized() { SDL_ID = 6, SDL_DivisionID = 6, SDL_Culture = "it", SDL_Name = "Welter", SDL_Description = null, SDL_MinWeight = 70.3, SDL_MaxWeight = 77.1, SDL_UnitOfMeasureID = EN_UnitOfMeasure.Kilograms.ID },
            new Sport_DivisionLocalized() { SDL_ID = 7, SDL_DivisionID = 7, SDL_Culture = "it", SDL_Name = "Medi", SDL_Description = null, SDL_MinWeight = 77.1, SDL_MaxWeight = 83.9, SDL_UnitOfMeasureID = EN_UnitOfMeasure.Kilograms.ID },
            new Sport_DivisionLocalized() { SDL_ID = 8, SDL_DivisionID = 8, SDL_Culture = "it", SDL_Name = "Massimi leggeri", SDL_Description = null, SDL_MinWeight = 83.9, SDL_MaxWeight = 93, SDL_UnitOfMeasureID = EN_UnitOfMeasure.Kilograms.ID },
            new Sport_DivisionLocalized() { SDL_ID = 9, SDL_DivisionID = 9, SDL_Culture = "it", SDL_Name = "Massimi", SDL_Description = null, SDL_MinWeight = 93, SDL_MaxWeight = 120.2, SDL_UnitOfMeasureID = EN_UnitOfMeasure.Kilograms.ID }
        };

        public static Sport_Level[] Sport_Level = new Sport_Level[]
        {
            new Sport_Level() { SL_ID = 1, SL_Name = "Rookie" },
            new Sport_Level() { SL_ID = 2, SL_Name = "Amateur" },
            new Sport_Level() { SL_ID = 3, SL_Name = "SemiPro" },
            new Sport_Level() { SL_ID = 4, SL_Name = "Pro" },
            new Sport_Level() { SL_ID = 5, SL_Name = "Veteran" }
        };

        public static Sport_LevelLocalized[] Sport_LevelLocalized = new Sport_LevelLocalized[]
        {
            new Sport_LevelLocalized() { SLL_ID = 1, SLL_LevelID = 1, SLL_Culture = "it", SLL_Name = "Praticante amatoriale" },
            new Sport_LevelLocalized() { SLL_ID = 2, SLL_LevelID = 2, SLL_Culture = "it", SLL_Name = "Esordiente" },
            new Sport_LevelLocalized() { SLL_ID = 3, SLL_LevelID = 3, SLL_Culture = "it", SLL_Name = "SemiPro" },
            new Sport_LevelLocalized() { SLL_ID = 4, SLL_LevelID = 4, SLL_Culture = "it", SLL_Name = "Pro" },
            new Sport_LevelLocalized() { SLL_ID = 5, SLL_LevelID = 5, SLL_Culture = "it", SLL_Name = "Veterano" },
        };

        public static Sport_EventResult[] Sport_EventResult = new Sport_EventResult[]
        {
            new Sport_EventResult() { SER_ID = 1, SER_Name = "Defeat", SER_ValueForRanking = 0 },
            new Sport_EventResult() { SER_ID = 2, SER_Name = "Draw", SER_ValueForRanking = 0 },
            new Sport_EventResult() { SER_ID = 3, SER_Name = "Victory", SER_ValueForRanking = 1 }
        };

        public static Sport_EventResultLocalized[] Sport_EventResultLocalized = new Sport_EventResultLocalized[]
        {
            new Sport_EventResultLocalized() { SerL_ID = 1, SerL_EventResultID = 1, SerL_Culture = "it", SerL_Name = "Sconfitta" },
            new Sport_EventResultLocalized() { SerL_ID = 2, SerL_EventResultID = 2, SerL_Culture = "it", SerL_Name = "Pareggio" },
            new Sport_EventResultLocalized() { SerL_ID = 3, SerL_EventResultID = 3, SerL_Culture = "it", SerL_Name = "Vittoria" },
        };

        public static Sport_EventResultType[] Sport_EventResultType = new Sport_EventResultType[]
        {
            new Sport_EventResultType() { SERT_ID = 1, SERT_Name = "Unanimus decision" },
            new Sport_EventResultType() { SERT_ID = 2, SERT_Name = "Split decision" },
            new Sport_EventResultType() { SERT_ID = 3, SERT_Name = "TKO" },
            new Sport_EventResultType() { SERT_ID = 4, SERT_Name = "KO" },
            new Sport_EventResultType() { SERT_ID = 5, SERT_Name = "Submission" },
            new Sport_EventResultType() { SERT_ID = 6, SERT_Name = "Medical decision" }
        };

        public static Sport_EventResultTypeLocalized[] Sport_EventResultTypeLocalized = new Sport_EventResultTypeLocalized[]
        {
            new Sport_EventResultTypeLocalized() { SertL_ID = 1, SertL_EventResultTypeID = 1, SertL_Culture = "it", SertL_Name = "Decisione unanime" },
            new Sport_EventResultTypeLocalized() { SertL_ID = 2, SertL_EventResultTypeID = 2, SertL_Culture = "it", SertL_Name = "Split decision" },
            new Sport_EventResultTypeLocalized() { SertL_ID = 3, SertL_EventResultTypeID = 3, SertL_Culture = "it", SertL_Name = "KO tecnico" },
            new Sport_EventResultTypeLocalized() { SertL_ID = 4, SertL_EventResultTypeID = 4, SertL_Culture = "it", SertL_Name = "KO" },
            new Sport_EventResultTypeLocalized() { SertL_ID = 5, SertL_EventResultTypeID = 5, SertL_Culture = "it", SertL_Name = "Sottomissione" },
            new Sport_EventResultTypeLocalized() { SertL_ID = 6, SertL_EventResultTypeID = 6, SertL_Culture = "it", SertL_Name = "Decisione medica" },
        };
    }
}
