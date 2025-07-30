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
namespace GeneXus.Programs.wallet.registered {
   public class timedwalletbackupnotowner : GXWebComponent
   {
      public timedwalletbackupnotowner( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
            context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         }
      }

      public timedwalletbackupnotowner( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( )
      {
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      public override void SetPrefix( string sPPrefix )
      {
         sPrefix = sPPrefix;
      }

      protected override void createObjects( )
      {
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
            if ( nGotPars == 0 )
            {
               entryPointCalled = false;
               gxfirstwebparm = GetNextPar( );
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
               else if ( StringUtil.StrCmp(gxfirstwebparm, "dyncomponent") == 0 )
               {
                  setAjaxEventMode();
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  nDynComponent = 1;
                  sCompPrefix = GetPar( "sCompPrefix");
                  sSFPrefix = GetPar( "sSFPrefix");
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix});
                  componentstart();
                  context.httpAjaxContext.ajax_rspStartCmp(sPrefix);
                  componentdraw();
                  context.httpAjaxContext.ajax_rspEndCmp();
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
                  gxfirstwebparm = GetNextPar( );
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
               {
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  gxfirstwebparm = GetNextPar( );
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridcontacts") == 0 )
               {
                  gxnrGridcontacts_newrow_invoke( ) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridcontacts") == 0 )
               {
                  gxgrGridcontacts_refresh_invoke( ) ;
                  return  ;
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
               if ( toggleJsOutput )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableJsOutput();
                  }
               }
            }
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.IsLocalStorageSupported( ) )
            {
               context.PushCurrentUrl();
            }
         }
      }

      protected void gxnrGridcontacts_newrow_invoke( )
      {
         nRC_GXsfl_16 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_16"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_16_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_16_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_16_idx = GetPar( "sGXsfl_16_idx");
         sPrefix = GetPar( "sPrefix");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridcontacts_newrow( ) ;
         /* End function gxnrGridcontacts_newrow_invoke */
      }

      protected void gxgrGridcontacts_refresh_invoke( )
      {
         AV6based64Key = GetPar( "based64Key");
         ajax_req_read_hidden_sdt(GetNextPar( ), AV16group_sdt_my);
         ajax_req_read_hidden_sdt(GetNextPar( ), AV13externalUser);
         AV39decryptedSecret = GetPar( "decryptedSecret");
         AV14group_sdt.gxTpr_Minimumshares = (short)(Math.Round(NumberUtil.Val( GetNextPar( ), "."), 18, MidpointRounding.ToEven));
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridcontacts_refresh( AV6based64Key, AV16group_sdt_my, AV13externalUser, AV39decryptedSecret, AV14group_sdt.gxTpr_Minimumshares, sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridcontacts_refresh_invoke */
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               ValidateSpaRequest();
            }
            PA2N2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavCtldate_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtldate_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtldate_Enabled), 5, 0), true);
               edtavCtlminimumshares_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlminimumshares_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlminimumshares_Enabled), 5, 0), true);
               edtavCtlcontactid1_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcontactid1_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcontactid1_Enabled), 5, 0), !bGXsfl_16_Refreshing);
               edtavCtlcontactprivatename_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcontactprivatename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcontactprivatename_Enabled), 5, 0), !bGXsfl_16_Refreshing);
               edtavCtlcontactusername_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcontactusername_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcontactusername_Enabled), 5, 0), !bGXsfl_16_Refreshing);
               edtavCtlnumshares_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlnumshares_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlnumshares_Enabled), 5, 0), !bGXsfl_16_Refreshing);
               edtavCtlcontactinvitationsent_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcontactinvitationsent_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcontactinvitationsent_Enabled), 5, 0), !bGXsfl_16_Refreshing);
               edtavCtlcontactinvitacionaccepted_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcontactinvitacionaccepted_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcontactinvitacionaccepted_Enabled), 5, 0), !bGXsfl_16_Refreshing);
               WS2N2( ) ;
               if ( ! isAjaxCallMode( ) )
               {
                  if ( nDynComponent == 0 )
                  {
                     throw new System.Net.WebException("WebComponent is not allowed to run") ;
                  }
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
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, true);
      }

      protected void RenderHtmlOpenForm( )
      {
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
            context.WriteHtmlText( "<title>") ;
            context.SendWebValue( "Timed Wallet Backup Not Owner") ;
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
         }
         if ( ( ( context.GetBrowserType( ) == 1 ) || ( context.GetBrowserType( ) == 5 ) ) && ( StringUtil.StrCmp(context.GetBrowserVersion( ), "7.0") == 0 ) )
         {
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1759500), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1759500), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1759500), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 1759500), false, true);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 1759500), false, true);
         context.AddJavascriptSource("calendar-en.js", "?"+context.GetBuildNumber( 1759500), false, true);
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            context.CloseHtmlHeader();
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
            FormProcess = " data-HasEnter=\"false\" data-Skiponenter=\"false\"";
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
            context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
            context.WriteHtmlText( FormProcess+">") ;
            context.skipLines(1);
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.timedwalletbackupnotowner") +"\">") ;
            GxWebStd.gx_hidden_field( context, "_EventName", "");
            GxWebStd.gx_hidden_field( context, "_EventGridId", "");
            GxWebStd.gx_hidden_field( context, "_EventRowId", "");
            context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
            AssignProp(sPrefix, false, "FORM", "Class", "form-horizontal Form", true);
         }
         else
         {
            bool toggleHtmlOutput = isOutputEnabled( );
            if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
            {
               if ( context.isSpaRequest( ) )
               {
                  disableOutput();
               }
            }
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gxwebcomponent-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            if ( toggleHtmlOutput )
            {
               if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableOutput();
                  }
               }
            }
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
         }
         if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"vBASED64KEY", StringUtil.RTrim( AV6based64Key));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vBASED64KEY", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( AV6based64Key, "")), context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vEXTERNALUSER", AV13externalUser);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vEXTERNALUSER", AV13externalUser);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vEXTERNALUSER", GetSecureSignedToken( sPrefix, AV13externalUser, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vDECRYPTEDSECRET", AV39decryptedSecret);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vDECRYPTEDSECRET", GetSecureSignedToken( sPrefix, AV39decryptedSecret, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", sPrefix+"hsh"+"TimedWalletBackupNotOwner");
         forbiddenHiddens.Add("GXV2", context.localUtil.Format( (decimal)(AV14group_sdt.gxTpr_Minimumshares), "ZZZ9"));
         GxWebStd.gx_hidden_field( context, sPrefix+"hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("wallet\\registered\\timedwalletbackupnotowner:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Onetimeconstrain", AV27oneTimeConstrain);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Onetimeconstrain", AV27oneTimeConstrain);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Group_sdt", AV14group_sdt);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Group_sdt", AV14group_sdt);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Groupcontacts", AV20groupContacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Groupcontacts", AV20groupContacts);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_16", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_16), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vBASED64KEY", StringUtil.RTrim( AV6based64Key));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vBASED64KEY", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( AV6based64Key, "")), context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vGROUP_SDT_MY", AV16group_sdt_my);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vGROUP_SDT_MY", AV16group_sdt_my);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vEXTERNALUSER", AV13externalUser);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vEXTERNALUSER", AV13externalUser);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vEXTERNALUSER", GetSecureSignedToken( sPrefix, AV13externalUser, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vSHARESTORECOVER", AV33sharesToRecover);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vSHARESTORECOVER", AV33sharesToRecover);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vDECRYPTEDSECRET", AV39decryptedSecret);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vDECRYPTEDSECRET", GetSecureSignedToken( sPrefix, AV39decryptedSecret, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vERROR", StringUtil.RTrim( AV11error));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vMESSAGE_SIGNATURE", AV23message_signature);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vMESSAGE_SIGNATURE", AV23message_signature);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vSDT_MESSAGE", AV30sdt_message);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vSDT_MESSAGE", AV30sdt_message);
         }
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vNUMOFSHAREDWASREACH", AV24numOfSharedWasReach);
         GxWebStd.gx_hidden_field( context, sPrefix+"vPOPUPNAME", StringUtil.RTrim( AV28PopupName));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vGROUPCONTACTS", AV20groupContacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vGROUPCONTACTS", AV20groupContacts);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vONETIMECONSTRAIN", AV27oneTimeConstrain);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vONETIMECONSTRAIN", AV27oneTimeConstrain);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vGROUP_SDT", AV14group_sdt);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vGROUP_SDT", AV14group_sdt);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vGROUP_SDT_MY_Cleartextshare", AV16group_sdt_my.gxTpr_Cleartextshare);
      }

      protected void RenderHtmlCloseForm2N2( )
      {
         SendCloseFormHiddens( ) ;
         if ( ( StringUtil.Len( sPrefix) != 0 ) && ( context.isAjaxRequest( ) || context.isSpaRequest( ) ) )
         {
            componentjscripts();
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GX_FocusControl", GX_FocusControl);
         define_styles( ) ;
         SendSecurityToken(sPrefix);
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            SendAjaxEncryptionKey();
            SendComponentObjects();
            SendServerCommands();
            SendState();
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
            context.WriteHtmlTextNl( "</form>") ;
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
         else
         {
            SendWebComponentState();
            context.WriteHtmlText( "</div>") ;
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
         }
      }

      public override string GetPgmname( )
      {
         return "Wallet.registered.TimedWalletBackupNotOwner" ;
      }

      public override string GetPgmdesc( )
      {
         return "Timed Wallet Backup Not Owner" ;
      }

      protected void WB2N0( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               RenderHtmlHeaders( ) ;
            }
            RenderHtmlOpenForm( ) ;
            if ( StringUtil.Len( sPrefix) != 0 )
            {
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.registered.timedwalletbackupnotowner");
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", sPrefix, "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavCtldate_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtldate_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtldate_Internalname, "The backup will be available for recovery on:", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',0)\"";
            context.WriteHtmlText( "<div id=\""+edtavCtldate_Internalname+"_dp_container\" class=\"dp_container\" style=\"white-space:nowrap;display:inline;\">") ;
            GxWebStd.gx_single_line_edit( context, edtavCtldate_Internalname, context.localUtil.Format(AV27oneTimeConstrain.gxTpr_Date, "99/99/99"), context.localUtil.Format( AV27oneTimeConstrain.gxTpr_Date, "99/99/99"), TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',0,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',0,12,'eng',false,0);"+";gx.evt.onblur(this,8);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtldate_Jsonclick, 0, "Attribute", "", "", "", "", edtavCtldate_Visible, edtavCtldate_Enabled, 0, "text", "", 8, "chr", 1, "row", 8, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/registered/TimedWalletBackupNotOwner.htm");
            GxWebStd.gx_bitmap( context, edtavCtldate_Internalname+"_dp_trigger", context.GetImagePath( "61b9b5d3-dff6-4d59-9b00-da61bc2cbe93", "", context.GetTheme( )), "", "", "", "", ((edtavCtldate_Visible==0)||(edtavCtldate_Enabled==0) ? 0 : 1), 0, "Date selector", "Date selector", 0, 1, 0, "", 0, "", 0, 0, 0, "", "", "cursor: pointer;", "", "", "", "", "", "", "", "", 1, false, false, "", "none", "HLP_Wallet/registered/TimedWalletBackupNotOwner.htm");
            context.WriteHtmlTextNl( "</div>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtlminimumshares_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtlminimumshares_Internalname, "Minimum amount of votes to recover the backup", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCtlminimumshares_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV14group_sdt.gxTpr_Minimumshares), 4, 0, ".", "")), StringUtil.LTrim( ((edtavCtlminimumshares_Enabled!=0) ? context.localUtil.Format( (decimal)(AV14group_sdt.gxTpr_Minimumshares), "ZZZ9") : context.localUtil.Format( (decimal)(AV14group_sdt.gxTpr_Minimumshares), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,13);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtlminimumshares_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtlminimumshares_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/registered/TimedWalletBackupNotOwner.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridcontactsContainer.SetWrapped(nGXWrapped);
            StartGridControl16( ) ;
         }
         if ( wbEnd == 16 )
         {
            wbEnd = 0;
            nRC_GXsfl_16 = (int)(nGXsfl_16_idx-1);
            if ( GridcontactsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV42GXV3 = nGXsfl_16_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+sPrefix+"GridcontactsContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Gridcontacts", GridcontactsContainer, subGridcontacts_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridcontactsContainerData", GridcontactsContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridcontactsContainerData"+"V", GridcontactsContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridcontactsContainerData"+"V"+"\" value='"+GridcontactsContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 25,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttClose1_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(16), 2, 0)+","+"null"+");", "Close", bttClose1_Jsonclick, 5, "Close", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'CLOSE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/TimedWalletBackupNotOwner.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 28,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttRestorewallet_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(16), 2, 0)+","+"null"+");", "Restore Wallet", bttRestorewallet_Jsonclick, 5, "Restore Wallet", "", StyleString, ClassString, bttRestorewallet_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'RESTORE WALLET\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/TimedWalletBackupNotOwner.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 31,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCreaterecoveredwallet_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(16), 2, 0)+","+"null"+");", "Create recovered wallet", bttCreaterecoveredwallet_Jsonclick, 5, "Create recovered wallet", "", StyleString, ClassString, bttCreaterecoveredwallet_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'CREATE RECOVERED WALLET\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/TimedWalletBackupNotOwner.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 16 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridcontactsContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV42GXV3 = nGXsfl_16_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+sPrefix+"GridcontactsContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Gridcontacts", GridcontactsContainer, subGridcontacts_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridcontactsContainerData", GridcontactsContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridcontactsContainerData"+"V", GridcontactsContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridcontactsContainerData"+"V"+"\" value='"+GridcontactsContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START2N2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.isSpaRequest( ) )
            {
               if ( context.ExposeMetadata( ) )
               {
                  Form.Meta.addItem("generator", "GeneXus .NET 18_0_13-186702", 0) ;
               }
            }
            Form.Meta.addItem("description", "Timed Wallet Backup Not Owner", 0) ;
            context.wjLoc = "";
            context.nUserReturn = 0;
            context.wbHandled = 0;
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               sXEvt = cgiGet( "_EventName");
               if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
               {
               }
            }
         }
         wbErr = false;
         if ( ( StringUtil.Len( sPrefix) == 0 ) || ( nDraw == 1 ) )
         {
            if ( nDoneStart == 0 )
            {
               STRUP2N0( ) ;
            }
         }
      }

      protected void WS2N2( )
      {
         START2N2( ) ;
         EVT2N2( ) ;
      }

      protected void EVT2N2( )
      {
         sXEvt = cgiGet( "_EventName");
         if ( ( ( ( StringUtil.Len( sPrefix) == 0 ) ) || ( StringUtil.StringSearch( sXEvt, sPrefix, 1) > 0 ) ) && ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) && ! wbErr )
            {
               /* Read Web Panel buttons. */
               if ( context.wbHandled == 0 )
               {
                  if ( StringUtil.Len( sPrefix) == 0 )
                  {
                     sEvt = cgiGet( "_EventName");
                     EvtGridId = cgiGet( "_EventGridId");
                     EvtRowId = cgiGet( "_EventRowId");
                  }
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
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2N0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CLOSE'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2N0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Close' */
                                    E112N2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'RESTORE WALLET'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2N0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Restore Wallet' */
                                    E122N2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CREATE RECOVERED WALLET'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2N0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Create recovered wallet' */
                                    E132N2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2N0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E142N2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2N0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavCtldate_Internalname;
                                    AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 }
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 17), "GRIDCONTACTS.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2N0( ) ;
                              }
                              nGXsfl_16_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
                              SubsflControlProps_162( ) ;
                              AV42GXV3 = nGXsfl_16_idx;
                              if ( ( AV20groupContacts.Count >= AV42GXV3 ) && ( AV42GXV3 > 0 ) )
                              {
                                 AV20groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20groupContacts.Item(AV42GXV3));
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtldate_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Refresh */
                                          E152N2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDCONTACTS.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtldate_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Gridcontacts.Load */
                                          E162N2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          if ( ! wbErr )
                                          {
                                             Rfr0gs = false;
                                             if ( ! Rfr0gs )
                                             {
                                             }
                                             dynload_actions( ) ;
                                          }
                                       }
                                    }
                                    /* No code required for Cancel button. It is implemented as the Reset button. */
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                                 {
                                    if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                                    {
                                       STRUP2N0( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtldate_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                       }
                                    }
                                 }
                              }
                              else
                              {
                              }
                           }
                        }
                     }
                     context.wbHandled = 1;
                  }
               }
            }
         }
      }

      protected void WE2N2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm2N2( ) ;
            }
         }
      }

      protected void PA2N2( )
      {
         if ( nDonePA == 0 )
         {
            if ( StringUtil.Len( sPrefix) != 0 )
            {
               initialize_properties( ) ;
            }
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
               {
                  gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
               }
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            toggleJsOutput = isJsOutputEnabled( );
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( context.isSpaRequest( ) )
               {
                  disableJsOutput();
               }
            }
            init_web_controls( ) ;
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( toggleJsOutput )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableJsOutput();
                  }
               }
            }
            if ( ! context.isAjaxRequest( ) )
            {
               GX_FocusControl = edtavCtldate_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridcontacts_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_162( ) ;
         while ( nGXsfl_16_idx <= nRC_GXsfl_16 )
         {
            sendrow_162( ) ;
            nGXsfl_16_idx = ((subGridcontacts_Islastpage==1)&&(nGXsfl_16_idx+1>subGridcontacts_fnc_Recordsperpage( )) ? 1 : nGXsfl_16_idx+1);
            sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
            SubsflControlProps_162( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridcontactsContainer)) ;
         /* End function gxnrGridcontacts_newrow */
      }

      protected void gxgrGridcontacts_refresh( string AV6based64Key ,
                                               GeneXus.Programs.wallet.registered.SdtGroup_SDT AV16group_sdt_my ,
                                               GeneXus.Programs.distcrypt.SdtExternalUser AV13externalUser ,
                                               string AV39decryptedSecret ,
                                               short GXV2 ,
                                               string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDCONTACTS_nCurrentRecord = 0;
         RF2N2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", sPrefix+"hsh"+"TimedWalletBackupNotOwner");
         forbiddenHiddens.Add("GXV2", context.localUtil.Format( (decimal)(AV14group_sdt.gxTpr_Minimumshares), "ZZZ9"));
         GxWebStd.gx_hidden_field( context, sPrefix+"hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("wallet\\registered\\timedwalletbackupnotowner:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
         /* End function gxgrGridcontacts_refresh */
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
         RF2N2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtldate_Enabled = 0;
         AssignProp(sPrefix, false, edtavCtldate_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtldate_Enabled), 5, 0), true);
         edtavCtlminimumshares_Enabled = 0;
         AssignProp(sPrefix, false, edtavCtlminimumshares_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlminimumshares_Enabled), 5, 0), true);
         edtavCtlcontactid1_Enabled = 0;
         edtavCtlcontactprivatename_Enabled = 0;
         edtavCtlcontactusername_Enabled = 0;
         edtavCtlnumshares_Enabled = 0;
         edtavCtlcontactinvitationsent_Enabled = 0;
         edtavCtlcontactinvitacionaccepted_Enabled = 0;
      }

      protected void RF2N2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridcontactsContainer.ClearRows();
         }
         wbStart = 16;
         /* Execute user event: Refresh */
         E152N2 ();
         nGXsfl_16_idx = 1;
         sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
         SubsflControlProps_162( ) ;
         bGXsfl_16_Refreshing = true;
         GridcontactsContainer.AddObjectProperty("GridName", "Gridcontacts");
         GridcontactsContainer.AddObjectProperty("CmpContext", sPrefix);
         GridcontactsContainer.AddObjectProperty("InMasterPage", "false");
         GridcontactsContainer.AddObjectProperty("Class", "Grid");
         GridcontactsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridcontactsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridcontactsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridcontacts_Backcolorstyle), 1, 0, ".", "")));
         GridcontactsContainer.PageSize = subGridcontacts_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_162( ) ;
            /* Execute user event: Gridcontacts.Load */
            E162N2 ();
            wbEnd = 16;
            WB2N0( ) ;
         }
         bGXsfl_16_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes2N2( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"vBASED64KEY", StringUtil.RTrim( AV6based64Key));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vBASED64KEY", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( AV6based64Key, "")), context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vEXTERNALUSER", AV13externalUser);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vEXTERNALUSER", AV13externalUser);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vEXTERNALUSER", GetSecureSignedToken( sPrefix, AV13externalUser, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vDECRYPTEDSECRET", AV39decryptedSecret);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vDECRYPTEDSECRET", GetSecureSignedToken( sPrefix, AV39decryptedSecret, context));
      }

      protected int subGridcontacts_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGridcontacts_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subGridcontacts_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subGridcontacts_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtldate_Enabled = 0;
         AssignProp(sPrefix, false, edtavCtldate_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtldate_Enabled), 5, 0), true);
         edtavCtlminimumshares_Enabled = 0;
         AssignProp(sPrefix, false, edtavCtlminimumshares_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlminimumshares_Enabled), 5, 0), true);
         edtavCtlcontactid1_Enabled = 0;
         edtavCtlcontactprivatename_Enabled = 0;
         edtavCtlcontactusername_Enabled = 0;
         edtavCtlnumshares_Enabled = 0;
         edtavCtlcontactinvitationsent_Enabled = 0;
         edtavCtlcontactinvitacionaccepted_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP2N0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vONETIMECONSTRAIN"), AV27oneTimeConstrain);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vGROUP_SDT"), AV14group_sdt);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Onetimeconstrain"), AV27oneTimeConstrain);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Group_sdt"), AV14group_sdt);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Groupcontacts"), AV20groupContacts);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vGROUPCONTACTS"), AV20groupContacts);
            /* Read saved values. */
            nRC_GXsfl_16 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_16"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_16 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_16"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_16_fel_idx = 0;
            while ( nGXsfl_16_fel_idx < nRC_GXsfl_16 )
            {
               nGXsfl_16_fel_idx = ((subGridcontacts_Islastpage==1)&&(nGXsfl_16_fel_idx+1>subGridcontacts_fnc_Recordsperpage( )) ? 1 : nGXsfl_16_fel_idx+1);
               sGXsfl_16_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_162( ) ;
               AV42GXV3 = nGXsfl_16_fel_idx;
               if ( ( AV20groupContacts.Count >= AV42GXV3 ) && ( AV42GXV3 > 0 ) )
               {
                  AV20groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20groupContacts.Item(AV42GXV3));
               }
            }
            if ( nGXsfl_16_fel_idx == 0 )
            {
               nGXsfl_16_idx = 1;
               sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
               SubsflControlProps_162( ) ;
            }
            nGXsfl_16_fel_idx = 1;
            /* Read variables values. */
            if ( context.localUtil.VCDate( cgiGet( edtavCtldate_Internalname), 1) == 0 )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_faildate", new   object[]  {""}), 1, "CTLDATE");
               GX_FocusControl = edtavCtldate_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV27oneTimeConstrain.gxTpr_Date = DateTime.MinValue;
            }
            else
            {
               AV27oneTimeConstrain.gxTpr_Date = context.localUtil.CToD( cgiGet( edtavCtldate_Internalname), 1);
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavCtlminimumshares_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavCtlminimumshares_Internalname), ".", ",") > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CTLMINIMUMSHARES");
               GX_FocusControl = edtavCtlminimumshares_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV14group_sdt.gxTpr_Minimumshares = 0;
            }
            else
            {
               AV14group_sdt.gxTpr_Minimumshares = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavCtlminimumshares_Internalname), ".", ","), 18, MidpointRounding.ToEven));
            }
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            forbiddenHiddens = new GXProperties();
            forbiddenHiddens.Add("hshsalt", sPrefix+"hsh"+"TimedWalletBackupNotOwner");
            AV14group_sdt.gxTpr_Minimumshares = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavCtlminimumshares_Internalname), ".", ","), 18, MidpointRounding.ToEven));
            forbiddenHiddens.Add("GXV2", context.localUtil.Format( (decimal)(AV14group_sdt.gxTpr_Minimumshares), "ZZZ9"));
            hsh = cgiGet( sPrefix+"hsh");
            if ( ! GXUtil.CheckEncryptedHash( forbiddenHiddens.ToString(), hsh, GXKey) )
            {
               GXUtil.WriteLogError("wallet\\registered\\timedwalletbackupnotowner:[ SecurityCheckFailed (403 Forbidden) value for]"+forbiddenHiddens.ToJSonString());
               GxWebError = 1;
               context.HttpContext.Response.StatusCode = 403;
               context.WriteHtmlText( "<title>403 Forbidden</title>") ;
               context.WriteHtmlText( "<h1>403 Forbidden</h1>") ;
               context.WriteHtmlText( "<p /><hr />") ;
               GXUtil.WriteLog("send_http_error_code " + 403.ToString());
               return  ;
            }
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void E152N2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         GXt_SdtExternalUser1 = AV13externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV13externalUser = GXt_SdtExternalUser1;
         AV16group_sdt_my.FromJSonString(AV38websession.Get("Group_EDIT"), null);
         if ( AV16group_sdt_my.gxTpr_Isactive && ! AV16group_sdt_my.gxTpr_Numofsharesreached )
         {
            bttRestorewallet_Visible = 1;
            AssignProp(sPrefix, false, bttRestorewallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttRestorewallet_Visible), 5, 0), true);
         }
         else
         {
            bttRestorewallet_Visible = 0;
            AssignProp(sPrefix, false, bttRestorewallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttRestorewallet_Visible), 5, 0), true);
         }
         if ( AV16group_sdt_my.gxTpr_Numofsharesreached )
         {
            bttCreaterecoveredwallet_Visible = 1;
            AssignProp(sPrefix, false, bttCreaterecoveredwallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttCreaterecoveredwallet_Visible), 5, 0), true);
            bttRestorewallet_Visible = 0;
            AssignProp(sPrefix, false, bttRestorewallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttRestorewallet_Visible), 5, 0), true);
         }
         else
         {
            bttCreaterecoveredwallet_Visible = 0;
            AssignProp(sPrefix, false, bttCreaterecoveredwallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttCreaterecoveredwallet_Visible), 5, 0), true);
         }
         GXt_char2 = AV11error;
         new GeneXus.Programs.wallet.registered.getgroupbyid(context ).execute(  AV16group_sdt_my.gxTpr_Othergroup.gxTpr_Referencegroupid,  AV16group_sdt_my.gxTpr_Othergroup.gxTpr_Encpassword, out  AV14group_sdt, out  GXt_char2) ;
         AV11error = GXt_char2;
         AssignAttri(sPrefix, false, "AV11error", AV11error);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            AV20groupContacts.Clear();
            gx_BV16 = true;
            AV49GXV10 = 1;
            while ( AV49GXV10 <= AV14group_sdt.gxTpr_Contact.Count )
            {
               AV18groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14group_sdt.gxTpr_Contact.Item(AV49GXV10));
               AV20groupContacts.Add(AV18groupContact, 0);
               gx_BV16 = true;
               AV49GXV10 = (int)(AV49GXV10+1);
            }
         }
         else
         {
            GX_msglist.addItem(AV11error);
         }
         edtavCtldate_Visible = 0;
         AssignProp(sPrefix, false, edtavCtldate_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavCtldate_Visible), 5, 0), true);
         if ( AV16group_sdt_my.gxTpr_Isactive )
         {
            GXt_SdtGroup_SDT_TimeConstrainItem3 = AV27oneTimeConstrain;
            new GeneXus.Programs.wallet.registered.getnewesttimeconstrain(context ).execute(  AV14group_sdt.gxTpr_Timeconstrain, out  GXt_SdtGroup_SDT_TimeConstrainItem3) ;
            AV27oneTimeConstrain = GXt_SdtGroup_SDT_TimeConstrainItem3;
            edtavCtldate_Visible = 1;
            AssignProp(sPrefix, false, edtavCtldate_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavCtldate_Visible), 5, 0), true);
            GXt_char2 = AV11error;
            new GeneXus.Programs.electrum.getsecretfromoneaddress(context ).execute(  StringUtil.Trim( AV27oneTimeConstrain.gxTpr_Address), out  AV22isActive, out  AV31secret, out  GXt_char2) ;
            AV11error = GXt_char2;
            AssignAttri(sPrefix, false, "AV11error", AV11error);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               if ( ! AV22isActive && String.IsNullOrEmpty(StringUtil.RTrim( AV31secret)) )
               {
                  this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"Backup Status",(string)"Although the backup is Active it needs final authorization from the owner"}, true);
                  bttRestorewallet_Visible = 0;
                  AssignProp(sPrefix, false, bttRestorewallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttRestorewallet_Visible), 5, 0), true);
               }
               else
               {
                  bttRestorewallet_Visible = 0;
                  AssignProp(sPrefix, false, bttRestorewallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttRestorewallet_Visible), 5, 0), true);
                  if ( ! AV22isActive && ! String.IsNullOrEmpty(StringUtil.RTrim( AV31secret)) )
                  {
                     /* User Code */
                      var secret = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(AV31secret);
                     /* User Code */
                      string base64String = System.Convert.ToBase64String(secret);
                     /* User Code */
                      AV6based64Key = base64String;
                     AV9DecryptionResult = AV10EncryptionService.decrypt(AV14group_sdt.gxTpr_Encryptedtextshare, AV6based64Key);
                     if ( AV9DecryptionResult.gxTpr_Success )
                     {
                        AV39decryptedSecret = AV9DecryptionResult.gxTpr_Decryptedtext;
                        AssignAttri(sPrefix, false, "AV39decryptedSecret", AV39decryptedSecret);
                        GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vDECRYPTEDSECRET", GetSecureSignedToken( sPrefix, AV39decryptedSecret, context));
                        bttRestorewallet_Visible = 1;
                        AssignProp(sPrefix, false, bttRestorewallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttRestorewallet_Visible), 5, 0), true);
                     }
                  }
               }
            }
            else
            {
               GX_msglist.addItem(AV11error);
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV13externalUser", AV13externalUser);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV16group_sdt_my", AV16group_sdt_my);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV14group_sdt", AV14group_sdt);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV20groupContacts", AV20groupContacts);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV27oneTimeConstrain", AV27oneTimeConstrain);
      }

      private void E162N2( )
      {
         /* Gridcontacts_Load Routine */
         returnInSub = false;
         AV42GXV3 = 1;
         while ( AV42GXV3 <= AV20groupContacts.Count )
         {
            AV20groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20groupContacts.Item(AV42GXV3));
            if ( AV16group_sdt_my.gxTpr_Amigroupowner )
            {
               edtavCtlcontactprivatename_Visible = 1;
               edtavCtlcontactusername_Visible = 0;
            }
            else
            {
               edtavCtlcontactprivatename_Visible = 0;
               edtavCtlcontactusername_Visible = 1;
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 16;
            }
            sendrow_162( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_16_Refreshing )
            {
               DoAjaxLoad(16, GridcontactsRow);
            }
            AV42GXV3 = (int)(AV42GXV3+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E112N2( )
      {
         /* 'Close' Routine */
         returnInSub = false;
         AV38websession.Set("Group_EDIT", "");
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
         /*  Sending Event outputs  */
      }

      protected void E122N2( )
      {
         /* 'Restore Wallet' Routine */
         returnInSub = false;
         AV24numOfSharedWasReach = false;
         AssignAttri(sPrefix, false, "AV24numOfSharedWasReach", AV24numOfSharedWasReach);
         AV33sharesToRecover.Clear();
         /* Execute user subroutine: 'LOOK FOR OTHERS SHARES' */
         S112 ();
         if (returnInSub) return;
         /* Execute user subroutine: 'UNENCRYPT MY SHARES AND COMBINE WITH OTHERS' */
         S122 ();
         if (returnInSub) return;
         /* Execute user subroutine: 'SEND COMBINED SHARES TO GROUP MEMBERS' */
         S132 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV33sharesToRecover", AV33sharesToRecover);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV16group_sdt_my", AV16group_sdt_my);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV23message_signature", AV23message_signature);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV30sdt_message", AV30sdt_message);
      }

      protected void S112( )
      {
         /* 'LOOK FOR OTHERS SHARES' Routine */
         returnInSub = false;
         AV50GXV11 = 1;
         while ( AV50GXV11 <= AV14group_sdt.gxTpr_Contact.Count )
         {
            AV18groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14group_sdt.gxTpr_Contact.Item(AV50GXV11));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV18groupContact.gxTpr_Contactusername), StringUtil.Trim( AV13externalUser.gxTpr_Userinfo.gxTpr_Username)) != 0 )
            {
               AV8contactFound = false;
               AV51GXV12 = 1;
               while ( AV51GXV12 <= AV16group_sdt_my.gxTpr_Contact.Count )
               {
                  AV19groupContactMy = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV16group_sdt_my.gxTpr_Contact.Item(AV51GXV12));
                  if ( StringUtil.StrCmp(AV19groupContactMy.gxTpr_Contactusername, AV18groupContact.gxTpr_Contactusername) == 0 )
                  {
                     if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV19groupContactMy.gxTpr_Cleartextshare)) )
                     {
                        if ( AV19groupContactMy.gxTpr_Numofsharesreached )
                        {
                           AV16group_sdt_my.gxTpr_Cleartextshare = AV19groupContactMy.gxTpr_Cleartextshare;
                           AV16group_sdt_my.gxTpr_Numofsharesreached = AV19groupContactMy.gxTpr_Numofsharesreached;
                           AV24numOfSharedWasReach = true;
                           AssignAttri(sPrefix, false, "AV24numOfSharedWasReach", AV24numOfSharedWasReach);
                           /* Execute user subroutine: 'UPDATE MY GROUP' */
                           S142 ();
                           if (returnInSub) return;
                        }
                        else
                        {
                           AV34sharesToRecoverTemp.Clear();
                           AV34sharesToRecoverTemp.FromJSonString(AV19groupContactMy.gxTpr_Cleartextshare, null);
                           AV52GXV13 = 1;
                           while ( AV52GXV13 <= AV34sharesToRecoverTemp.Count )
                           {
                              AV26oneShare = ((string)AV34sharesToRecoverTemp.Item(AV52GXV13));
                              AV33sharesToRecover.Add(AV26oneShare, 0);
                              AV52GXV13 = (int)(AV52GXV13+1);
                           }
                        }
                        AV8contactFound = true;
                        if (true) break;
                     }
                  }
                  AV51GXV12 = (int)(AV51GXV12+1);
               }
               if ( ! AV8contactFound )
               {
                  GXt_char2 = AV11error;
                  new GeneXus.Programs.wallet.registered.getgroupbyid(context ).execute(  AV18groupContact.gxTpr_Contactgroupid,  AV18groupContact.gxTpr_Contactgroupencpassword, out  AV17group_sdt_temp, out  GXt_char2) ;
                  AV11error = GXt_char2;
                  AssignAttri(sPrefix, false, "AV11error", AV11error);
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                  {
                     AV25oneGroupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
                     if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV17group_sdt_temp.gxTpr_Cleartextshare)) )
                     {
                        AV25oneGroupContact.gxTpr_Numofsharesreached = AV17group_sdt_temp.gxTpr_Numofsharesreached;
                        AV25oneGroupContact.gxTpr_Cleartextshare = StringUtil.Trim( AV17group_sdt_temp.gxTpr_Cleartextshare);
                        AV34sharesToRecoverTemp.Clear();
                        AV34sharesToRecoverTemp.FromJSonString(AV17group_sdt_temp.gxTpr_Cleartextshare, null);
                        AV53GXV14 = 1;
                        while ( AV53GXV14 <= AV34sharesToRecoverTemp.Count )
                        {
                           AV26oneShare = ((string)AV34sharesToRecoverTemp.Item(AV53GXV14));
                           AV33sharesToRecover.Add(AV26oneShare, 0);
                           AV53GXV14 = (int)(AV53GXV14+1);
                        }
                     }
                  }
                  else
                  {
                     GX_msglist.addItem(AV11error);
                  }
               }
            }
            AV50GXV11 = (int)(AV50GXV11+1);
         }
      }

      protected void S122( )
      {
         /* 'UNENCRYPT MY SHARES AND COMBINE WITH OTHERS' Routine */
         returnInSub = false;
         AV54GXV15 = 1;
         while ( AV54GXV15 <= AV14group_sdt.gxTpr_Contact.Count )
         {
            AV18groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14group_sdt.gxTpr_Contact.Item(AV54GXV15));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV18groupContact.gxTpr_Contactusername), StringUtil.Trim( AV13externalUser.gxTpr_Userinfo.gxTpr_Username)) == 0 )
            {
               GXt_char2 = AV11error;
               new GeneXus.Programs.distributedcryptographylib.decryptjsonfor(context ).execute(  AV18groupContact.gxTpr_Contactencryptedtext,  AV18groupContact.gxTpr_Contactencryptedkey,  AV13externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey, out  AV32share, out  GXt_char2) ;
               AV11error = GXt_char2;
               AssignAttri(sPrefix, false, "AV11error", AV11error);
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  AV36userShares.FromJSonString(AV32share, null);
                  AV16group_sdt_my.gxTpr_Cleartextshare = AV36userShares.ToJSonString(false);
                  AV34sharesToRecoverTemp.Clear();
                  AV34sharesToRecoverTemp.FromJSonString(AV16group_sdt_my.gxTpr_Cleartextshare, null);
                  AV55GXV16 = 1;
                  while ( AV55GXV16 <= AV34sharesToRecoverTemp.Count )
                  {
                     AV26oneShare = ((string)AV34sharesToRecoverTemp.Item(AV55GXV16));
                     AV33sharesToRecover.Add(AV26oneShare, 0);
                     AV55GXV16 = (int)(AV55GXV16+1);
                  }
                  if ( AV33sharesToRecover.Count > 1 )
                  {
                     GXt_char2 = AV11error;
                     new GeneXus.Programs.shamirss.combineshares(context ).execute(  AV33sharesToRecover, out  AV29recoveredSecret, out  GXt_char2) ;
                     AV11error = GXt_char2;
                     AssignAttri(sPrefix, false, "AV11error", AV11error);
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV29recoveredSecret)) )
                     {
                        AV33sharesToRecover.Clear();
                        AV33sharesToRecover.Add(AV29recoveredSecret, 0);
                        AV33sharesToRecover.Add(AV39decryptedSecret, 0);
                        AV29recoveredSecret = "";
                        GXt_char2 = AV11error;
                        new GeneXus.Programs.shamirss.combineshares(context ).execute(  AV33sharesToRecover, out  AV29recoveredSecret, out  GXt_char2) ;
                        AV11error = GXt_char2;
                        AssignAttri(sPrefix, false, "AV11error", AV11error);
                        if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                        {
                           AV16group_sdt_my.gxTpr_Numofsharesreached = true;
                           AV16group_sdt_my.gxTpr_Cleartextshare = AV29recoveredSecret;
                           AV24numOfSharedWasReach = true;
                           AssignAttri(sPrefix, false, "AV24numOfSharedWasReach", AV24numOfSharedWasReach);
                        }
                     }
                     else
                     {
                        if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                        {
                           GX_msglist.addItem(AV11error);
                        }
                     }
                  }
               }
               else
               {
                  GX_msglist.addItem(AV11error);
               }
            }
            AV54GXV15 = (int)(AV54GXV15+1);
         }
      }

      protected void S132( )
      {
         /* 'SEND COMBINED SHARES TO GROUP MEMBERS' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            AV17group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
            AV17group_sdt_temp.gxTpr_Groupname = AV16group_sdt_my.gxTpr_Groupname;
            AV17group_sdt_temp.gxTpr_Grouptype = AV16group_sdt_my.gxTpr_Grouptype;
            AV17group_sdt_temp.gxTpr_Cleartextshare = AV16group_sdt_my.gxTpr_Cleartextshare;
            AV17group_sdt_temp.gxTpr_Numofsharesreached = AV16group_sdt_my.gxTpr_Numofsharesreached;
            AV17group_sdt_temp.gxTpr_Othergroup.gxTpr_Referenceusernname = StringUtil.Trim( AV13externalUser.gxTpr_Userinfo.gxTpr_Username);
            AV17group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid = AV16group_sdt_my.gxTpr_Othergroup.gxTpr_Referencegroupid;
            AV23message_signature.gxTpr_Username = StringUtil.Trim( AV13externalUser.gxTpr_Userinfo.gxTpr_Username);
            AV23message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV13externalUser.gxTpr_Groupskeyinfo.gxTpr_Publickey);
            GXt_char2 = AV11error;
            GXt_char4 = AV23message_signature.gxTpr_Signature;
            new GeneXus.Programs.nbitcoin.eccsignmsg(context ).execute(  AV13externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey,  StringUtil.Trim( AV23message_signature.gxTpr_Username)+StringUtil.Trim( AV23message_signature.gxTpr_Grouppubkey), out  GXt_char4, out  GXt_char2) ;
            AV23message_signature.gxTpr_Signature = GXt_char4;
            AV11error = GXt_char2;
            AssignAttri(sPrefix, false, "AV11error", AV11error);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               AV17group_sdt_temp.gxTpr_Othergroup.gxTpr_Signature = StringUtil.Trim( AV23message_signature.gxTpr_Signature);
               AV56GXV17 = 1;
               while ( AV56GXV17 <= AV14group_sdt.gxTpr_Contact.Count )
               {
                  AV18groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14group_sdt.gxTpr_Contact.Item(AV56GXV17));
                  if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV18groupContact.gxTpr_Contactusername), StringUtil.Trim( AV13externalUser.gxTpr_Userinfo.gxTpr_Username)) == 0 ) )
                  {
                     AV30sdt_message.gxTpr_Id = Guid.NewGuid( );
                     GXt_int5 = 0;
                     new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int5) ;
                     AV30sdt_message.gxTpr_Datetimeunix = GXt_int5;
                     AV30sdt_message.gxTpr_Messagetype = 100;
                     AV30sdt_message.gxTpr_Message = AV17group_sdt_temp.ToJSonString(false, true);
                     AV7contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
                     AV7contact.gxTpr_Username = StringUtil.Trim( AV18groupContact.gxTpr_Contactusername);
                     AV7contact.gxTpr_Messagepubkey = StringUtil.Trim( AV18groupContact.gxTpr_Contactuserpubkey);
                     GXt_char4 = AV11error;
                     new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV7contact,  AV30sdt_message, out  GXt_char4) ;
                     AV11error = GXt_char4;
                     AssignAttri(sPrefix, false, "AV11error", AV11error);
                     if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                     {
                        GX_msglist.addItem("There was a problem sending the Message to the Group: "+AV11error);
                        if (true) break;
                     }
                  }
                  AV56GXV17 = (int)(AV56GXV17+1);
               }
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  /* Execute user subroutine: 'UPDATE MY GROUP' */
                  S142 ();
                  if (returnInSub) return;
               }
               else
               {
                  GX_msglist.addItem(AV11error);
               }
            }
            else
            {
               GX_msglist.addItem("There was a problem Signing the invitation: "+AV11error);
            }
         }
         else
         {
            GX_msglist.addItem(AV11error);
         }
      }

      protected void S142( )
      {
         /* 'UPDATE MY GROUP' Routine */
         returnInSub = false;
         GXt_char4 = AV11error;
         new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV16group_sdt_my,  StringUtil.Trim( AV16group_sdt_my.gxTpr_Encpassword), out  AV21grpupId, out  GXt_char4) ;
         AV11error = GXt_char4;
         AssignAttri(sPrefix, false, "AV11error", AV11error);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            AV5all_groups_sdt.Clear();
            AV5all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV11error), null);
            AV57GXV18 = 1;
            while ( AV57GXV18 <= AV5all_groups_sdt.Count )
            {
               AV15group_sdt_delete = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV5all_groups_sdt.Item(AV57GXV18));
               if ( AV15group_sdt_delete.gxTpr_Groupid == AV16group_sdt_my.gxTpr_Groupid )
               {
                  AV5all_groups_sdt.RemoveItem(AV5all_groups_sdt.IndexOf(AV15group_sdt_delete));
               }
               AV57GXV18 = (int)(AV57GXV18+1);
            }
            AV5all_groups_sdt.Add(AV16group_sdt_my, 0);
            new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "gropus.enc",  AV5all_groups_sdt.ToJSonString(false), out  AV11error) ;
            AssignAttri(sPrefix, false, "AV11error", AV11error);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
            {
               if ( AV24numOfSharedWasReach )
               {
                  AV38websession.Set("Group_EDIT", AV16group_sdt_my.ToJSonString(false, true));
                  this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Group Notification",(string)"The group has been RESTORED"}, true);
                  bttRestorewallet_Visible = 0;
                  AssignProp(sPrefix, false, bttRestorewallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttRestorewallet_Visible), 5, 0), true);
                  bttCreaterecoveredwallet_Visible = 1;
                  AssignProp(sPrefix, false, bttCreaterecoveredwallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttCreaterecoveredwallet_Visible), 5, 0), true);
                  bttRestorewallet_Visible = 0;
                  AssignProp(sPrefix, false, bttRestorewallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttRestorewallet_Visible), 5, 0), true);
                  context.DoAjaxRefreshForm();
               }
               else
               {
                  AV38websession.Set("Group_EDIT", "");
                  this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Group Notification",(string)"The group has been notified"}, true);
                  context.setWebReturnParms(new Object[] {});
                  context.setWebReturnParmsMetadata(new Object[] {});
                  context.wjLocDisableFrm = 1;
                  context.nUserReturn = 1;
                  returnInSub = true;
                  if (true) return;
               }
            }
            else
            {
               GX_msglist.addItem(AV11error);
            }
         }
         else
         {
            GX_msglist.addItem(AV11error);
         }
      }

      protected void E132N2( )
      {
         /* 'Create recovered wallet' Routine */
         returnInSub = false;
         AV38websession.Set("Group_Restore_ExtPrivKey", StringUtil.Trim( AV16group_sdt_my.gxTpr_Cleartextshare));
         context.PopUp(formatLink("wallet.registered.resotregroupwallet") , new Object[] {});
         /*  Sending Event outputs  */
      }

      protected void E142N2( )
      {
         /* Extensions\Web\Popup_Onpopupclosed Routine */
         returnInSub = false;
         AV12expectedPopupName = "Wallet.registered.ResotreGroupWallet";
         AV35strFound = (short)(StringUtil.StringSearch( AV28PopupName, StringUtil.Trim( StringUtil.Lower( AV12expectedPopupName)), 1));
         if ( AV35strFound > 0 )
         {
            AV37walletRestored = BooleanUtil.Val( AV38websession.Get("Group_Restore_Confirmed"));
            if ( AV37walletRestored )
            {
               CallWebObject(formatLink("wallet.returntowallets") );
               context.wjLocDisableFrm = 1;
            }
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
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
         PA2N2( ) ;
         WS2N2( ) ;
         WE2N2( ) ;
         cleanup();
         context.SetWrapped(false);
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      public override void componentbind( Object[] obj )
      {
         if ( IsUrlCreated( ) )
         {
            return  ;
         }
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA2N2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\registered\\timedwalletbackupnotowner", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA2N2( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
         }
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
      }

      public override void componentprocess( string sPPrefix ,
                                             string sPSFPrefix ,
                                             string sCompEvt )
      {
         sCompPrefix = sPPrefix;
         sSFPrefix = sPSFPrefix;
         sPrefix = sCompPrefix + sSFPrefix;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         INITWEB( ) ;
         nDraw = 0;
         PA2N2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS2N2( ) ;
         if ( isFullAjaxMode( ) )
         {
            componentdraw();
         }
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      public override void componentstart( )
      {
         if ( nDoneStart == 0 )
         {
            WCStart( ) ;
         }
      }

      protected void WCStart( )
      {
         nDraw = 1;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         WS2N2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
      }

      public override void componentdraw( )
      {
         if ( nDoneStart == 0 )
         {
            WCStart( ) ;
         }
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         WCParametersSet( ) ;
         WE2N2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      public override string getstring( string sGXControl )
      {
         string sCtrlName;
         if ( StringUtil.StrCmp(StringUtil.Substring( sGXControl, 1, 1), "&") == 0 )
         {
            sCtrlName = StringUtil.Substring( sGXControl, 2, StringUtil.Len( sGXControl)-1);
         }
         else
         {
            sCtrlName = sGXControl;
         }
         return cgiGet( sPrefix+"v"+StringUtil.Upper( sCtrlName)) ;
      }

      public override void componentjscripts( )
      {
         include_jscripts( ) ;
      }

      public override void componentthemes( )
      {
         define_styles( ) ;
      }

      protected void define_styles( )
      {
         AddStyleSheetFile("calendar-system.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202573011554458", true, true);
            idxLst = (int)(idxLst+1);
         }
         if ( ! outputEnabled )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
         CloseStyles();
         /* End function define_styles */
      }

      protected void include_jscripts( )
      {
         context.AddJavascriptSource("wallet/registered/timedwalletbackupnotowner.js", "?202573011554458", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_162( )
      {
         edtavCtlcontactid1_Internalname = sPrefix+"CTLCONTACTID1_"+sGXsfl_16_idx;
         edtavCtlcontactprivatename_Internalname = sPrefix+"CTLCONTACTPRIVATENAME_"+sGXsfl_16_idx;
         edtavCtlcontactusername_Internalname = sPrefix+"CTLCONTACTUSERNAME_"+sGXsfl_16_idx;
         edtavCtlnumshares_Internalname = sPrefix+"CTLNUMSHARES_"+sGXsfl_16_idx;
         edtavCtlcontactinvitationsent_Internalname = sPrefix+"CTLCONTACTINVITATIONSENT_"+sGXsfl_16_idx;
         edtavCtlcontactinvitacionaccepted_Internalname = sPrefix+"CTLCONTACTINVITACIONACCEPTED_"+sGXsfl_16_idx;
      }

      protected void SubsflControlProps_fel_162( )
      {
         edtavCtlcontactid1_Internalname = sPrefix+"CTLCONTACTID1_"+sGXsfl_16_fel_idx;
         edtavCtlcontactprivatename_Internalname = sPrefix+"CTLCONTACTPRIVATENAME_"+sGXsfl_16_fel_idx;
         edtavCtlcontactusername_Internalname = sPrefix+"CTLCONTACTUSERNAME_"+sGXsfl_16_fel_idx;
         edtavCtlnumshares_Internalname = sPrefix+"CTLNUMSHARES_"+sGXsfl_16_fel_idx;
         edtavCtlcontactinvitationsent_Internalname = sPrefix+"CTLCONTACTINVITATIONSENT_"+sGXsfl_16_fel_idx;
         edtavCtlcontactinvitacionaccepted_Internalname = sPrefix+"CTLCONTACTINVITACIONACCEPTED_"+sGXsfl_16_fel_idx;
      }

      protected void sendrow_162( )
      {
         sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
         SubsflControlProps_162( ) ;
         WB2N0( ) ;
         GridcontactsRow = GXWebRow.GetNew(context,GridcontactsContainer);
         if ( subGridcontacts_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subGridcontacts_Backstyle = 0;
            if ( StringUtil.StrCmp(subGridcontacts_Class, "") != 0 )
            {
               subGridcontacts_Linesclass = subGridcontacts_Class+"Odd";
            }
         }
         else if ( subGridcontacts_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subGridcontacts_Backstyle = 0;
            subGridcontacts_Backcolor = subGridcontacts_Allbackcolor;
            if ( StringUtil.StrCmp(subGridcontacts_Class, "") != 0 )
            {
               subGridcontacts_Linesclass = subGridcontacts_Class+"Uniform";
            }
         }
         else if ( subGridcontacts_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subGridcontacts_Backstyle = 1;
            if ( StringUtil.StrCmp(subGridcontacts_Class, "") != 0 )
            {
               subGridcontacts_Linesclass = subGridcontacts_Class+"Odd";
            }
            subGridcontacts_Backcolor = (int)(0x0);
         }
         else if ( subGridcontacts_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subGridcontacts_Backstyle = 1;
            if ( ((int)((nGXsfl_16_idx) % (2))) == 0 )
            {
               subGridcontacts_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridcontacts_Class, "") != 0 )
               {
                  subGridcontacts_Linesclass = subGridcontacts_Class+"Even";
               }
            }
            else
            {
               subGridcontacts_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridcontacts_Class, "") != 0 )
               {
                  subGridcontacts_Linesclass = subGridcontacts_Class+"Odd";
               }
            }
         }
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr ") ;
            context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
            context.WriteHtmlText( " gxrow=\""+sGXsfl_16_idx+"\">") ;
         }
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+"display:none;"+"\">") ;
         }
         /* Single line edit */
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactid1_Internalname,((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20groupContacts.Item(AV42GXV3)).gxTpr_Contactid.ToString(),((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20groupContacts.Item(AV42GXV3)).gxTpr_Contactid.ToString(),""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactid1_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtlcontactid1_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)36,(short)0,(short)0,(short)16,(short)0,(short)0,(short)0,(bool)true,(string)"",(string)"",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavCtlcontactprivatename_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 18,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',16)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactprivatename_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20groupContacts.Item(AV42GXV3)).gxTpr_Contactprivatename),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,18);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactprivatename_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(int)edtavCtlcontactprivatename_Visible,(int)edtavCtlcontactprivatename_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)16,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavCtlcontactusername_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',16)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactusername_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20groupContacts.Item(AV42GXV3)).gxTpr_Contactusername),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,19);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactusername_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(int)edtavCtlcontactusername_Visible,(int)edtavCtlcontactusername_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)16,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 20,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',16)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlnumshares_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20groupContacts.Item(AV42GXV3)).gxTpr_Numshares), 4, 0, ".", "")),StringUtil.LTrim( ((edtavCtlnumshares_Enabled!=0) ? context.localUtil.Format( (decimal)(((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20groupContacts.Item(AV42GXV3)).gxTpr_Numshares), "ZZZ9") : context.localUtil.Format( (decimal)(((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20groupContacts.Item(AV42GXV3)).gxTpr_Numshares), "ZZZ9")))," dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,20);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlnumshares_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlnumshares_Enabled,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)16,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 21,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',16)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactinvitationsent_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20groupContacts.Item(AV42GXV3)).gxTpr_Contactinvitationsent, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20groupContacts.Item(AV42GXV3)).gxTpr_Contactinvitationsent, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onblur(this,21);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactinvitationsent_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlcontactinvitationsent_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)16,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 22,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',16)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactinvitacionaccepted_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20groupContacts.Item(AV42GXV3)).gxTpr_Contactinvitacionaccepted, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV20groupContacts.Item(AV42GXV3)).gxTpr_Contactinvitacionaccepted, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onblur(this,22);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactinvitacionaccepted_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlcontactinvitacionaccepted_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)16,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         send_integrity_lvl_hashes2N2( ) ;
         GridcontactsContainer.AddRow(GridcontactsRow);
         nGXsfl_16_idx = ((subGridcontacts_Islastpage==1)&&(nGXsfl_16_idx+1>subGridcontacts_fnc_Recordsperpage( )) ? 1 : nGXsfl_16_idx+1);
         sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
         SubsflControlProps_162( ) ;
         /* End function sendrow_162 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl16( )
      {
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"GridcontactsContainer"+"DivS\" data-gxgridid=\"16\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGridcontacts_Internalname, subGridcontacts_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGridcontacts_Backcolorstyle == 0 )
            {
               subGridcontacts_Titlebackstyle = 0;
               if ( StringUtil.Len( subGridcontacts_Class) > 0 )
               {
                  subGridcontacts_Linesclass = subGridcontacts_Class+"Title";
               }
            }
            else
            {
               subGridcontacts_Titlebackstyle = 1;
               if ( subGridcontacts_Backcolorstyle == 1 )
               {
                  subGridcontacts_Titlebackcolor = subGridcontacts_Allbackcolor;
                  if ( StringUtil.Len( subGridcontacts_Class) > 0 )
                  {
                     subGridcontacts_Linesclass = subGridcontacts_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGridcontacts_Class) > 0 )
                  {
                     subGridcontacts_Linesclass = subGridcontacts_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "contact Id") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtavCtlcontactprivatename_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "Contact  Name") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtavCtlcontactusername_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "Contact Name") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Number of votes") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Invitation Sent") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Invitacion Accepted") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridcontactsContainer.AddObjectProperty("GridName", "Gridcontacts");
         }
         else
         {
            GridcontactsContainer.AddObjectProperty("GridName", "Gridcontacts");
            GridcontactsContainer.AddObjectProperty("Header", subGridcontacts_Header);
            GridcontactsContainer.AddObjectProperty("Class", "Grid");
            GridcontactsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridcontactsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridcontactsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridcontacts_Backcolorstyle), 1, 0, ".", "")));
            GridcontactsContainer.AddObjectProperty("CmpContext", sPrefix);
            GridcontactsContainer.AddObjectProperty("InMasterPage", "false");
            GridcontactsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridcontactsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlcontactid1_Enabled), 5, 0, ".", "")));
            GridcontactsContainer.AddColumnProperties(GridcontactsColumn);
            GridcontactsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridcontactsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlcontactprivatename_Enabled), 5, 0, ".", "")));
            GridcontactsColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlcontactprivatename_Visible), 5, 0, ".", "")));
            GridcontactsContainer.AddColumnProperties(GridcontactsColumn);
            GridcontactsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridcontactsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlcontactusername_Enabled), 5, 0, ".", "")));
            GridcontactsColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlcontactusername_Visible), 5, 0, ".", "")));
            GridcontactsContainer.AddColumnProperties(GridcontactsColumn);
            GridcontactsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridcontactsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlnumshares_Enabled), 5, 0, ".", "")));
            GridcontactsContainer.AddColumnProperties(GridcontactsColumn);
            GridcontactsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridcontactsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlcontactinvitationsent_Enabled), 5, 0, ".", "")));
            GridcontactsContainer.AddColumnProperties(GridcontactsColumn);
            GridcontactsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridcontactsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlcontactinvitacionaccepted_Enabled), 5, 0, ".", "")));
            GridcontactsContainer.AddColumnProperties(GridcontactsColumn);
            GridcontactsContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridcontacts_Selectedindex), 4, 0, ".", "")));
            GridcontactsContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridcontacts_Allowselection), 1, 0, ".", "")));
            GridcontactsContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridcontacts_Selectioncolor), 9, 0, ".", "")));
            GridcontactsContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridcontacts_Allowhovering), 1, 0, ".", "")));
            GridcontactsContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridcontacts_Hoveringcolor), 9, 0, ".", "")));
            GridcontactsContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridcontacts_Allowcollapsing), 1, 0, ".", "")));
            GridcontactsContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridcontacts_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         edtavCtldate_Internalname = sPrefix+"CTLDATE";
         edtavCtlminimumshares_Internalname = sPrefix+"CTLMINIMUMSHARES";
         edtavCtlcontactid1_Internalname = sPrefix+"CTLCONTACTID1";
         edtavCtlcontactprivatename_Internalname = sPrefix+"CTLCONTACTPRIVATENAME";
         edtavCtlcontactusername_Internalname = sPrefix+"CTLCONTACTUSERNAME";
         edtavCtlnumshares_Internalname = sPrefix+"CTLNUMSHARES";
         edtavCtlcontactinvitationsent_Internalname = sPrefix+"CTLCONTACTINVITATIONSENT";
         edtavCtlcontactinvitacionaccepted_Internalname = sPrefix+"CTLCONTACTINVITACIONACCEPTED";
         bttClose1_Internalname = sPrefix+"CLOSE1";
         bttRestorewallet_Internalname = sPrefix+"RESTOREWALLET";
         bttCreaterecoveredwallet_Internalname = sPrefix+"CREATERECOVEREDWALLET";
         divMaintable_Internalname = sPrefix+"MAINTABLE";
         Form.Internalname = sPrefix+"FORM";
         subGridcontacts_Internalname = sPrefix+"GRIDCONTACTS";
      }

      public override void initialize_properties( )
      {
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
         }
         init_default_properties( ) ;
         subGridcontacts_Allowcollapsing = 0;
         subGridcontacts_Allowselection = 0;
         subGridcontacts_Header = "";
         edtavCtlcontactinvitacionaccepted_Jsonclick = "";
         edtavCtlcontactinvitacionaccepted_Enabled = 0;
         edtavCtlcontactinvitationsent_Jsonclick = "";
         edtavCtlcontactinvitationsent_Enabled = 0;
         edtavCtlnumshares_Jsonclick = "";
         edtavCtlnumshares_Enabled = 0;
         edtavCtlcontactusername_Jsonclick = "";
         edtavCtlcontactusername_Enabled = 0;
         edtavCtlcontactusername_Visible = -1;
         edtavCtlcontactprivatename_Jsonclick = "";
         edtavCtlcontactprivatename_Enabled = 0;
         edtavCtlcontactprivatename_Visible = -1;
         edtavCtlcontactid1_Jsonclick = "";
         edtavCtlcontactid1_Enabled = 0;
         subGridcontacts_Class = "Grid";
         subGridcontacts_Backcolorstyle = 0;
         bttCreaterecoveredwallet_Visible = 1;
         bttRestorewallet_Visible = 1;
         edtavCtlminimumshares_Jsonclick = "";
         edtavCtlminimumshares_Enabled = 0;
         edtavCtldate_Jsonclick = "";
         edtavCtldate_Enabled = 0;
         edtavCtldate_Visible = 1;
         edtavCtlcontactinvitacionaccepted_Enabled = -1;
         edtavCtlcontactinvitationsent_Enabled = -1;
         edtavCtlnumshares_Enabled = -1;
         edtavCtlcontactusername_Enabled = -1;
         edtavCtlcontactprivatename_Enabled = -1;
         edtavCtlcontactid1_Enabled = -1;
         edtavCtlminimumshares_Enabled = -1;
         edtavCtldate_Enabled = -1;
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               enableJsOutput();
            }
         }
      }

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"GRIDCONTACTS_nEOF","type":"int"},{"av":"AV20groupContacts","fld":"vGROUPCONTACTS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"nRC_GXsfl_16","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":16,"type":"int"},{"av":"AV16group_sdt_my","fld":"vGROUP_SDT_MY","type":""},{"av":"sPrefix","type":"char"},{"av":"AV6based64Key","fld":"vBASED64KEY","hsh":true,"type":"char"},{"av":"AV13externalUser","fld":"vEXTERNALUSER","hsh":true,"type":""},{"av":"AV39decryptedSecret","fld":"vDECRYPTEDSECRET","hsh":true,"type":"vchar"},{"av":"GXV2","fld":"CTLMINIMUMSHARES","pic":"ZZZ9","type":"int"}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV13externalUser","fld":"vEXTERNALUSER","hsh":true,"type":""},{"av":"AV16group_sdt_my","fld":"vGROUP_SDT_MY","type":""},{"ctrl":"RESTOREWALLET","prop":"Visible"},{"ctrl":"CREATERECOVEREDWALLET","prop":"Visible"},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"AV14group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV20groupContacts","fld":"vGROUPCONTACTS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":16,"type":"int"},{"ctrl":"CTLDATE","prop":"Visible"},{"av":"AV27oneTimeConstrain","fld":"vONETIMECONSTRAIN","type":""},{"av":"AV39decryptedSecret","fld":"vDECRYPTEDSECRET","hsh":true,"type":"vchar"}]}""");
         setEventMetadata("GRIDCONTACTS.LOAD","""{"handler":"E162N2","iparms":[{"av":"AV16group_sdt_my","fld":"vGROUP_SDT_MY","type":""}]""");
         setEventMetadata("GRIDCONTACTS.LOAD",""","oparms":[{"ctrl":"CTLCONTACTPRIVATENAME","prop":"Visible"},{"ctrl":"CTLCONTACTUSERNAME","prop":"Visible"}]}""");
         setEventMetadata("'CLOSE'","""{"handler":"E112N2","iparms":[]}""");
         setEventMetadata("'RESTORE WALLET'","""{"handler":"E122N2","iparms":[{"av":"AV14group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV13externalUser","fld":"vEXTERNALUSER","hsh":true,"type":""},{"av":"AV16group_sdt_my","fld":"vGROUP_SDT_MY","type":""},{"av":"AV33sharesToRecover","fld":"vSHARESTORECOVER","type":""},{"av":"AV39decryptedSecret","fld":"vDECRYPTEDSECRET","hsh":true,"type":"vchar"},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"AV23message_signature","fld":"vMESSAGE_SIGNATURE","type":""},{"av":"AV30sdt_message","fld":"vSDT_MESSAGE","type":""},{"av":"AV24numOfSharedWasReach","fld":"vNUMOFSHAREDWASREACH","type":"boolean"}]""");
         setEventMetadata("'RESTORE WALLET'",""","oparms":[{"av":"AV24numOfSharedWasReach","fld":"vNUMOFSHAREDWASREACH","type":"boolean"},{"av":"AV33sharesToRecover","fld":"vSHARESTORECOVER","type":""},{"av":"AV16group_sdt_my","fld":"vGROUP_SDT_MY","type":""},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"AV23message_signature","fld":"vMESSAGE_SIGNATURE","type":""},{"av":"AV30sdt_message","fld":"vSDT_MESSAGE","type":""},{"ctrl":"RESTOREWALLET","prop":"Visible"},{"ctrl":"CREATERECOVEREDWALLET","prop":"Visible"}]}""");
         setEventMetadata("'CREATE RECOVERED WALLET'","""{"handler":"E132N2","iparms":[{"av":"AV16group_sdt_my","fld":"vGROUP_SDT_MY","type":""}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED","""{"handler":"E142N2","iparms":[{"av":"AV28PopupName","fld":"vPOPUPNAME","type":"char"}]}""");
         setEventMetadata("VALIDV_GXV4","""{"handler":"Validv_Gxv4","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gxv9","iparms":[]}""");
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
         AV16group_sdt_my = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         AV6based64Key = "";
         AV13externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV39decryptedSecret = "";
         AV14group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         forbiddenHiddens = new GXProperties();
         AV27oneTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         AV20groupContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem>( context, "Group_SDT.ContactItem", "distributedcryptography");
         AV33sharesToRecover = new GxSimpleCollection<string>();
         AV11error = "";
         AV23message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         AV30sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV28PopupName = "";
         GX_FocusControl = "";
         TempTags = "";
         GridcontactsContainer = new GXWebGrid( context);
         sStyleString = "";
         ClassString = "";
         StyleString = "";
         bttClose1_Jsonclick = "";
         bttRestorewallet_Jsonclick = "";
         bttCreaterecoveredwallet_Jsonclick = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         hsh = "";
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV38websession = context.GetSession();
         AV18groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         GXt_SdtGroup_SDT_TimeConstrainItem3 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         AV31secret = "";
         AV9DecryptionResult = new GeneXus.Programs.distributedcryptographylib.SdtDecryptionResult(context);
         AV10EncryptionService = new GeneXus.Programs.distributedcryptographylib.SdtEncryptionService(context);
         GridcontactsRow = new GXWebRow();
         AV19groupContactMy = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV34sharesToRecoverTemp = new GxSimpleCollection<string>();
         AV26oneShare = "";
         AV17group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV25oneGroupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV32share = "";
         AV36userShares = new GxSimpleCollection<string>();
         AV29recoveredSecret = "";
         GXt_char2 = "";
         AV7contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         GXt_char4 = "";
         AV21grpupId = Guid.Empty;
         AV5all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV15group_sdt_delete = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV12expectedPopupName = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGridcontacts_Linesclass = "";
         ROClassString = "";
         GridcontactsColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtldate_Enabled = 0;
         edtavCtlminimumshares_Enabled = 0;
         edtavCtlcontactid1_Enabled = 0;
         edtavCtlcontactprivatename_Enabled = 0;
         edtavCtlcontactusername_Enabled = 0;
         edtavCtlnumshares_Enabled = 0;
         edtavCtlcontactinvitationsent_Enabled = 0;
         edtavCtlcontactinvitacionaccepted_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGridcontacts_Backcolorstyle ;
      private short GRIDCONTACTS_nEOF ;
      private short AV35strFound ;
      private short nGXWrapped ;
      private short subGridcontacts_Backstyle ;
      private short subGridcontacts_Titlebackstyle ;
      private short subGridcontacts_Allowselection ;
      private short subGridcontacts_Allowhovering ;
      private short subGridcontacts_Allowcollapsing ;
      private short subGridcontacts_Collapsed ;
      private int nRC_GXsfl_16 ;
      private int nGXsfl_16_idx=1 ;
      private int edtavCtldate_Enabled ;
      private int edtavCtlminimumshares_Enabled ;
      private int edtavCtlcontactid1_Enabled ;
      private int edtavCtlcontactprivatename_Enabled ;
      private int edtavCtlcontactusername_Enabled ;
      private int edtavCtlnumshares_Enabled ;
      private int edtavCtlcontactinvitationsent_Enabled ;
      private int edtavCtlcontactinvitacionaccepted_Enabled ;
      private int edtavCtldate_Visible ;
      private int AV42GXV3 ;
      private int bttRestorewallet_Visible ;
      private int bttCreaterecoveredwallet_Visible ;
      private int subGridcontacts_Islastpage ;
      private int nGXsfl_16_fel_idx=1 ;
      private int AV49GXV10 ;
      private int edtavCtlcontactprivatename_Visible ;
      private int edtavCtlcontactusername_Visible ;
      private int AV50GXV11 ;
      private int AV51GXV12 ;
      private int AV52GXV13 ;
      private int AV53GXV14 ;
      private int AV54GXV15 ;
      private int AV55GXV16 ;
      private int AV56GXV17 ;
      private int AV57GXV18 ;
      private int idxLst ;
      private int subGridcontacts_Backcolor ;
      private int subGridcontacts_Allbackcolor ;
      private int subGridcontacts_Titlebackcolor ;
      private int subGridcontacts_Selectedindex ;
      private int subGridcontacts_Selectioncolor ;
      private int subGridcontacts_Hoveringcolor ;
      private long GRIDCONTACTS_nCurrentRecord ;
      private long GRIDCONTACTS_nFirstRecordOnPage ;
      private long GXt_int5 ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_16_idx="0001" ;
      private string AV6based64Key ;
      private string edtavCtldate_Internalname ;
      private string edtavCtlminimumshares_Internalname ;
      private string edtavCtlcontactid1_Internalname ;
      private string edtavCtlcontactprivatename_Internalname ;
      private string edtavCtlcontactusername_Internalname ;
      private string edtavCtlnumshares_Internalname ;
      private string edtavCtlcontactinvitationsent_Internalname ;
      private string edtavCtlcontactinvitacionaccepted_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV11error ;
      private string AV28PopupName ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string TempTags ;
      private string edtavCtldate_Jsonclick ;
      private string edtavCtlminimumshares_Jsonclick ;
      private string sStyleString ;
      private string subGridcontacts_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string bttClose1_Internalname ;
      private string bttClose1_Jsonclick ;
      private string bttRestorewallet_Internalname ;
      private string bttRestorewallet_Jsonclick ;
      private string bttCreaterecoveredwallet_Internalname ;
      private string bttCreaterecoveredwallet_Jsonclick ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string sGXsfl_16_fel_idx="0001" ;
      private string hsh ;
      private string AV31secret ;
      private string AV29recoveredSecret ;
      private string GXt_char2 ;
      private string GXt_char4 ;
      private string AV12expectedPopupName ;
      private string subGridcontacts_Class ;
      private string subGridcontacts_Linesclass ;
      private string ROClassString ;
      private string edtavCtlcontactid1_Jsonclick ;
      private string edtavCtlcontactprivatename_Jsonclick ;
      private string edtavCtlcontactusername_Jsonclick ;
      private string edtavCtlnumshares_Jsonclick ;
      private string edtavCtlcontactinvitationsent_Jsonclick ;
      private string edtavCtlcontactinvitacionaccepted_Jsonclick ;
      private string subGridcontacts_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_16_Refreshing=false ;
      private bool AV24numOfSharedWasReach ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool gx_refresh_fired ;
      private bool returnInSub ;
      private bool gx_BV16 ;
      private bool AV22isActive ;
      private bool AV8contactFound ;
      private bool AV37walletRestored ;
      private string AV39decryptedSecret ;
      private string AV26oneShare ;
      private string AV32share ;
      private Guid AV21grpupId ;
      private IGxSession AV38websession ;
      private GXProperties forbiddenHiddens ;
      private GXWebGrid GridcontactsContainer ;
      private GXWebRow GridcontactsRow ;
      private GXWebColumn GridcontactsColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV16group_sdt_my ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV13externalUser ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV14group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem AV27oneTimeConstrain ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem> AV20groupContacts ;
      private GxSimpleCollection<string> AV33sharesToRecover ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV23message_signature ;
      private GeneXus.Programs.nostr.SdtSDT_message AV30sdt_message ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV18groupContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem GXt_SdtGroup_SDT_TimeConstrainItem3 ;
      private GeneXus.Programs.distributedcryptographylib.SdtDecryptionResult AV9DecryptionResult ;
      private GeneXus.Programs.distributedcryptographylib.SdtEncryptionService AV10EncryptionService ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV19groupContactMy ;
      private GxSimpleCollection<string> AV34sharesToRecoverTemp ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV17group_sdt_temp ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV25oneGroupContact ;
      private GxSimpleCollection<string> AV36userShares ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV7contact ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV5all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV15group_sdt_delete ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
