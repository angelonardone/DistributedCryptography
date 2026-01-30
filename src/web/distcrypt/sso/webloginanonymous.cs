using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using System.Data;
using GeneXus.Data;
using com.genexus;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.WebControls;
using GeneXus.Http;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
namespace GeneXus.Programs.distcrypt.sso {
   public class webloginanonymous : GXHttpHandler
   {
      public webloginanonymous( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public webloginanonymous( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_hexaParameters )
      {
         this.AV20hexaParameters = aP0_hexaParameters;
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      protected override void createObjects( )
      {
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( nGotPars == 0 )
         {
            entryPointCalled = false;
            gxfirstwebparm = GetFirstPar( "hexaParameters");
            gxfirstwebparm_bkp = gxfirstwebparm;
            gxfirstwebparm = DecryptAjaxCall( gxfirstwebparm);
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            if ( StringUtil.StrCmp(gxfirstwebparm, "dyncall") == 0 )
            {
               setAjaxCallMode();
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               dyncall( GetNextPar( )) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxEvt") == 0 )
            {
               setAjaxEventMode();
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetFirstPar( "hexaParameters");
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
            {
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetFirstPar( "hexaParameters");
            }
            else
            {
               if ( ! IsValidAjaxCall( false) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = gxfirstwebparm_bkp;
            }
            if ( ! entryPointCalled && ! ( isAjaxCallMode( ) || isFullAjaxMode( ) ) )
            {
               AV20hexaParameters = gxfirstwebparm;
               AssignAttri("", false, "AV20hexaParameters", AV20hexaParameters);
               GxWebStd.gx_hidden_field( context, "gxhash_vHEXAPARAMETERS", GetSecureSignedToken( "", AV20hexaParameters, context));
            }
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
         }
         if ( ! context.IsLocalStorageSupported( ) )
         {
            context.PushCurrentUrl();
         }
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            ValidateSpaRequest();
            PA142( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               WS142( ) ;
               if ( ! isAjaxCallMode( ) )
               {
                  WE142( ) ;
               }
            }
            if ( ( GxWebError == 0 ) && context.isAjaxRequest( ) )
            {
               enableOutput();
               if ( ! context.isAjaxRequest( ) )
               {
                  context.GX_webresponse.AppendHeader("Cache-Control", "no-store");
               }
               if ( ! context.WillRedirect( ) )
               {
                  AddString( context.getJSONResponse( )) ;
               }
               else
               {
                  if ( context.isAjaxRequest( ) )
                  {
                     disableOutput();
                  }
                  RenderHtmlHeaders( ) ;
                  context.Redirect( context.wjLoc );
                  context.DispatchAjaxCommands();
               }
            }
         }
         cleanup();
      }

      protected void RenderHtmlHeaders( )
      {
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, false);
      }

      protected void RenderHtmlOpenForm( )
      {
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.WriteHtmlText( "<title>") ;
         context.SendWebValue( "Web Login Anonymous") ;
         context.WriteHtmlTextNl( "</title>") ;
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         if ( StringUtil.Len( sDynURL) > 0 )
         {
            context.WriteHtmlText( "<BASE href=\""+sDynURL+"\" />") ;
         }
         define_styles( ) ;
         CloseStyles();
         if ( ( ( context.GetBrowserType( ) == 1 ) || ( context.GetBrowserType( ) == 5 ) ) && ( StringUtil.StrCmp(context.GetBrowserVersion( ), "7.0") == 0 ) )
         {
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 2351240), false, true, false);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 2351240), false, true, false);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 2351240), false, true, false);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true, false);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.CloseHtmlHeader();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         FormProcess = ((nGXWrapped==0) ? " data-HasEnter=\"false\" data-Skiponenter=\"false\"" : "");
         context.WriteHtmlText( "<body ") ;
         if ( StringUtil.StrCmp(context.GetLanguageProperty( "rtl"), "true") == 0 )
         {
            context.WriteHtmlText( " dir=\"rtl\" ") ;
         }
         bodyStyle = "";
         if ( nGXWrapped == 0 )
         {
            bodyStyle += "-moz-opacity:0;opacity:0;";
         }
         context.WriteHtmlText( " "+"gxversion=\"15.0.0\""+" "+"class=\"Form\""+" "+ "style='"+bodyStyle+"'") ;
         context.WriteHtmlText( FormProcess+">") ;
         context.skipLines(1);
         if ( nGXWrapped != 1 )
         {
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"Form\" data-gx-class=\"Form\" novalidate action=\""+formatLink("distcrypt.sso.webloginanonymous", new object[] {UrlEncode(StringUtil.RTrim(AV20hexaParameters))}, new string[] {"hexaParameters"}) +"\">") ;
            GxWebStd.gx_hidden_field( context, "_EventName", "");
            GxWebStd.gx_hidden_field( context, "_EventGridId", "");
            GxWebStd.gx_hidden_field( context, "_EventRowId", "");
            context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
            AssignProp("", false, "FORM", "Class", "Form", true);
         }
         toggleJsOutput = isJsOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         GxWebStd.gx_hidden_field( context, "vHEXAPARAMETERS", AV20hexaParameters);
         GxWebStd.gx_hidden_field( context, "gxhash_vHEXAPARAMETERS", GetSecureSignedToken( "", AV20hexaParameters, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "vHEXAPARAMETERS", AV20hexaParameters);
         GxWebStd.gx_hidden_field( context, "gxhash_vHEXAPARAMETERS", GetSecureSignedToken( "", AV20hexaParameters, context));
      }

      protected void RenderHtmlCloseForm142( )
      {
         SendCloseFormHiddens( ) ;
         GxWebStd.gx_hidden_field( context, "GX_FocusControl", GX_FocusControl);
         SendAjaxEncryptionKey();
         SendSecurityToken((string)(sPrefix));
         SendComponentObjects();
         SendServerCommands();
         SendState();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         if ( nGXWrapped != 1 )
         {
            context.WriteHtmlTextNl( "</form>") ;
         }
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         include_jscripts( ) ;
         context.WriteHtmlTextNl( "</body>") ;
         context.WriteHtmlTextNl( "</html>") ;
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
      }

      public override string GetPgmname( )
      {
         return "DistCrypt.SSO.WebLoginAnonymous" ;
      }

      public override string GetPgmdesc( )
      {
         return "Web Login Anonymous" ;
      }

      protected void WB140( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            RenderHtmlHeaders( ) ;
            RenderHtmlOpenForm( ) ;
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", "", "false");
         }
         wbLoad = true;
      }

      protected void START142( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_14-187820", 0) ;
            }
         }
         Form.Meta.addItem("description", "Web Login Anonymous", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP140( ) ;
      }

      protected void WS142( )
      {
         START142( ) ;
         EVT142( ) ;
      }

      protected void EVT142( )
      {
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) && ! wbErr )
            {
               /* Read Web Panel buttons. */
               sEvt = cgiGet( "_EventName");
               EvtGridId = cgiGet( "_EventGridId");
               EvtRowId = cgiGet( "_EventRowId");
               if ( StringUtil.Len( sEvt) > 0 )
               {
                  sEvtType = StringUtil.Left( sEvt, 1);
                  sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-1));
                  if ( StringUtil.StrCmp(sEvtType, "E") == 0 )
                  {
                     sEvtType = StringUtil.Right( sEvt, 1);
                     if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                     {
                        sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                        if ( StringUtil.StrCmp(sEvt, "RFR") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                        }
                        else if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: Start */
                           E11142 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: Load */
                           E12142 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                        {
                           context.wbHandled = 1;
                           if ( ! wbErr )
                           {
                              Rfr0gs = false;
                              if ( ! Rfr0gs )
                              {
                              }
                              dynload_actions( ) ;
                           }
                           /* No code required for Cancel button. It is implemented as the Reset button. */
                        }
                        else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           dynload_actions( ) ;
                        }
                     }
                     else
                     {
                     }
                  }
                  context.wbHandled = 1;
               }
            }
         }
      }

      protected void WE142( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm142( ) ;
            }
         }
      }

      protected void PA142( )
      {
         if ( nDonePA == 0 )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            init_web_controls( ) ;
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
            if ( ! context.isAjaxRequest( ) )
            {
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void send_integrity_hashes( )
      {
      }

      protected void clear_multi_value_controls( )
      {
         if ( context.isAjaxRequest( ) )
         {
            dynload_actions( ) ;
            before_start_formulas( ) ;
         }
      }

      protected void fix_multi_value_controls( )
      {
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF142( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
      }

      protected void RF142( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E12142 ();
            WB140( ) ;
         }
      }

      protected void send_integrity_lvl_hashes142( )
      {
      }

      protected void before_start_formulas( )
      {
         fix_multi_value_controls( ) ;
      }

      protected void STRUP140( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E11142 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            /* Read variables values. */
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void GXStart( )
      {
         /* Execute user event: Start */
         E11142 ();
         if (returnInSub) return;
      }

      protected void E11142( )
      {
         /* Start Routine */
         returnInSub = false;
         GXt_SdtWallet1 = AV25wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV25wallet = GXt_SdtWallet1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV20hexaParameters)) )
         {
            GXt_SdtKeyInfo2 = AV16keyInfo;
            new GeneXus.Programs.wallet.getlogindistcrypt(context ).execute( out  GXt_SdtKeyInfo2) ;
            AV16keyInfo = GXt_SdtKeyInfo2;
            AV17parametersSDT.gxTpr_Address = AV16keyInfo.gxTpr_Address;
            AV17parametersSDT.gxTpr_Publickey = AV16keyInfo.gxTpr_Publickey;
            GXt_char3 = AV11error;
            GXt_char4 = AV17parametersSDT.gxTpr_Password;
            new GeneXus.Programs.nbitcoin.eccsignmsg(context ).execute(  AV16keyInfo.gxTpr_Privatekey,  AV16keyInfo.gxTpr_Publickey, out  GXt_char4, out  GXt_char3) ;
            AV17parametersSDT.gxTpr_Password = GXt_char4;
            AV11error = GXt_char3;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               GXt_char4 = AV11error;
               GXt_char3 = AV17parametersSDT.gxTpr_Signature;
               new GeneXus.Programs.nbitcoin.schnorrsignmsg(context ).execute(  AV16keyInfo.gxTpr_Privatekey,  AV16keyInfo.gxTpr_Publickey, out  GXt_char3, out  GXt_char4) ;
               AV17parametersSDT.gxTpr_Signature = GXt_char3;
               AV11error = GXt_char4;
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  GXt_char4 = AV11error;
                  GXt_char3 = AV11error;
                  new GeneXus.Programs.distcrypt.getdistcryptpublickey(context ).execute( out  GXt_char3) ;
                  new GeneXus.Programs.nbitcoin.eccenctrypt(context ).execute(  GXt_char3,  AV17parametersSDT.ToJSonString(false, true), out  AV10encParameters, out  GXt_char4) ;
                  AV11error = GXt_char4;
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                  {
                     if ( StringUtil.StrCmp(AV25wallet.gxTpr_Networktype, "MainNet") == 0 )
                     {
                        GXt_SdtSDT_IdentityProvider5 = AV15IdentityProvider;
                        new GeneXus.Programs.distcrypt.anonymousdesktopappipmainnet(context ).execute( out  GXt_SdtSDT_IdentityProvider5) ;
                        AV15IdentityProvider = GXt_SdtSDT_IdentityProvider5;
                     }
                     else
                     {
                        GXt_SdtSDT_IdentityProvider5 = AV15IdentityProvider;
                        new GeneXus.Programs.distcrypt.anonymousdesktopappiptestnet(context ).execute( out  GXt_SdtSDT_IdentityProvider5) ;
                        AV15IdentityProvider = GXt_SdtSDT_IdentityProvider5;
                     }
                     AV8ClienteId = AV15IdentityProvider.gxTpr_Identityproviderclientid;
                     GXt_char4 = AV24hexText;
                     new GeneXus.Programs.distcrypt.texttohex(context ).execute(  StringUtil.Trim( AV10encParameters), out  GXt_char4) ;
                     AV24hexText = GXt_char4;
                     GXt_char4 = AV19url;
                     new GeneXus.Programs.distcrypt.sso.getidentprovbaseurl(context ).execute(  AV15IdentityProvider, out  GXt_char4) ;
                     AV19url = GXt_char4 + "?" + StringUtil.Trim( AV8ClienteId) + "," + StringUtil.Trim( AV24hexText);
                     CallWebObject(formatLink(AV19url) );
                     context.wjLocDisableFrm = 0;
                  }
                  else
                  {
                     GX_msglist.addItem("Problems encrypting login transaction");
                  }
               }
               else
               {
                  GX_msglist.addItem("Problems siginig Schnorr login transaction");
               }
            }
            else
            {
               GX_msglist.addItem("Problems siginig login transaction");
            }
         }
         else
         {
            GXt_SdtExternalUser6 = AV12externalUser;
            new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser6) ;
            AV12externalUser = GXt_SdtExternalUser6;
            AV7callerURL = AV21WebSession.Get("LoginCaller");
            GXt_SdtKeyInfo2 = AV16keyInfo;
            new GeneXus.Programs.wallet.getlogindistcrypt(context ).execute( out  GXt_SdtKeyInfo2) ;
            AV16keyInfo = GXt_SdtKeyInfo2;
            GXt_char4 = AV10encParameters;
            new GeneXus.Programs.distcrypt.hextotext(context ).execute(  StringUtil.Trim( AV20hexaParameters), out  GXt_char4) ;
            AV10encParameters = GXt_char4;
            GXt_char4 = AV11error;
            new GeneXus.Programs.nbitcoin.eccdecrypt(context ).execute(  StringUtil.Trim( AV16keyInfo.gxTpr_Privatekey),  StringUtil.Trim( AV10encParameters), out  AV23retParametersTxt, out  GXt_char4) ;
            AV11error = GXt_char4;
            AV22retParameters.FromJSonString(AV23retParametersTxt, null);
            AV12externalUser.gxTpr_Externaltoken = AV22retParameters.gxTpr_Externaltoken;
            AV12externalUser.gxTpr_Userinfo = AV22retParameters.gxTpr_Userinfo;
            AV12externalUser.gxTpr_Keyinfo = AV16keyInfo;
            GXt_char4 = AV11error;
            new GeneXus.Programs.distcrypt.sso.updateandsaveexternaluser(context ).execute(  AV12externalUser, out  GXt_char4) ;
            AV11error = GXt_char4;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               CallWebObject(formatLink(AV7callerURL) );
               context.wjLocDisableFrm = 0;
            }
            else
            {
               GX_msglist.addItem("Error creating additional Keys for ExternalUser: "+AV11error);
            }
         }
      }

      protected void nextLoad( )
      {
      }

      protected void E12142( )
      {
         /* Load Routine */
         returnInSub = false;
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV20hexaParameters = (string)getParm(obj,0);
         AssignAttri("", false, "AV20hexaParameters", AV20hexaParameters);
         GxWebStd.gx_hidden_field( context, "gxhash_vHEXAPARAMETERS", GetSecureSignedToken( "", AV20hexaParameters, context));
      }

      public override string getresponse( string sGXDynURL )
      {
         initialize_properties( ) ;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         sDynURL = sGXDynURL;
         nGotPars = (short)(1);
         nGXWrapped = (short)(1);
         context.SetWrapped(true);
         PA142( ) ;
         WS142( ) ;
         WE142( ) ;
         cleanup();
         context.SetWrapped(false);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      protected void define_styles( )
      {
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202613016301367", true, true, false);
            idxLst = (int)(idxLst+1);
         }
         if ( ! outputEnabled )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
         /* End function define_styles */
      }

      protected void include_jscripts( )
      {
         if ( nGXWrapped != 1 )
         {
            context.AddJavascriptSource("messages.eng.js", "?"+GetCacheInvalidationToken( ), false, true, false);
            context.AddJavascriptSource("distcrypt/sso/webloginanonymous.js", "?202613016301367", false, true, false);
         }
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         Form.Internalname = "FORM";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         if ( context.isSpaRequest( ) )
         {
            enableJsOutput();
         }
      }

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV20hexaParameters","fld":"vHEXAPARAMETERS","hsh":true,"type":"vchar"}]}""");
         return  ;
      }

      public override void cleanup( )
      {
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
      }

      public override void initialize( )
      {
         wcpOAV20hexaParameters = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GX_FocusControl = "";
         sPrefix = "";
         Form = new GXWebForm();
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV25wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV16keyInfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV17parametersSDT = new GeneXus.Programs.distcrypt.SdtParametersSDT(context);
         AV11error = "";
         GXt_char3 = "";
         AV10encParameters = "";
         AV15IdentityProvider = new GeneXus.Programs.distcrypt.sso.SdtSDT_IdentityProvider(context);
         GXt_SdtSDT_IdentityProvider5 = new GeneXus.Programs.distcrypt.sso.SdtSDT_IdentityProvider(context);
         AV8ClienteId = "";
         AV24hexText = "";
         AV19url = "";
         AV12externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser6 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV7callerURL = "";
         AV21WebSession = context.GetSession();
         GXt_SdtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
         AV23retParametersTxt = "";
         AV22retParameters = new GeneXus.Programs.distcrypt.SdtRetrunParametersSDT(context);
         GXt_char4 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         /* GeneXus formulas. */
      }

      private short nGotPars ;
      private short GxWebError ;
      private short nGXWrapped ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private int idxLst ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV11error ;
      private string GXt_char3 ;
      private string AV8ClienteId ;
      private string AV19url ;
      private string AV7callerURL ;
      private string GXt_char4 ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private string AV20hexaParameters ;
      private string wcpOAV20hexaParameters ;
      private string AV10encParameters ;
      private string AV24hexText ;
      private string AV23retParametersTxt ;
      private IGxSession AV21WebSession ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GeneXus.Programs.wallet.SdtWallet AV25wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo AV16keyInfo ;
      private GeneXus.Programs.distcrypt.SdtParametersSDT AV17parametersSDT ;
      private GeneXus.Programs.distcrypt.sso.SdtSDT_IdentityProvider AV15IdentityProvider ;
      private GeneXus.Programs.distcrypt.sso.SdtSDT_IdentityProvider GXt_SdtSDT_IdentityProvider5 ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV12externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser6 ;
      private GeneXus.Programs.nbitcoin.SdtKeyInfo GXt_SdtKeyInfo2 ;
      private GeneXus.Programs.distcrypt.SdtRetrunParametersSDT AV22retParameters ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
