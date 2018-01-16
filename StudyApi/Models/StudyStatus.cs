using System;

namespace StudyApi.Models
{
    public static class StudyStatus
    {
        public static readonly Guid CreatedGuid = new Guid("8af962c0-a437-46f8-ba39-53898764da60");
        public static readonly Guid ReviewedGuid = new Guid("0314c7e7-56f2-44f3-8967-3d5e0a005d31");
        public static readonly Guid ReadyGuid = new Guid("5e7104ea-c3bd-46b8-b7a0-e7d1bde7acd2");
        public static readonly Guid NotReadyGuid =  new Guid("8eda205f-ea18-49b6-b7ab-38b84c09b5c8");

        public static readonly string CreatedName = "Created";
        public static readonly string ReviewedName = "Reviewed";
        public static readonly string ReadyName = "Ready";
        public static readonly string NotReadyName = "NotReady";
    }
}