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
   public class smartgroupwalletbackupnotowner : GXWebComponent
   {
      public smartgroupwalletbackupnotowner( )
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

      public smartgroupwalletbackupnotowner( IGxContext context )
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
         ajax_req_read_hidden_sdt(GetNextPar( ), AV66view);
         AV66view.gxTpr_Minimumshares = (short)(Math.Round(NumberUtil.Val( GetNextPar( ), "."), 18, MidpointRounding.ToEven));
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridcontacts_refresh( AV66view, AV66view.gxTpr_Minimumshares, sPrefix) ;
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
            PA1L2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavCtlminimumshares_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlminimumshares_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlminimumshares_Enabled), 5, 0), true);
               edtavRestorestatus_Enabled = 0;
               AssignProp(sPrefix, false, edtavRestorestatus_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavRestorestatus_Enabled), 5, 0), true);
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
               WS1L2( ) ;
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
            context.SendWebValue( "Smart Group Wallet Backup Not Owner") ;
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true, false);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("calendar-en.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
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
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.smartgroupwalletbackupnotowner") +"\">") ;
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
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", sPrefix+"hsh"+"SmartGroupWalletBackupNotOwner");
         forbiddenHiddens.Add("GXV1", context.localUtil.Format( (decimal)(AV66view.gxTpr_Minimumshares), "ZZZ9"));
         GxWebStd.gx_hidden_field( context, sPrefix+"hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("wallet\\registered\\smartgroupwalletbackupnotowner:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"View", AV66view);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"View", AV66view);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Groupcontacts", AV18groupContacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Groupcontacts", AV18groupContacts);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_16", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_16), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vPOPUPNAME", StringUtil.RTrim( AV34PopupName));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vGROUPCONTACTS", AV18groupContacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vGROUPCONTACTS", AV18groupContacts);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vVIEW", AV66view);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vVIEW", AV66view);
         }
      }

      protected void RenderHtmlCloseForm1L2( )
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
         return "Wallet.registered.SmartGroupWalletBackupNotOwner" ;
      }

      public override string GetPgmdesc( )
      {
         return "Smart Group Wallet Backup Not Owner" ;
      }

      protected void WB1L0( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.registered.smartgroupwalletbackupnotowner");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCtlminimumshares_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCtlminimumshares_Internalname, "Minimum amount of votes to recover the backup", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCtlminimumshares_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV66view.gxTpr_Minimumshares), 4, 0, ".", "")), StringUtil.LTrim( ((edtavCtlminimumshares_Enabled!=0) ? context.localUtil.Format( (decimal)(AV66view.gxTpr_Minimumshares), "ZZZ9") : context.localUtil.Format( (decimal)(AV66view.gxTpr_Minimumshares), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,8);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtlminimumshares_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtlminimumshares_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/registered/SmartGroupWalletBackupNotOwner.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavRestorestatus_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavRestorestatus_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavRestorestatus_Internalname, "Restore status", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavRestorestatus_Internalname, StringUtil.RTrim( AV60restoreStatus), "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,13);\"", 0, edtavRestorestatus_Visible, edtavRestorestatus_Enabled, 0, 80, "chr", 4, "row", 0, StyleString, ClassString, "", "", "250", -1, 0, "", "", -1, true, "", "'"+sPrefix+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/SmartGroupWalletBackupNotOwner.htm");
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
               AV69GXV2 = nGXsfl_16_idx;
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
            GxWebStd.gx_button_ctrl( context, bttClose1_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(16), 2, 0)+","+"null"+");", "Close", bttClose1_Jsonclick, 5, "Close", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'CLOSE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SmartGroupWalletBackupNotOwner.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 28,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttRestorewallet_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(16), 2, 0)+","+"null"+");", "Restore Wallet", bttRestorewallet_Jsonclick, 5, "Restore Wallet", "", StyleString, ClassString, bttRestorewallet_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'RESTORE WALLET\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SmartGroupWalletBackupNotOwner.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 31,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCreaterecoveredwallet_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(16), 2, 0)+","+"null"+");", "Create recovered wallet", bttCreaterecoveredwallet_Jsonclick, 5, "Create recovered wallet", "", StyleString, ClassString, bttCreaterecoveredwallet_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'CREATE RECOVERED WALLET\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SmartGroupWalletBackupNotOwner.htm");
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
                  AV69GXV2 = nGXsfl_16_idx;
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

      protected void START1L2( )
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
                  Form.Meta.addItem("generator", "GeneXus .NET 18_0_16-189595", 0) ;
               }
            }
            Form.Meta.addItem("description", "Smart Group Wallet Backup Not Owner", 0) ;
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
               STRUP1L0( ) ;
            }
         }
      }

      protected void WS1L2( )
      {
         START1L2( ) ;
         EVT1L2( ) ;
      }

      protected void EVT1L2( )
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
                                 STRUP1L0( ) ;
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
                                 STRUP1L0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Close' */
                                    E111L2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'RESTORE WALLET'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1L0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Restore Wallet' */
                                    E121L2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CREATE RECOVERED WALLET'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1L0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Create recovered wallet' */
                                    E131L2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1L0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E141L2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1L0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavCtlminimumshares_Internalname;
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
                                 STRUP1L0( ) ;
                              }
                              nGXsfl_16_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
                              SubsflControlProps_162( ) ;
                              AV69GXV2 = nGXsfl_16_idx;
                              if ( ( AV18groupContacts.Count >= AV69GXV2 ) && ( AV69GXV2 > 0 ) )
                              {
                                 AV18groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV18groupContacts.Item(AV69GXV2));
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
                                          GX_FocusControl = edtavCtlminimumshares_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Refresh */
                                          E151L2 ();
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
                                          GX_FocusControl = edtavCtlminimumshares_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Gridcontacts.Load */
                                          E161L2 ();
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
                                       STRUP1L0( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlminimumshares_Internalname;
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

      protected void WE1L2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm1L2( ) ;
            }
         }
      }

      protected void PA1L2( )
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
               GX_FocusControl = edtavCtlminimumshares_Internalname;
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

      protected void gxgrGridcontacts_refresh( GeneXus.Programs.wallet.registered.SdtWalletBackupView AV66view ,
                                               short GXV1 ,
                                               string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDCONTACTS_nCurrentRecord = 0;
         RF1L2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", sPrefix+"hsh"+"SmartGroupWalletBackupNotOwner");
         forbiddenHiddens.Add("GXV1", context.localUtil.Format( (decimal)(AV66view.gxTpr_Minimumshares), "ZZZ9"));
         GxWebStd.gx_hidden_field( context, sPrefix+"hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("wallet\\registered\\smartgroupwalletbackupnotowner:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
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
         RF1L2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtlminimumshares_Enabled = 0;
         AssignProp(sPrefix, false, edtavCtlminimumshares_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlminimumshares_Enabled), 5, 0), true);
         edtavRestorestatus_Enabled = 0;
         AssignProp(sPrefix, false, edtavRestorestatus_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavRestorestatus_Enabled), 5, 0), true);
         edtavCtlcontactid1_Enabled = 0;
         edtavCtlcontactprivatename_Enabled = 0;
         edtavCtlcontactusername_Enabled = 0;
         edtavCtlnumshares_Enabled = 0;
         edtavCtlcontactinvitationsent_Enabled = 0;
         edtavCtlcontactinvitacionaccepted_Enabled = 0;
      }

      protected void RF1L2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridcontactsContainer.ClearRows();
         }
         wbStart = 16;
         /* Execute user event: Refresh */
         E151L2 ();
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
            E161L2 ();
            wbEnd = 16;
            WB1L0( ) ;
         }
         bGXsfl_16_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes1L2( )
      {
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
         edtavCtlminimumshares_Enabled = 0;
         AssignProp(sPrefix, false, edtavCtlminimumshares_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlminimumshares_Enabled), 5, 0), true);
         edtavRestorestatus_Enabled = 0;
         AssignProp(sPrefix, false, edtavRestorestatus_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavRestorestatus_Enabled), 5, 0), true);
         edtavCtlcontactid1_Enabled = 0;
         edtavCtlcontactprivatename_Enabled = 0;
         edtavCtlcontactusername_Enabled = 0;
         edtavCtlnumshares_Enabled = 0;
         edtavCtlcontactinvitationsent_Enabled = 0;
         edtavCtlcontactinvitacionaccepted_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP1L0( )
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
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vVIEW"), AV66view);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"View"), AV66view);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Groupcontacts"), AV18groupContacts);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vGROUPCONTACTS"), AV18groupContacts);
            /* Read saved values. */
            nRC_GXsfl_16 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_16"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_16 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_16"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_16_fel_idx = 0;
            while ( nGXsfl_16_fel_idx < nRC_GXsfl_16 )
            {
               nGXsfl_16_fel_idx = ((subGridcontacts_Islastpage==1)&&(nGXsfl_16_fel_idx+1>subGridcontacts_fnc_Recordsperpage( )) ? 1 : nGXsfl_16_fel_idx+1);
               sGXsfl_16_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_162( ) ;
               AV69GXV2 = nGXsfl_16_fel_idx;
               if ( ( AV18groupContacts.Count >= AV69GXV2 ) && ( AV69GXV2 > 0 ) )
               {
                  AV18groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV18groupContacts.Item(AV69GXV2));
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
            if ( ( ( context.localUtil.CToN( cgiGet( edtavCtlminimumshares_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavCtlminimumshares_Internalname), ".", ",") > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CTLMINIMUMSHARES");
               GX_FocusControl = edtavCtlminimumshares_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV66view.gxTpr_Minimumshares = 0;
            }
            else
            {
               AV66view.gxTpr_Minimumshares = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavCtlminimumshares_Internalname), ".", ","), 18, MidpointRounding.ToEven));
            }
            AV60restoreStatus = cgiGet( edtavRestorestatus_Internalname);
            AssignAttri(sPrefix, false, "AV60restoreStatus", AV60restoreStatus);
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            forbiddenHiddens = new GXProperties();
            forbiddenHiddens.Add("hshsalt", sPrefix+"hsh"+"SmartGroupWalletBackupNotOwner");
            AV66view.gxTpr_Minimumshares = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavCtlminimumshares_Internalname), ".", ","), 18, MidpointRounding.ToEven));
            forbiddenHiddens.Add("GXV1", context.localUtil.Format( (decimal)(AV66view.gxTpr_Minimumshares), "ZZZ9"));
            hsh = cgiGet( sPrefix+"hsh");
            if ( ! GXUtil.CheckEncryptedHash( forbiddenHiddens.ToString(), hsh, GXKey) )
            {
               GXUtil.WriteLogError("wallet\\registered\\smartgroupwalletbackupnotowner:[ SecurityCheckFailed (403 Forbidden) value for]"+forbiddenHiddens.ToJSonString());
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

      protected void E151L2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         GXt_SdtGroupListItem1 = AV64groupView;
         new GeneXus.Programs.wallet.registered.getgroupeditview(context ).execute( out  GXt_SdtGroupListItem1) ;
         AV64groupView = GXt_SdtGroupListItem1;
         GXt_char2 = AV10error;
         new GeneXus.Programs.wallet.registered.getwalletbackupview(context ).execute(  AV64groupView.gxTpr_Groupid, out  AV66view, out  GXt_char2) ;
         AV10error = GXt_char2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            GX_msglist.addItem(AV10error);
         }
         AV18groupContacts.Clear();
         gx_BV16 = true;
         AV76GXV9 = 1;
         while ( AV76GXV9 <= AV66view.gxTpr_Contact.Count )
         {
            AV19groupContact = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV66view.gxTpr_Contact.Item(AV76GXV9));
            AV18groupContacts.Add(AV19groupContact, 0);
            gx_BV16 = true;
            AV76GXV9 = (int)(AV76GXV9+1);
         }
         AV66view.gxTpr_Contact.Clear();
         if ( AV66view.gxTpr_Restorestopped )
         {
            AV60restoreStatus = "The owner of this wallet STOPPED the restore on " + context.localUtil.TToC( AV66view.gxTpr_Restorestoppeddatetime, 8, 5, 1, 2, "/", ":", " ");
            AssignAttri(sPrefix, false, "AV60restoreStatus", AV60restoreStatus);
            edtavRestorestatus_Visible = 1;
            AssignProp(sPrefix, false, edtavRestorestatus_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavRestorestatus_Visible), 5, 0), true);
            bttRestorewallet_Visible = 0;
            AssignProp(sPrefix, false, bttRestorewallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttRestorewallet_Visible), 5, 0), true);
            bttCreaterecoveredwallet_Visible = 0;
            AssignProp(sPrefix, false, bttCreaterecoveredwallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttCreaterecoveredwallet_Visible), 5, 0), true);
         }
         else
         {
            AV60restoreStatus = "";
            AssignAttri(sPrefix, false, "AV60restoreStatus", AV60restoreStatus);
            edtavRestorestatus_Visible = 0;
            AssignProp(sPrefix, false, edtavRestorestatus_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavRestorestatus_Visible), 5, 0), true);
            if ( AV66view.gxTpr_Isactive && ! AV66view.gxTpr_Numofsharesreached )
            {
               bttRestorewallet_Visible = 1;
               AssignProp(sPrefix, false, bttRestorewallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttRestorewallet_Visible), 5, 0), true);
            }
            else
            {
               bttRestorewallet_Visible = 0;
               AssignProp(sPrefix, false, bttRestorewallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttRestorewallet_Visible), 5, 0), true);
            }
            if ( AV66view.gxTpr_Numofsharesreached )
            {
               bttCreaterecoveredwallet_Visible = 1;
               AssignProp(sPrefix, false, bttCreaterecoveredwallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttCreaterecoveredwallet_Visible), 5, 0), true);
            }
            else
            {
               bttCreaterecoveredwallet_Visible = 0;
               AssignProp(sPrefix, false, bttCreaterecoveredwallet_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttCreaterecoveredwallet_Visible), 5, 0), true);
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV66view", AV66view);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV18groupContacts", AV18groupContacts);
      }

      private void E161L2( )
      {
         /* Gridcontacts_Load Routine */
         returnInSub = false;
         AV69GXV2 = 1;
         while ( AV69GXV2 <= AV18groupContacts.Count )
         {
            AV18groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV18groupContacts.Item(AV69GXV2));
            if ( AV66view.gxTpr_Amigroupowner )
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
            AV69GXV2 = (int)(AV69GXV2+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E111L2( )
      {
         /* 'Close' Routine */
         returnInSub = false;
         AV6websession.Set("Group_EDIT", "");
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
         /*  Sending Event outputs  */
      }

      protected void E121L2( )
      {
         /* 'Restore Wallet' Routine */
         returnInSub = false;
         GXt_char2 = AV10error;
         new GeneXus.Programs.wallet.registered.signwalletbackuprestore(context ).execute(  AV66view.gxTpr_Groupid, out  AV65restored, out  AV67warning, out  GXt_char2) ;
         AV10error = GXt_char2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV67warning)) )
         {
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"warning",(string)"Group Notification",(string)AV67warning}, true);
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            GX_msglist.addItem(AV10error);
         }
         else
         {
            if ( AV65restored )
            {
               this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Group Notification",(string)"The group has been RESTORED"}, true);
               context.DoAjaxRefreshCmp(sPrefix);
            }
            else
            {
               AV6websession.Set("Group_EDIT", "");
               this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Group Notification",(string)"The group has been notified"}, true);
               context.setWebReturnParms(new Object[] {});
               context.setWebReturnParmsMetadata(new Object[] {});
               context.wjLocDisableFrm = 1;
               context.nUserReturn = 1;
               returnInSub = true;
               if (true) return;
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV66view", AV66view);
         if ( gx_BV16 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV18groupContacts", AV18groupContacts);
            nGXsfl_16_bak_idx = nGXsfl_16_idx;
            gxgrGridcontacts_refresh( AV66view, AV66view.gxTpr_Minimumshares, sPrefix) ;
            nGXsfl_16_idx = nGXsfl_16_bak_idx;
            sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
            SubsflControlProps_162( ) ;
         }
      }

      protected void E131L2( )
      {
         /* 'Create recovered wallet' Routine */
         returnInSub = false;
         GXt_char2 = AV10error;
         new GeneXus.Programs.wallet.registered.checkgrouprestoreallowed(context ).execute(  AV66view.gxTpr_Groupid, out  GXt_char2) ;
         AV10error = GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            context.PopUp(formatLink("wallet.registered.resotregroupwallet", new object[] {UrlEncode(AV66view.gxTpr_Groupid.ToString())}, new string[] {"groupId"}) , new Object[] {});
         }
         else
         {
            GX_msglist.addItem(AV10error);
         }
      }

      protected void E141L2( )
      {
         /* Extensions\Web\Popup_Onpopupclosed Routine */
         returnInSub = false;
         AV24expectedPopupName = "Wallet.registered.ResotreGroupWallet";
         AV54strFound = (short)(StringUtil.StringSearch( AV34PopupName, StringUtil.Trim( StringUtil.Lower( AV24expectedPopupName)), 1));
         if ( AV54strFound > 0 )
         {
            AV53walletRestored = BooleanUtil.Val( AV6websession.Get("Group_Restore_Confirmed"));
            if ( AV53walletRestored )
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
         PA1L2( ) ;
         WS1L2( ) ;
         WE1L2( ) ;
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
         PA1L2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\registered\\smartgroupwalletbackupnotowner", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA1L2( ) ;
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
         PA1L2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS1L2( ) ;
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
         WS1L2( ) ;
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
         WE1L2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202610714152536", true, true, false);
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
         context.AddJavascriptSource("wallet/registered/smartgroupwalletbackupnotowner.js", "?202610714152536", false, true, false);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true, false);
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
         WB1L0( ) ;
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
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactid1_Internalname,((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV18groupContacts.Item(AV69GXV2)).gxTpr_Contactid.ToString(),((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV18groupContacts.Item(AV69GXV2)).gxTpr_Contactid.ToString(),""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactid1_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtlcontactid1_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)36,(short)0,(short)0,(short)16,(short)0,(short)0,(short)0,(bool)true,(string)"",(string)"",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavCtlcontactprivatename_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 18,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',16)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactprivatename_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV18groupContacts.Item(AV69GXV2)).gxTpr_Contactprivatename),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,18);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactprivatename_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(int)edtavCtlcontactprivatename_Visible,(int)edtavCtlcontactprivatename_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)16,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavCtlcontactusername_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',16)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactusername_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV18groupContacts.Item(AV69GXV2)).gxTpr_Contactusername),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,19);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactusername_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(int)edtavCtlcontactusername_Visible,(int)edtavCtlcontactusername_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)16,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 20,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',16)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlnumshares_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV18groupContacts.Item(AV69GXV2)).gxTpr_Numshares), 4, 0, ".", "")),StringUtil.LTrim( ((edtavCtlnumshares_Enabled!=0) ? context.localUtil.Format( (decimal)(((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV18groupContacts.Item(AV69GXV2)).gxTpr_Numshares), "ZZZ9") : context.localUtil.Format( (decimal)(((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV18groupContacts.Item(AV69GXV2)).gxTpr_Numshares), "ZZZ9")))," dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,20);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlnumshares_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlnumshares_Enabled,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)16,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 21,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',16)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactinvitationsent_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV18groupContacts.Item(AV69GXV2)).gxTpr_Contactinvitationsent, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV18groupContacts.Item(AV69GXV2)).gxTpr_Contactinvitationsent, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onblur(this,21);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactinvitationsent_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlcontactinvitationsent_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)16,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 22,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',16)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactinvitacionaccepted_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV18groupContacts.Item(AV69GXV2)).gxTpr_Contactinvitacionaccepted, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV18groupContacts.Item(AV69GXV2)).gxTpr_Contactinvitacionaccepted, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onblur(this,22);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactinvitacionaccepted_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlcontactinvitacionaccepted_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)16,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         send_integrity_lvl_hashes1L2( ) ;
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
         edtavCtlminimumshares_Internalname = sPrefix+"CTLMINIMUMSHARES";
         edtavRestorestatus_Internalname = sPrefix+"vRESTORESTATUS";
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
         edtavRestorestatus_Enabled = 1;
         edtavRestorestatus_Visible = 1;
         edtavCtlminimumshares_Jsonclick = "";
         edtavCtlminimumshares_Enabled = 0;
         edtavCtlcontactinvitacionaccepted_Enabled = -1;
         edtavCtlcontactinvitationsent_Enabled = -1;
         edtavCtlnumshares_Enabled = -1;
         edtavCtlcontactusername_Enabled = -1;
         edtavCtlcontactprivatename_Enabled = -1;
         edtavCtlcontactid1_Enabled = -1;
         edtavCtlminimumshares_Enabled = -1;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"GRIDCONTACTS_nEOF","type":"int"},{"av":"AV18groupContacts","fld":"vGROUPCONTACTS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"nRC_GXsfl_16","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":16,"type":"int"},{"av":"AV66view","fld":"vVIEW","type":""},{"av":"sPrefix","type":"char"},{"av":"GXV1","fld":"CTLMINIMUMSHARES","pic":"ZZZ9","type":"int"}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV66view","fld":"vVIEW","type":""},{"av":"AV18groupContacts","fld":"vGROUPCONTACTS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":16,"type":"int"},{"av":"AV60restoreStatus","fld":"vRESTORESTATUS","type":"char"},{"av":"edtavRestorestatus_Visible","ctrl":"vRESTORESTATUS","prop":"Visible"},{"ctrl":"RESTOREWALLET","prop":"Visible"},{"ctrl":"CREATERECOVEREDWALLET","prop":"Visible"}]}""");
         setEventMetadata("GRIDCONTACTS.LOAD","""{"handler":"E161L2","iparms":[{"av":"AV66view","fld":"vVIEW","type":""}]""");
         setEventMetadata("GRIDCONTACTS.LOAD",""","oparms":[{"ctrl":"CTLCONTACTPRIVATENAME","prop":"Visible"},{"ctrl":"CTLCONTACTUSERNAME","prop":"Visible"}]}""");
         setEventMetadata("'CLOSE'","""{"handler":"E111L2","iparms":[]}""");
         setEventMetadata("'RESTORE WALLET'","""{"handler":"E121L2","iparms":[{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"GRIDCONTACTS_nEOF","type":"int"},{"av":"AV18groupContacts","fld":"vGROUPCONTACTS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"nRC_GXsfl_16","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":16,"type":"int"},{"av":"AV66view","fld":"vVIEW","type":""},{"av":"GXV1","fld":"CTLMINIMUMSHARES","pic":"ZZZ9","type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'RESTORE WALLET'",""","oparms":[{"av":"AV66view","fld":"vVIEW","type":""},{"av":"AV18groupContacts","fld":"vGROUPCONTACTS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":16,"type":"int"},{"av":"AV60restoreStatus","fld":"vRESTORESTATUS","type":"char"},{"av":"edtavRestorestatus_Visible","ctrl":"vRESTORESTATUS","prop":"Visible"},{"ctrl":"RESTOREWALLET","prop":"Visible"},{"ctrl":"CREATERECOVEREDWALLET","prop":"Visible"}]}""");
         setEventMetadata("'CREATE RECOVERED WALLET'","""{"handler":"E131L2","iparms":[{"av":"AV66view","fld":"vVIEW","type":""}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED","""{"handler":"E141L2","iparms":[{"av":"AV34PopupName","fld":"vPOPUPNAME","type":"char"}]}""");
         setEventMetadata("VALIDV_GXV3","""{"handler":"Validv_Gxv3","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gxv8","iparms":[]}""");
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
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         AV66view = new GeneXus.Programs.wallet.registered.SdtWalletBackupView(context);
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         forbiddenHiddens = new GXProperties();
         AV18groupContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem>( context, "WalletBackupView.ContactItem", "distributedcryptography");
         AV34PopupName = "";
         GX_FocusControl = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         AV60restoreStatus = "";
         GridcontactsContainer = new GXWebGrid( context);
         sStyleString = "";
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
         AV64groupView = new GeneXus.Programs.wallet.registered.SdtGroupListItem(context);
         GXt_SdtGroupListItem1 = new GeneXus.Programs.wallet.registered.SdtGroupListItem(context);
         AV10error = "";
         AV19groupContact = new GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem(context);
         GridcontactsRow = new GXWebRow();
         AV6websession = context.GetSession();
         AV67warning = "";
         GXt_char2 = "";
         AV24expectedPopupName = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGridcontacts_Linesclass = "";
         ROClassString = "";
         GridcontactsColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtlminimumshares_Enabled = 0;
         edtavRestorestatus_Enabled = 0;
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
      private short AV54strFound ;
      private short nGXWrapped ;
      private short subGridcontacts_Backstyle ;
      private short subGridcontacts_Titlebackstyle ;
      private short subGridcontacts_Allowselection ;
      private short subGridcontacts_Allowhovering ;
      private short subGridcontacts_Allowcollapsing ;
      private short subGridcontacts_Collapsed ;
      private int nRC_GXsfl_16 ;
      private int nGXsfl_16_idx=1 ;
      private int edtavCtlminimumshares_Enabled ;
      private int edtavRestorestatus_Enabled ;
      private int edtavCtlcontactid1_Enabled ;
      private int edtavCtlcontactprivatename_Enabled ;
      private int edtavCtlcontactusername_Enabled ;
      private int edtavCtlnumshares_Enabled ;
      private int edtavCtlcontactinvitationsent_Enabled ;
      private int edtavCtlcontactinvitacionaccepted_Enabled ;
      private int edtavRestorestatus_Visible ;
      private int AV69GXV2 ;
      private int bttRestorewallet_Visible ;
      private int bttCreaterecoveredwallet_Visible ;
      private int subGridcontacts_Islastpage ;
      private int nGXsfl_16_fel_idx=1 ;
      private int AV76GXV9 ;
      private int edtavCtlcontactprivatename_Visible ;
      private int edtavCtlcontactusername_Visible ;
      private int nGXsfl_16_bak_idx=1 ;
      private int idxLst ;
      private int subGridcontacts_Backcolor ;
      private int subGridcontacts_Allbackcolor ;
      private int subGridcontacts_Titlebackcolor ;
      private int subGridcontacts_Selectedindex ;
      private int subGridcontacts_Selectioncolor ;
      private int subGridcontacts_Hoveringcolor ;
      private long GRIDCONTACTS_nCurrentRecord ;
      private long GRIDCONTACTS_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_16_idx="0001" ;
      private string edtavCtlminimumshares_Internalname ;
      private string edtavRestorestatus_Internalname ;
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
      private string AV34PopupName ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string TempTags ;
      private string edtavCtlminimumshares_Jsonclick ;
      private string ClassString ;
      private string StyleString ;
      private string AV60restoreStatus ;
      private string sStyleString ;
      private string subGridcontacts_Internalname ;
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
      private string AV10error ;
      private string AV67warning ;
      private string GXt_char2 ;
      private string AV24expectedPopupName ;
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
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool gx_refresh_fired ;
      private bool returnInSub ;
      private bool gx_BV16 ;
      private bool AV65restored ;
      private bool AV53walletRestored ;
      private GXProperties forbiddenHiddens ;
      private GXWebGrid GridcontactsContainer ;
      private GXWebRow GridcontactsRow ;
      private GXWebColumn GridcontactsColumn ;
      private GXWebForm Form ;
      private IGxSession AV6websession ;
      private IGxDataStore dsDefault ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView AV66view ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> AV18groupContacts ;
      private GeneXus.Programs.wallet.registered.SdtGroupListItem AV64groupView ;
      private GeneXus.Programs.wallet.registered.SdtGroupListItem GXt_SdtGroupListItem1 ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem AV19groupContact ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
