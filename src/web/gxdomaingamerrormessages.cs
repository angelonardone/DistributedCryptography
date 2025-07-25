using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
namespace GeneXus.Programs {
   public class gxdomaingamerrormessages
   {
      private static Hashtable domain = new Hashtable();
      private static Hashtable domainMap;
      static gxdomaingamerrormessages ()
      {
         domain[(long)1] = "GAM_ConnectionNotSpecified";
         domain[(long)2] = "GAM_RepositoryNotFound";
         domain[(long)3] = "GAM_AuthenticationTypeNotFound";
         domain[(long)4] = "GAM_AuthenticationFunctionNotFound";
         domain[(long)5] = "GAM_UserIdentificationNotValid";
         domain[(long)6] = "GAM_UserLoginIsNull";
         domain[(long)7] = "GAM_UserNotFound";
         domain[(long)8] = "GAM_UserUnknown";
         domain[(long)9] = "GAM_UserInactive";
         domain[(long)10] = "GAM_UserBlocked";
         domain[(long)11] = "GAM_UserPasswordIncorrect";
         domain[(long)12] = "GAM_WebServiceError";
         domain[(long)13] = "GAM_SessionExpired";
         domain[(long)14] = "GAM_SessionNotExist";
         domain[(long)15] = "GAM_SessionTypeNotValid";
         domain[(long)16] = "GAM_SessionError";
         domain[(long)18] = "GAM_UserLoginIncorrect";
         domain[(long)17] = "GAM_UserNotAuthorized";
         domain[(long)19] = "GAM_ConsumerChallengeNotValid";
         domain[(long)20] = "GAM_ConsumerModeNotValid";
         domain[(long)21] = "GAM_SessionFinished";
         domain[(long)22] = "GAM_RepositoryWithoutAnonymousUser";
         domain[(long)23] = "GAM_UserPasswordMustBeChanged";
         domain[(long)24] = "GAM_UserPasswordExpired";
         domain[(long)25] = "GAM_SessionLocked";
         domain[(long)26] = "GAM_UserCanNotChangePassword";
         domain[(long)27] = "GAM_UserNameTooShort";
         domain[(long)28] = "GAM_UserNameSpecialCharacterNotValid";
         domain[(long)30] = "GAM_ConnectionNotFound";
         domain[(long)31] = "GAM_ConnectionTypeNotFound";
         domain[(long)32] = "GAM_ConnectionLoginFailed";
         domain[(long)33] = "GAM_ConnectionUserPasswordFailedForUser";
         domain[(long)34] = "GAM_RemoteConnectionUserFailed";
         domain[(long)35] = "GAM_AuthenticationTypeNotValid";
         domain[(long)36] = "GAM_RememberUserTypeNotValid";
         domain[(long)37] = "GAM_WebServiceVersionNotFound";
         domain[(long)38] = "GAM_WebServiceUnsupportedVersion";
         domain[(long)39] = "GAM_ApplicationNotFound";
         domain[(long)40] = "GAM_WebserviceNotResponding";
         domain[(long)41] = "GAM_UserAnonymousError";
         domain[(long)42] = "GAM_BCMessage";
         domain[(long)43] = "GAM_UserPasswordKeyIncorrect";
         domain[(long)44] = "GAM_UserPasswordKeyExpired";
         domain[(long)45] = "GAM_UserPasswordAnswerIncorrect";
         domain[(long)46] = "GAM_NameSpaceCannotBeNull";
         domain[(long)47] = "GAM_DescriptionCannotNull";
         domain[(long)48] = "GAM_APIModeUnrecognized";
         domain[(long)49] = "GAM_UserNameAlreadyExist";
         domain[(long)50] = "GAM_UserAlreadyActivated";
         domain[(long)51] = "GAM_UserActivationKeyIncorrect";
         domain[(long)52] = "GAM_UserActivationKeyExpired";
         domain[(long)53] = "GAM_RepositoryNameSpaceNotValid";
         domain[(long)54] = "GAM_OpenIdError";
         domain[(long)55] = "GAM_OpenIdIdentityNotMatch";
         domain[(long)56] = "GAM_OpenIdModeError";
         domain[(long)57] = "GAM_OpenIdReturnError";
         domain[(long)59] = "GAM_OpenIdSignedError";
         domain[(long)60] = "GAM_MissingRequiredData";
         domain[(long)62] = "GAM_UserEmailSyntaxError";
         domain[(long)63] = "GAM_UserEmailRequired";
         domain[(long)64] = "GAM_UserPasswordRequired";
         domain[(long)65] = "GAM_UserFirstNameRequired";
         domain[(long)66] = "GAM_UserLastNameRequired";
         domain[(long)67] = "GAM_UserBirthdayRequired";
         domain[(long)68] = "GAM_UserGenderRequired";
         domain[(long)69] = "GAM_UserPhoneRequired";
         domain[(long)70] = "GAM_UserAddressRequired";
         domain[(long)71] = "GAM_UserCityRequired";
         domain[(long)72] = "GAM_UserStateRequired";
         domain[(long)73] = "GAM_UserCountryRequired";
         domain[(long)74] = "GAM_UserPostCodeRequired";
         domain[(long)75] = "GAM_UserLanguageRequired";
         domain[(long)76] = "GAM_UserTimeZoneRequired";
         domain[(long)77] = "GAM_UserPhotoRequired";
         domain[(long)78] = "GAM_UserEmailAlreadyExist";
         domain[(long)79] = "GAM_UserNameMustBeEntered";
         domain[(long)80] = "GAM_ApplicationParentNotValid";
         domain[(long)81] = "GAM_ApplicationParentNotExist";
         domain[(long)82] = "GAM_BaseAapplicationNotValid";
         domain[(long)83] = "GAM_BaseApplicationNotExist";
         domain[(long)84] = "GAM_UserActivationTypeIncorrect";
         domain[(long)85] = "GAM_SessionAlreadyExist";
         domain[(long)86] = "GAM_ErrorWhenSaveCookie";
         domain[(long)87] = "GAM_RepositoryNeedLocalAuthentication";
         domain[(long)88] = "GAM_ExternalAuthenticationServerInvalid";
         domain[(long)89] = "GAM_ExternalAuthenticationTokenInvalid";
         domain[(long)90] = "GAM_ExternalAuthenticationError";
         domain[(long)91] = "GAM_FacebookAccessError";
         domain[(long)92] = "GAM_FacebookResponseError";
         domain[(long)93] = "GAM_ErrorExternalAuthenticationCallback";
         domain[(long)94] = "GAM_TwitterResponseError";
         domain[(long)95] = "GAM_TwitterTokenError";
         domain[(long)96] = "GAM_TwitterVerifierError";
         domain[(long)97] = "GAM_TwitterAccessError";
         domain[(long)98] = "GAM_UserGUIDErrorTryingLogin";
         domain[(long)99] = "GAM_AuthenticationTypeAlreadyDefined";
         domain[(long)100] = "GAM_OauthProtocolNotEnabled";
         domain[(long)101] = "GAM_RefreshTokenError";
         domain[(long)102] = "GAM_TokenRevoked";
         domain[(long)103] = "GAM_TokenExpired";
         domain[(long)104] = "GAM_UserMustBeAuthenticated";
         domain[(long)106] = "GAM_AccessGrantNotFound";
         domain[(long)107] = "GAM_AccessGrantRevoked";
         domain[(long)108] = "GAM_AuthorizationRequestNotFound";
         domain[(long)109] = "GAM_AuthorizationRequestRevoked";
         domain[(long)110] = "GAM_AuthorizationRequestNotAuthorized";
         domain[(long)111] = "GAM_ApplicationClientRevoked";
         domain[(long)112] = "GAM_AccessTokenNotFound";
         domain[(long)113] = "GAM_AccessTokenRevoked";
         domain[(long)114] = "GAM_AccessTokenNotValid";
         domain[(long)115] = "GAM_RepositoryAlreadyExist";
         domain[(long)116] = "GAM_ApplicationSecretNotValid";
         domain[(long)117] = "GAM_AccessGrantScopeNotValid";
         domain[(long)118] = "GAM_UserNameRequired";
         domain[(long)119] = "GAM_SessionChangeOriginalIP";
         domain[(long)120] = "GAM_UserEmailDuplicated";
         domain[(long)121] = "GAM_SessionUserError";
         domain[(long)122] = "GAM_SessionTokenError";
         domain[(long)123] = "GAM_ApplicationTokenNotFound";
         domain[(long)124] = "GAM_ApplicationTokenAlreadyExist";
         domain[(long)125] = "GAM_UserExternalIdAlreadyExist";
         domain[(long)126] = "GAM_RoleExternalIdAlreadyExist";
         domain[(long)127] = "GAM_CanNotChangeCurrentConnection";
         domain[(long)128] = "GAM_CanNotDeleteCurrentConnection";
         domain[(long)129] = "GAM_FacebookAccessTokenError";
         domain[(long)130] = "GAM_FacebookAccountUnverified";
         domain[(long)131] = "GAM_FacebookResponseMessage";
         domain[(long)132] = "GAM_UserDoesNotInRepository";
         domain[(long)133] = "GAM_ApplicationEnvironmentNotFound";
         domain[(long)134] = "GAM_AccessGrantExpired";
         domain[(long)135] = "GAM_GrantTokenExpired";
         domain[(long)136] = "GAM_GrantTokenNotFound";
         domain[(long)137] = "GAM_UserAttributeNotFound";
         domain[(long)138] = "GAM_ApplicationAlreadyExist";
         domain[(long)139] = "GAM_UnauthorizedAccessDenied";
         domain[(long)140] = "GAM_ApplicationNameCannotBeNull";
         domain[(long)141] = "GAM_PasswordTooShort";
         domain[(long)142] = "GAM_PasswordMustBeNumbers";
         domain[(long)143] = "GAM_PasswordMustBeUpperLetters";
         domain[(long)144] = "GAM_PasswordMustBeSpecialCharacters";
         domain[(long)145] = "GAM_UserPasswordNewDiferentOld";
         domain[(long)146] = "GAM_PasswordWasRrecentlyUsed";
         domain[(long)147] = "GAM_UserEmailRequiredToLogon";
         domain[(long)148] = "GAM_UserEmailRequiredToUnique";
         domain[(long)149] = "GAM_UserEmailMustUniqueToLogon";
         domain[(long)150] = "GAM_DirectoryNotExist";
         domain[(long)151] = "GAM_TokenNotRelatedToApplication";
         domain[(long)152] = "GAM_PermissionNameNotValid";
         domain[(long)153] = "GAM_PermissionNotFound";
         domain[(long)154] = "GAM_PermissionNameAlreadyExist";
         domain[(long)160] = "GAM_UserPasswordRecentlyChanged";
         domain[(long)161] = "GAM_UserMissingRequiredData";
         domain[(long)162] = "GAM_UserKeyToCompleteDataNotFound";
         domain[(long)163] = "GAM_UserKeyToCompleteDataIncorrect";
         domain[(long)164] = "GAM_UserRecoveryPasswordKeyTimeOutCanNotNull";
         domain[(long)166] = "GAM_TwitterAccountUnverified";
         domain[(long)167] = "GAM_DeviceIdCoundNotGet";
         domain[(long)168] = "GAM_TokenRenewError";
         domain[(long)169] = "GAM_ApplicationConfigurationErrorAanomymousUser";
         domain[(long)170] = "GAM_NameSpaceRestricted";
         domain[(long)171] = "GAM_RepeatedConnectionNameInFile";
         domain[(long)172] = "GAM_CanNotDeleteAdministratorRepository";
         domain[(long)173] = "GAM_InvalidTokenToFinish";
         domain[(long)174] = "GAM_ApplicationGUIDUnidentified";
         domain[(long)175] = "GAM_SecurityPolicyNameCannotBeNull";
         domain[(long)176] = "GAM_RoleNameCannotBeNull";
         domain[(long)177] = "GAM_RoleNameAlreadyExists";
         domain[(long)178] = "GAM_RepositoryNameCannotBeNull";
         domain[(long)179] = "GAM_ConnectionNameCannotBeNull";
         domain[(long)180] = "GAM_ConenctionUserNameCannotBeNull";
         domain[(long)182] = "GAM_ConnectionUserPasswordRequired";
         domain[(long)183] = "GAM_CanNotDeleteDefaultRole";
         domain[(long)184] = "GAM_CanNotDeleteSmartDeviceAnonymousRole";
         domain[(long)185] = "GAM_InvalidOperationOnDeviceUser";
         domain[(long)200] = "GAM_RegisterNotFound";
         domain[(long)201] = "GAM_PermissionAccessTypeNotValid";
         domain[(long)202] = "GAM_GoogleAccessError";
         domain[(long)203] = "GAM_GoogleResponseError";
         domain[(long)204] = "GAM_GoogleAccessTokenError";
         domain[(long)205] = "GAM_GoogleResponseMessages";
         domain[(long)206] = "GAM_GoogleAccountUnverified";
         domain[(long)207] = "GAM_InvalidImportDataDirectory";
         domain[(long)208] = "GAM_MissingDefaultSecurityPolicy";
         domain[(long)209] = "GAM_TwitterAccessTokenError";
         domain[(long)211] = "GAM_DelegateAuthorizationVersionError";
         domain[(long)212] = "GAM_DelegateAuthorizationServiceError";
         domain[(long)213] = "GAM_ExternalTokenInvalid";
         domain[(long)220] = "GAM_ApplicationClientLocalLoginURLCannotBeNull";
         domain[(long)221] = "GAM_ApplicationClientCallbackURLCannotBeNull";
         domain[(long)222] = "GAM_ApplicationClientCallbackURLNotValid";
         domain[(long)230] = "GAM_ApplicationClientNotAllowRemoteAuthentication";
         domain[(long)231] = "GAM_ApplicationClientEncryptionKeyError";
         domain[(long)232] = "GAM_ApplicationClientMissingScopeRequestUserData";
         domain[(long)233] = "GAM_ApplicationClientNotHaveScopeUserAdditionalData";
         domain[(long)234] = "GAM_ApplicationClientNotHaveScopeUserRoles";
         domain[(long)235] = "GAM_ApplicationClientReceivedInvalidScope";
         domain[(long)236] = "GAM_ApplicationClientNotFound";
         domain[(long)237] = "GAM_ApplicationMenuNotFound";
         domain[(long)238] = "GAM_ApplicationMenuNameNull";
         domain[(long)239] = "GAM_ApplicationMenuOptionNameNull";
         domain[(long)240] = "GAM_AuthenticationTypeNotEnabled";
         domain[(long)241] = "GAM_AuthenticationTypeNameCannotBeNull";
         domain[(long)242] = "GAM_AuthenticationTypeToImpersonateNotValid";
         domain[(long)243] = "GAM_AuthenticationTypeClientIdCannotBeNull";
         domain[(long)244] = "GAM_AuthenticationTypeClientSecretCannotBeNull";
         domain[(long)245] = "GAM_AuthenticationTypeSiteURLCannotBeNull";
         domain[(long)246] = "GAM_AuthenticationTypeRemoteServerURLCannotBeNull";
         domain[(long)247] = "GAM_AuthenticationTypeWebServiceNameCannotBeNull";
         domain[(long)248] = "GAM_AuthenticationTypeServerNameCannotBeNull";
         domain[(long)249] = "GAM_AuthenticationTypeConsumerKeyCannotBeNull";
         domain[(long)250] = "GAM_AuthenticationTypeConsumerSecretCannotBeNull";
         domain[(long)251] = "GAM_AuthenticationTypeCallbackURLCannotBeNull";
         domain[(long)252] = "GAM_AuthenticationTypeFileNameCannotBeNull";
         domain[(long)253] = "GAM_AuthenticationTypeClassNameCannotBeNull";
         domain[(long)260] = "GAM_AuthenticationTypeIsDefinedUsers";
         domain[(long)261] = "GAM_AuthenticationTypeIsUsedByDefault";
         domain[(long)270] = "GAM_GAMRemoteAccessError";
         domain[(long)271] = "GAM_GAMRemoteResponseError";
         domain[(long)272] = "GAM_GAMRemoteAccessTokenError";
         domain[(long)273] = "GAM_GAMRemoteResponseMessages";
         domain[(long)274] = "GAM_GAMRemoteAccountUnverified";
         domain[(long)290] = "GAM_ScopeNotFound";
         domain[(long)291] = "GAM_AccessCodeNotFound";
         domain[(long)292] = "GAMGrantTypeNotFound";
         domain[(long)300] = "GAM_UserNamespaceNotValid";
         domain[(long)301] = "GAM_UserAlreadyExistWithDifferentNamespace";
         domain[(long)302] = "GAM_UserAlreadyExistWithDifferentAuthenticationType";
         domain[(long)303] = "GAM_UserAttributeMultivaluedNotFound";
         domain[(long)304] = "GAM_UserAttributeMustBeMultivalued";
         domain[(long)305] = "GAM_UserAttributeIdIsRequired";
         domain[(long)320] = "GAM_EventSubscriptionEntityNull";
         domain[(long)321] = "GAM_EventSubscriptionActionNull";
         domain[(long)322] = "GAM_EventSubscriptionClassNameNull";
         domain[(long)323] = "GAM_EventSubscriprionFileNameNull";
         domain[(long)324] = "GAM_EventSubscriptionAlreadyExists";
         domain[(long)325] = "GAM_EventSubscriptionNotExists";
      }

      public static string getDescription( IGxContext context ,
                                           long key )
      {
         string value;
         value = (string)(domain[key]==null?"":domain[key]);
         return value ;
      }

      public static GxSimpleCollection<long> getValues( )
      {
         GxSimpleCollection<long> value = new GxSimpleCollection<long>();
         ArrayList aKeys = new ArrayList(domain.Keys);
         aKeys.Sort();
         foreach (long key in aKeys)
         {
            value.Add(key);
         }
         return value;
      }

      public static long getValue( string key )
      {
         if(domainMap == null)
         {
            domainMap = new Hashtable();
            domainMap["ConnectionNotSpecified"] = (long)1;
            domainMap["RepositoryNotFound"] = (long)2;
            domainMap["AuthenticationTypeNotFound"] = (long)3;
            domainMap["AuthenticationFunctionNotFound"] = (long)4;
            domainMap["UserIdentificationNotValid"] = (long)5;
            domainMap["UserLoginIsNull"] = (long)6;
            domainMap["UserNotFound"] = (long)7;
            domainMap["UserUnknown"] = (long)8;
            domainMap["UserInactive"] = (long)9;
            domainMap["UserBlocked"] = (long)10;
            domainMap["UserPasswordIncorrect"] = (long)11;
            domainMap["WebServiceError"] = (long)12;
            domainMap["SessionExpired"] = (long)13;
            domainMap["SessionNotExist"] = (long)14;
            domainMap["SessionTypeNotValid"] = (long)15;
            domainMap["SessionError"] = (long)16;
            domainMap["UserLoginIncorrect"] = (long)18;
            domainMap["UserNotAuthorized"] = (long)17;
            domainMap["ConsumerChallengeNotValid"] = (long)19;
            domainMap["ConsumerModeNotValid"] = (long)20;
            domainMap["SessionFinished"] = (long)21;
            domainMap["RepositoryWithoutAnonymousUser"] = (long)22;
            domainMap["UserPasswordMustBeChanged"] = (long)23;
            domainMap["UserPasswordExpired"] = (long)24;
            domainMap["SessionLocked"] = (long)25;
            domainMap["UserCanNotChangePassword"] = (long)26;
            domainMap["UserNameTooShort"] = (long)27;
            domainMap["UserNameNotSpecialChar"] = (long)28;
            domainMap["ConnectionNotFound"] = (long)30;
            domainMap["ConnectionTypeNotFound"] = (long)31;
            domainMap["ConnectionLoginFailed"] = (long)32;
            domainMap["ConnectionPasswordFailed"] = (long)33;
            domainMap["ConnectionRemoteFailed"] = (long)34;
            domainMap["AuthenticationTypeNotValid"] = (long)35;
            domainMap["RememberUserTypeNotValid"] = (long)36;
            domainMap["WebServiceVersionNotFound"] = (long)37;
            domainMap["WebServiceUnsupportedVersion"] = (long)38;
            domainMap["ApplicationNotFound"] = (long)39;
            domainMap["ErrorWSResponse"] = (long)40;
            domainMap["UserAnonymousError"] = (long)41;
            domainMap["BCMessage"] = (long)42;
            domainMap["UserPasswordKeyIncorrect"] = (long)43;
            domainMap["UserPasswordKeyExpired"] = (long)44;
            domainMap["UserPasswordAnswerIncorrect"] = (long)45;
            domainMap["NameSpaceCannotNull"] = (long)46;
            domainMap["DescriptionCannotNull"] = (long)47;
            domainMap["APIModeUnrecognized"] = (long)48;
            domainMap["UserNameAlreadyExist"] = (long)49;
            domainMap["UserAlreadyActivated"] = (long)50;
            domainMap["UserActivationKeyIncorrect"] = (long)51;
            domainMap["UserActivationKeyExpired"] = (long)52;
            domainMap["RepositoryNameSpaceNotValid"] = (long)53;
            domainMap["OpenIdError"] = (long)54;
            domainMap["OpenIdIdentityDoesntMatch"] = (long)55;
            domainMap["OpenIdModeError"] = (long)56;
            domainMap["OpenIdReturnToError"] = (long)57;
            domainMap["OpenIdSignedError"] = (long)59;
            domainMap["MissingRequiredData"] = (long)60;
            domainMap["UserEmailSyntaxError"] = (long)62;
            domainMap["UserEmailRequired"] = (long)63;
            domainMap["UserPasswordRequired"] = (long)64;
            domainMap["UserFirstNameRequired"] = (long)65;
            domainMap["UserLastNameRequired"] = (long)66;
            domainMap["UserBirthdayRequired"] = (long)67;
            domainMap["UserGenderRequired"] = (long)68;
            domainMap["UserPhoneRequired"] = (long)69;
            domainMap["UserAddressRequired"] = (long)70;
            domainMap["UserCityRequired"] = (long)71;
            domainMap["UserStateRequired"] = (long)72;
            domainMap["UserCountryRequired"] = (long)73;
            domainMap["UserPostCodeRequired"] = (long)74;
            domainMap["UserLanguageRequired"] = (long)75;
            domainMap["UserTimeZoneRequired"] = (long)76;
            domainMap["UserPhotoRequired"] = (long)77;
            domainMap["UserEmailAlreadyExist"] = (long)78;
            domainMap["UserNameMustBeEntered"] = (long)79;
            domainMap["ApplicationParentNotValid"] = (long)80;
            domainMap["ApplicationParentNotExist"] = (long)81;
            domainMap["ApplicationBaseNotValid"] = (long)82;
            domainMap["ApplicationBaseNotExist"] = (long)83;
            domainMap["UserActivationTypeIncorrect"] = (long)84;
            domainMap["SessionAlreadyExist"] = (long)85;
            domainMap["ErrorSetCookie"] = (long)86;
            domainMap["RepositoryNeedLocalAuthentication"] = (long)87;
            domainMap["ExternalAuthenticationServerInvalid"] = (long)88;
            domainMap["ExternalAuthenticationTokenInvalid"] = (long)89;
            domainMap["ExternalAuthenticationError"] = (long)90;
            domainMap["FacebookAccessError"] = (long)91;
            domainMap["FacebookResponseError"] = (long)92;
            domainMap["ErrorExternalAuthenticationCallback"] = (long)93;
            domainMap["TwitterResponseError"] = (long)94;
            domainMap["TwitterTokenError"] = (long)95;
            domainMap["TwitterVerifierError"] = (long)96;
            domainMap["TwitterAccessError"] = (long)97;
            domainMap["UserGUIDErrorTryingLogin"] = (long)98;
            domainMap["AuthenticationTypeAlreadyDefined"] = (long)99;
            domainMap["OauthProtocolNotEnabled"] = (long)100;
            domainMap["RefreshTokenError"] = (long)101;
            domainMap["SessionTokenRevoked"] = (long)102;
            domainMap["SessionTokenExpired"] = (long)103;
            domainMap["UserMustBeAuthenticated"] = (long)104;
            domainMap["AccessGrantNotFound"] = (long)106;
            domainMap["AccessGrantRevoked"] = (long)107;
            domainMap["AuthorizationRequestNotFound"] = (long)108;
            domainMap["AuthorizationRequestRevoked"] = (long)109;
            domainMap["AuthorizationRequestNotAuthorized"] = (long)110;
            domainMap["ApplicationClientRevoked"] = (long)111;
            domainMap["AccessTokenNotFound"] = (long)112;
            domainMap["AccessTokenRevoked"] = (long)113;
            domainMap["AccessTokenNotValid"] = (long)114;
            domainMap["RepositoryAlreadyExist"] = (long)115;
            domainMap["ApplicationSecretNotValid"] = (long)116;
            domainMap["AccessGrantScopeNotValid"] = (long)117;
            domainMap["UserNameRequired"] = (long)118;
            domainMap["SessionChangeOriginalIP"] = (long)119;
            domainMap["UserEmailDuplicated"] = (long)120;
            domainMap["SessionUserError"] = (long)121;
            domainMap["SessionTokenError"] = (long)122;
            domainMap["AppTokenNotFound"] = (long)123;
            domainMap["AppTokenAlreadyExist"] = (long)124;
            domainMap["UserExternalIdAlreadyExist"] = (long)125;
            domainMap["RoleExternalIdAlreadyExist"] = (long)126;
            domainMap["CanNotChangeCurrentConnection"] = (long)127;
            domainMap["CanNotDeleteCurrentConnection"] = (long)128;
            domainMap["FacebookAccessTokenError"] = (long)129;
            domainMap["FacebookAccountUnverified"] = (long)130;
            domainMap["FacebookResponse"] = (long)131;
            domainMap["UserDoesNotInRepository"] = (long)132;
            domainMap["ApplicationEnvironmentNotFound"] = (long)133;
            domainMap["AccessGrantExpired"] = (long)134;
            domainMap["GrantTokenExpired"] = (long)135;
            domainMap["GrantTokenNotFound"] = (long)136;
            domainMap["UserAttributeNotFound"] = (long)137;
            domainMap["ApplicationAlreadyExist"] = (long)138;
            domainMap["UnauthorizedError"] = (long)139;
            domainMap["ApplicationNameNull"] = (long)140;
            domainMap["UserPasswordIsTooShort"] = (long)141;
            domainMap["UserPasswordMustBeNumbers"] = (long)142;
            domainMap["UserPasswordMustBeUpperLetter"] = (long)143;
            domainMap["UserPasswordMustBeSpecialChar"] = (long)144;
            domainMap["UserPasswordNewDiferentOld"] = (long)145;
            domainMap["UserPasswordWasRecentlyUsed"] = (long)146;
            domainMap["UserEmailRequiredToLogon"] = (long)147;
            domainMap["UserEmailRequiredToUnique"] = (long)148;
            domainMap["UserEmailMustUniqueToLogon"] = (long)149;
            domainMap["DirectoryNotExist"] = (long)150;
            domainMap["AccessTokenNotRelatedToApplication"] = (long)151;
            domainMap["PermissionNameNotValid"] = (long)152;
            domainMap["PermissionNotFound"] = (long)153;
            domainMap["PermissionNameAlreadyExist"] = (long)154;
            domainMap["UserPasswordRecentlyChanged"] = (long)160;
            domainMap["UserMissingRequiredData"] = (long)161;
            domainMap["UserKeyToCompleteDataNotFound"] = (long)162;
            domainMap["UserKeyToCompleteDataIncorrect"] = (long)163;
            domainMap["UserRecoveryPasswordKeyTimeOutCanNotNull"] = (long)164;
            domainMap["TwitterAccountUnverified"] = (long)166;
            domainMap["DeviceIdCoundNotGet"] = (long)167;
            domainMap["TokenRenewError"] = (long)168;
            domainMap["ApplicationNotSetPropertyAutoRegisterDevice"] = (long)169;
            domainMap["NameSpaceRestricted"] = (long)170;
            domainMap["RepeatedConnectionNameInFile"] = (long)171;
            domainMap["CanNotDeleteAdministratorRepository"] = (long)172;
            domainMap["InvalidTokenToFinish"] = (long)173;
            domainMap["ApplicationUnidentified"] = (long)174;
            domainMap["SecurityPolicyNameNull"] = (long)175;
            domainMap["RoleNameNull"] = (long)176;
            domainMap["RoleNameAlreadyExists"] = (long)177;
            domainMap["RepositoryNameNull"] = (long)178;
            domainMap["ConnectionNameNull"] = (long)179;
            domainMap["ConnectionUserNameNull"] = (long)180;
            domainMap["ConnectionUserPasswordRequired"] = (long)182;
            domainMap["CanNotDeleteDefaultRole"] = (long)183;
            domainMap["CanNotDeleteSmartDeviceAnonymousRole"] = (long)184;
            domainMap["InvalidOperationOnDeviceUser"] = (long)185;
            domainMap["RegisterNotFound"] = (long)200;
            domainMap["PermissionAccessTypeNotValid"] = (long)201;
            domainMap["GoogleAccessError"] = (long)202;
            domainMap["GoogleResponseError"] = (long)203;
            domainMap["GoogleAccessTokenError"] = (long)204;
            domainMap["GoogleResponse"] = (long)205;
            domainMap["GoogleAccountUnverified"] = (long)206;
            domainMap["InvalidImportDataDirectory"] = (long)207;
            domainMap["MissingDefaultSecurityPolicy"] = (long)208;
            domainMap["TwitterAccessTokenError"] = (long)209;
            domainMap["DelegateAuthorizationVersionError"] = (long)211;
            domainMap["DelegateAuthorizationServiceError"] = (long)212;
            domainMap["ExternalTokenInvalid"] = (long)213;
            domainMap["ApplicationClientLocalLoginURLCannotBeNull"] = (long)220;
            domainMap["ApplicationClientCallbackURLCannotBeNull"] = (long)221;
            domainMap["ApplicationClientCallbackURLNotValid"] = (long)222;
            domainMap["ApplicationClientNotAllowRemoteAuthentication"] = (long)230;
            domainMap["ApplicationClientEncryptionKeyError"] = (long)231;
            domainMap["ApplicationClientMissingScopeRequestUserData"] = (long)232;
            domainMap["ApplicationClientNotHaveScopeUserAdditionalData"] = (long)233;
            domainMap["ApplicationClientNotHaveScopeUserRoles"] = (long)234;
            domainMap["ApplicationClientReceivedInvalidScope"] = (long)235;
            domainMap["ApplicationClientNotFound"] = (long)236;
            domainMap["ApplicationMenuNotFound"] = (long)237;
            domainMap["ApplicationMenuNameNull"] = (long)238;
            domainMap["ApplicationMenuOptionNameNull"] = (long)239;
            domainMap["AuthenticationTypeNotEnabled"] = (long)240;
            domainMap["AuthenticationTypeNameCannotBeNull"] = (long)241;
            domainMap["AuthenticationTypeToImpersonateNotValid"] = (long)242;
            domainMap["AuthenticationTypeClientIdCannotBeNull"] = (long)243;
            domainMap["AuthenticationTypeClientSecretCannotBeNull"] = (long)244;
            domainMap["AuthenticationTypeSiteURLCannotBeNull"] = (long)245;
            domainMap["AuthenticationTypeRemoteServerURLCannotBeNull"] = (long)246;
            domainMap["AuthenticationTypeWebServiceNameCannotBeNull"] = (long)247;
            domainMap["AuthenticationTypeServerNameCannotBeNull"] = (long)248;
            domainMap["AuthenticationTypeConsumerKeyCannotBeNull"] = (long)249;
            domainMap["AuthenticationTypeConsumerSecretCannotBeNull"] = (long)250;
            domainMap["AuthenticationTypeCallbackURLCannotBeNull"] = (long)251;
            domainMap["AuthenticationTypeFileNameCannotBeNull"] = (long)252;
            domainMap["AuthenticationTypeClassNameCannotBeNull"] = (long)253;
            domainMap["AuthenticationTypeIsDefinedUsers"] = (long)260;
            domainMap["AuthenticationTypeIsUsedByDefault"] = (long)261;
            domainMap["GAMRemoteAccessError"] = (long)270;
            domainMap["GAMRemoteResponseError"] = (long)271;
            domainMap["GAMRemoteAccessTokenError"] = (long)272;
            domainMap["GAMRemoteResponseMessages"] = (long)273;
            domainMap["GAMRemoteAccountUnverified"] = (long)274;
            domainMap["ScopeNotFound"] = (long)290;
            domainMap["AccessCodeNotFound"] = (long)291;
            domainMap["GrantTypeNotFound"] = (long)292;
            domainMap["UserNamespaceNotValid"] = (long)300;
            domainMap["UserAlreadyExistWithDifferentNamespace"] = (long)301;
            domainMap["UserAlreadyExistWithDifferentAuthType"] = (long)302;
            domainMap["UserAttributeMultivaluedNotFound"] = (long)303;
            domainMap["UserAttributeMustBeMultivalued"] = (long)304;
            domainMap["UserAttributeIdIsRequired"] = (long)305;
            domainMap["EventSubscriptionEntityNull"] = (long)320;
            domainMap["EventSubscriptionActionNull"] = (long)321;
            domainMap["EventSubscriptionClassNameNull"] = (long)322;
            domainMap["EventSubscriprionFileNameNull"] = (long)323;
            domainMap["EventSubscriptionAlreadyExists"] = (long)324;
            domainMap["EventSubscriptionNotExists"] = (long)325;
         }
         return (long)domainMap[key] ;
      }

   }

}
