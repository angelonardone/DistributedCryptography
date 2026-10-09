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
   public class walletbackuprestoresignatures : GXWebComponent
   {
      public walletbackuprestoresignatures( )
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

      public walletbackuprestoresignatures( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( Guid aP0_groupId )
      {
         this.AV17groupId = aP0_groupId;
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
               gxfirstwebparm = GetFirstPar( "groupId");
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
                  AV17groupId = StringUtil.StrToGuid( GetPar( "groupId"));
                  AssignAttri(sPrefix, false, "AV17groupId", AV17groupId.ToString());
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix,(Guid)AV17groupId});
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
                  gxfirstwebparm = GetFirstPar( "groupId");
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
               {
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  gxfirstwebparm = GetFirstPar( "groupId");
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridrestoresignatures") == 0 )
               {
                  gxnrGridrestoresignatures_newrow_invoke( ) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridrestoresignatures") == 0 )
               {
                  gxgrGridrestoresignatures_refresh_invoke( ) ;
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

      protected void gxnrGridrestoresignatures_newrow_invoke( )
      {
         nRC_GXsfl_11 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_11"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_11_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_11_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_11_idx = GetPar( "sGXsfl_11_idx");
         sPrefix = GetPar( "sPrefix");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridrestoresignatures_newrow( ) ;
         /* End function gxnrGridrestoresignatures_newrow_invoke */
      }

      protected void gxgrGridrestoresignatures_refresh_invoke( )
      {
         subGridrestoresignatures_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subGridrestoresignatures_Rows"), "."), 18, MidpointRounding.ToEven));
         AV17groupId = StringUtil.StrToGuid( GetPar( "groupId"));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV22restoreSignatures);
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridrestoresignatures_refresh( subGridrestoresignatures_Rows, AV17groupId, AV22restoreSignatures, sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridrestoresignatures_refresh_invoke */
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
            PA3E2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavRestorestatus_Enabled = 0;
               AssignProp(sPrefix, false, edtavRestorestatus_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavRestorestatus_Enabled), 5, 0), true);
               edtavCtlcontactprivatename_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcontactprivatename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcontactprivatename_Enabled), 5, 0), !bGXsfl_11_Refreshing);
               edtavCtlnumshares_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlnumshares_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlnumshares_Enabled), 5, 0), !bGXsfl_11_Refreshing);
               edtavSignedat_Enabled = 0;
               AssignProp(sPrefix, false, edtavSignedat_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSignedat_Enabled), 5, 0), !bGXsfl_11_Refreshing);
               edtavCtlcontactusername_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcontactusername_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcontactusername_Enabled), 5, 0), !bGXsfl_11_Refreshing);
               WS3E2( ) ;
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
            context.SendWebValue( "Wallet Backup - who signed the restore and when (owner can STOP it)") ;
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
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.walletbackuprestoresignatures", new object[] {UrlEncode(AV17groupId.ToString())}, new string[] {"groupId"}) +"\">") ;
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
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Restoresignatures", AV22restoreSignatures);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Restoresignatures", AV22restoreSignatures);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_11", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_11), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV17groupId", wcpOAV17groupId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"vGROUPID", AV17groupId.ToString());
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vRESTORESIGNATURES", AV22restoreSignatures);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vRESTORESIGNATURES", AV22restoreSignatures);
         }
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vUSERRESPONSE", AV28UserResponse);
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vCONFIRMSTOP", AV7confirmStop);
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDRESTORESIGNATURES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDRESTORESIGNATURES_nFirstRecordOnPage), 15, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDRESTORESIGNATURES_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDRESTORESIGNATURES_nEOF), 1, 0, ".", "")));
      }

      protected void RenderHtmlCloseForm3E2( )
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
         return "Wallet.registered.WalletBackupRestoreSignatures" ;
      }

      public override string GetPgmdesc( )
      {
         return "Wallet Backup - who signed the restore and when (owner can STOP it)" ;
      }

      protected void WB3E0( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.registered.walletbackuprestoresignatures");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavRestorestatus_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavRestorestatus_Internalname, "Restore status", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'" + sPrefix + "',false,'" + sGXsfl_11_idx + "',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavRestorestatus_Internalname, StringUtil.RTrim( AV23restoreStatus), "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,8);\"", 0, 1, edtavRestorestatus_Enabled, 0, 80, "chr", 4, "row", 0, StyleString, ClassString, "", "", "250", -1, 0, "", "", -1, true, "", "'"+sPrefix+"'"+",false,"+"'"+""+"'", 0, "", "HLP_Wallet/registered/WalletBackupRestoreSignatures.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridrestoresignaturesContainer.SetWrapped(nGXWrapped);
            StartGridControl11( ) ;
         }
         if ( wbEnd == 11 )
         {
            wbEnd = 0;
            nRC_GXsfl_11 = (int)(nGXsfl_11_idx-1);
            if ( GridrestoresignaturesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               GridrestoresignaturesContainer.AddObjectProperty("GRIDRESTORESIGNATURES_nEOF", GRIDRESTORESIGNATURES_nEOF);
               GridrestoresignaturesContainer.AddObjectProperty("GRIDRESTORESIGNATURES_nFirstRecordOnPage", GRIDRESTORESIGNATURES_nFirstRecordOnPage);
               AV30GXV1 = nGXsfl_11_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+sPrefix+"GridrestoresignaturesContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Gridrestoresignatures", GridrestoresignaturesContainer, subGridrestoresignatures_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridrestoresignaturesContainerData", GridrestoresignaturesContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridrestoresignaturesContainerData"+"V", GridrestoresignaturesContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridrestoresignaturesContainerData"+"V"+"\" value='"+GridrestoresignaturesContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 18,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttStoprestore_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(11), 2, 0)+","+"null"+");", "Stop Restore", bttStoprestore_Jsonclick, 7, "Stop Restore", "", StyleString, ClassString, bttStoprestore_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+"e113e1_client"+"'", TempTags, "", 2, "HLP_Wallet/registered/WalletBackupRestoreSignatures.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 21,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttAllowrestore_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(11), 2, 0)+","+"null"+");", "Allow Restore", bttAllowrestore_Jsonclick, 7, "Allow Restore", "", StyleString, ClassString, bttAllowrestore_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+"e123e1_client"+"'", TempTags, "", 2, "HLP_Wallet/registered/WalletBackupRestoreSignatures.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 11 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridrestoresignaturesContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  GridrestoresignaturesContainer.AddObjectProperty("GRIDRESTORESIGNATURES_nEOF", GRIDRESTORESIGNATURES_nEOF);
                  GridrestoresignaturesContainer.AddObjectProperty("GRIDRESTORESIGNATURES_nFirstRecordOnPage", GRIDRESTORESIGNATURES_nFirstRecordOnPage);
                  AV30GXV1 = nGXsfl_11_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+sPrefix+"GridrestoresignaturesContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Gridrestoresignatures", GridrestoresignaturesContainer, subGridrestoresignatures_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridrestoresignaturesContainerData", GridrestoresignaturesContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridrestoresignaturesContainerData"+"V", GridrestoresignaturesContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridrestoresignaturesContainerData"+"V"+"\" value='"+GridrestoresignaturesContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START3E2( )
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
            Form.Meta.addItem("description", "Wallet Backup - who signed the restore and when (owner can STOP it)", 0) ;
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
               STRUP3E0( ) ;
            }
         }
      }

      protected void WS3E2( )
      {
         START3E2( ) ;
         EVT3E2( ) ;
      }

      protected void EVT3E2( )
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
                                 STRUP3E0( ) ;
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
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3E0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E133E2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3E0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavRestorestatus_Internalname;
                                    AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDRESTORESIGNATURESPAGING") == 0 )
                           {
                              context.wbHandled = 1;
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3E0( ) ;
                              }
                              sEvt = cgiGet( sPrefix+"GRIDRESTORESIGNATURESPAGING");
                              if ( StringUtil.StrCmp(sEvt, "FIRST") == 0 )
                              {
                                 subgridrestoresignatures_firstpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "PREV") == 0 )
                              {
                                 subgridrestoresignatures_previouspage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "NEXT") == 0 )
                              {
                                 subgridrestoresignatures_nextpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "LAST") == 0 )
                              {
                                 subgridrestoresignatures_lastpage( ) ;
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 26), "GRIDRESTORESIGNATURES.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP3E0( ) ;
                              }
                              nGXsfl_11_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
                              SubsflControlProps_112( ) ;
                              AV30GXV1 = (int)(nGXsfl_11_idx+GRIDRESTORESIGNATURES_nFirstRecordOnPage);
                              if ( ( AV22restoreSignatures.Count >= AV30GXV1 ) && ( AV30GXV1 > 0 ) )
                              {
                                 AV22restoreSignatures.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV22restoreSignatures.Item(AV30GXV1));
                                 AV29signedAt = cgiGet( edtavSignedat_Internalname);
                                 AssignAttri(sPrefix, false, edtavSignedat_Internalname, AV29signedAt);
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
                                          GX_FocusControl = edtavRestorestatus_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Refresh */
                                          E143E2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDRESTORESIGNATURES.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavRestorestatus_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Gridrestoresignatures.Load */
                                          E153E2 ();
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
                                       STRUP3E0( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavRestorestatus_Internalname;
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

      protected void WE3E2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm3E2( ) ;
            }
         }
      }

      protected void PA3E2( )
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
               GX_FocusControl = edtavRestorestatus_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridrestoresignatures_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_112( ) ;
         while ( nGXsfl_11_idx <= nRC_GXsfl_11 )
         {
            sendrow_112( ) ;
            nGXsfl_11_idx = ((subGridrestoresignatures_Islastpage==1)&&(nGXsfl_11_idx+1>subGridrestoresignatures_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_idx+1);
            sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
            SubsflControlProps_112( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridrestoresignaturesContainer)) ;
         /* End function gxnrGridrestoresignatures_newrow */
      }

      protected void gxgrGridrestoresignatures_refresh( int subGridrestoresignatures_Rows ,
                                                        Guid AV17groupId ,
                                                        GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> AV22restoreSignatures ,
                                                        string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDRESTORESIGNATURES_nCurrentRecord = 0;
         RF3E2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGridrestoresignatures_refresh */
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
         RF3E2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavRestorestatus_Enabled = 0;
         AssignProp(sPrefix, false, edtavRestorestatus_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavRestorestatus_Enabled), 5, 0), true);
         edtavCtlcontactprivatename_Enabled = 0;
         edtavCtlnumshares_Enabled = 0;
         edtavSignedat_Enabled = 0;
         edtavCtlcontactusername_Enabled = 0;
      }

      protected void RF3E2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridrestoresignaturesContainer.ClearRows();
         }
         wbStart = 11;
         /* Execute user event: Refresh */
         E143E2 ();
         nGXsfl_11_idx = 1;
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
         bGXsfl_11_Refreshing = true;
         GridrestoresignaturesContainer.AddObjectProperty("GridName", "Gridrestoresignatures");
         GridrestoresignaturesContainer.AddObjectProperty("CmpContext", sPrefix);
         GridrestoresignaturesContainer.AddObjectProperty("InMasterPage", "false");
         GridrestoresignaturesContainer.AddObjectProperty("Class", "Grid");
         GridrestoresignaturesContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridrestoresignaturesContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridrestoresignaturesContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridrestoresignatures_Backcolorstyle), 1, 0, ".", "")));
         GridrestoresignaturesContainer.PageSize = subGridrestoresignatures_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_112( ) ;
            /* Execute user event: Gridrestoresignatures.Load */
            E153E2 ();
            if ( ( subGridrestoresignatures_Islastpage == 0 ) && ( GRIDRESTORESIGNATURES_nCurrentRecord > 0 ) && ( GRIDRESTORESIGNATURES_nGridOutOfScope == 0 ) && ( nGXsfl_11_idx == 1 ) )
            {
               GRIDRESTORESIGNATURES_nCurrentRecord = 0;
               GRIDRESTORESIGNATURES_nGridOutOfScope = 1;
               subgridrestoresignatures_firstpage( ) ;
               /* Execute user event: Gridrestoresignatures.Load */
               E153E2 ();
            }
            wbEnd = 11;
            WB3E0( ) ;
         }
         bGXsfl_11_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes3E2( )
      {
      }

      protected int subGridrestoresignatures_fnc_Pagecount( )
      {
         GRIDRESTORESIGNATURES_nRecordCount = subGridrestoresignatures_fnc_Recordcount( );
         if ( ((long)((GRIDRESTORESIGNATURES_nRecordCount) % (subGridrestoresignatures_fnc_Recordsperpage( )))) == 0 )
         {
            return (int)(NumberUtil.Int( (long)(Math.Round(GRIDRESTORESIGNATURES_nRecordCount/ (decimal)(subGridrestoresignatures_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))) ;
         }
         return (int)(NumberUtil.Int( (long)(Math.Round(GRIDRESTORESIGNATURES_nRecordCount/ (decimal)(subGridrestoresignatures_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected int subGridrestoresignatures_fnc_Recordcount( )
      {
         return AV22restoreSignatures.Count ;
      }

      protected int subGridrestoresignatures_fnc_Recordsperpage( )
      {
         return (int)(20*1) ;
      }

      protected int subGridrestoresignatures_fnc_Currentpage( )
      {
         return (int)(NumberUtil.Int( (long)(Math.Round(GRIDRESTORESIGNATURES_nFirstRecordOnPage/ (decimal)(subGridrestoresignatures_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected short subgridrestoresignatures_firstpage( )
      {
         GRIDRESTORESIGNATURES_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDRESTORESIGNATURES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDRESTORESIGNATURES_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridrestoresignatures_refresh( subGridrestoresignatures_Rows, AV17groupId, AV22restoreSignatures, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgridrestoresignatures_nextpage( )
      {
         GRIDRESTORESIGNATURES_nRecordCount = subGridrestoresignatures_fnc_Recordcount( );
         if ( ( GRIDRESTORESIGNATURES_nRecordCount >= subGridrestoresignatures_fnc_Recordsperpage( ) ) && ( GRIDRESTORESIGNATURES_nEOF == 0 ) )
         {
            GRIDRESTORESIGNATURES_nFirstRecordOnPage = (long)(GRIDRESTORESIGNATURES_nFirstRecordOnPage+subGridrestoresignatures_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDRESTORESIGNATURES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDRESTORESIGNATURES_nFirstRecordOnPage), 15, 0, ".", "")));
         GridrestoresignaturesContainer.AddObjectProperty("GRIDRESTORESIGNATURES_nFirstRecordOnPage", GRIDRESTORESIGNATURES_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrGridrestoresignatures_refresh( subGridrestoresignatures_Rows, AV17groupId, AV22restoreSignatures, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((GRIDRESTORESIGNATURES_nEOF==0) ? 0 : 2)) ;
      }

      protected short subgridrestoresignatures_previouspage( )
      {
         if ( GRIDRESTORESIGNATURES_nFirstRecordOnPage >= subGridrestoresignatures_fnc_Recordsperpage( ) )
         {
            GRIDRESTORESIGNATURES_nFirstRecordOnPage = (long)(GRIDRESTORESIGNATURES_nFirstRecordOnPage-subGridrestoresignatures_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDRESTORESIGNATURES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDRESTORESIGNATURES_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridrestoresignatures_refresh( subGridrestoresignatures_Rows, AV17groupId, AV22restoreSignatures, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgridrestoresignatures_lastpage( )
      {
         GRIDRESTORESIGNATURES_nRecordCount = subGridrestoresignatures_fnc_Recordcount( );
         if ( GRIDRESTORESIGNATURES_nRecordCount > subGridrestoresignatures_fnc_Recordsperpage( ) )
         {
            if ( ((long)((GRIDRESTORESIGNATURES_nRecordCount) % (subGridrestoresignatures_fnc_Recordsperpage( )))) == 0 )
            {
               GRIDRESTORESIGNATURES_nFirstRecordOnPage = (long)(GRIDRESTORESIGNATURES_nRecordCount-subGridrestoresignatures_fnc_Recordsperpage( ));
            }
            else
            {
               GRIDRESTORESIGNATURES_nFirstRecordOnPage = (long)(GRIDRESTORESIGNATURES_nRecordCount-((long)((GRIDRESTORESIGNATURES_nRecordCount) % (subGridrestoresignatures_fnc_Recordsperpage( )))));
            }
         }
         else
         {
            GRIDRESTORESIGNATURES_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDRESTORESIGNATURES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDRESTORESIGNATURES_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridrestoresignatures_refresh( subGridrestoresignatures_Rows, AV17groupId, AV22restoreSignatures, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subgridrestoresignatures_gotopage( int nPageNo )
      {
         if ( nPageNo > 0 )
         {
            GRIDRESTORESIGNATURES_nFirstRecordOnPage = (long)(subGridrestoresignatures_fnc_Recordsperpage( )*(nPageNo-1));
         }
         else
         {
            GRIDRESTORESIGNATURES_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDRESTORESIGNATURES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDRESTORESIGNATURES_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridrestoresignatures_refresh( subGridrestoresignatures_Rows, AV17groupId, AV22restoreSignatures, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         edtavRestorestatus_Enabled = 0;
         AssignProp(sPrefix, false, edtavRestorestatus_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavRestorestatus_Enabled), 5, 0), true);
         edtavCtlcontactprivatename_Enabled = 0;
         edtavCtlnumshares_Enabled = 0;
         edtavSignedat_Enabled = 0;
         edtavCtlcontactusername_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3E0( )
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
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Restoresignatures"), AV22restoreSignatures);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vRESTORESIGNATURES"), AV22restoreSignatures);
            /* Read saved values. */
            nRC_GXsfl_11 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_11"), ".", ","), 18, MidpointRounding.ToEven));
            wcpOAV17groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"wcpOAV17groupId"));
            AV7confirmStop = StringUtil.StrToBool( cgiGet( sPrefix+"vCONFIRMSTOP"));
            GRIDRESTORESIGNATURES_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRIDRESTORESIGNATURES_nFirstRecordOnPage"), ".", ","), 18, MidpointRounding.ToEven));
            GRIDRESTORESIGNATURES_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRIDRESTORESIGNATURES_nEOF"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_11 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_11"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_11_fel_idx = 0;
            while ( nGXsfl_11_fel_idx < nRC_GXsfl_11 )
            {
               nGXsfl_11_fel_idx = ((subGridrestoresignatures_Islastpage==1)&&(nGXsfl_11_fel_idx+1>subGridrestoresignatures_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_fel_idx+1);
               sGXsfl_11_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_112( ) ;
               AV30GXV1 = (int)(nGXsfl_11_fel_idx+GRIDRESTORESIGNATURES_nFirstRecordOnPage);
               if ( ( AV22restoreSignatures.Count >= AV30GXV1 ) && ( AV30GXV1 > 0 ) )
               {
                  AV22restoreSignatures.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV22restoreSignatures.Item(AV30GXV1));
                  AV29signedAt = cgiGet( edtavSignedat_Internalname);
               }
            }
            if ( nGXsfl_11_fel_idx == 0 )
            {
               nGXsfl_11_idx = 1;
               sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
               SubsflControlProps_112( ) ;
            }
            nGXsfl_11_fel_idx = 1;
            /* Read variables values. */
            AV23restoreStatus = cgiGet( edtavRestorestatus_Internalname);
            AssignAttri(sPrefix, false, "AV23restoreStatus", AV23restoreStatus);
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            /* Check if conditions changed and reset current page numbers */
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void E143E2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         GXt_char1 = AV9error;
         new GeneXus.Programs.wallet.registered.getwalletbackupsignatures(context ).execute(  AV17groupId, out  AV19isOwner, out  AV24restoreStopped, out  AV23restoreStatus, out  AV22restoreSignatures, out  GXt_char1) ;
         gx_BV11 = true;
         AssignAttri(sPrefix, false, "AV23restoreStatus", AV23restoreStatus);
         AV9error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
         {
            GX_msglist.addItem(AV9error);
         }
         bttStoprestore_Visible = 0;
         AssignProp(sPrefix, false, bttStoprestore_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttStoprestore_Visible), 5, 0), true);
         bttAllowrestore_Visible = 0;
         AssignProp(sPrefix, false, bttAllowrestore_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttAllowrestore_Visible), 5, 0), true);
         if ( AV19isOwner )
         {
            if ( AV24restoreStopped )
            {
               bttAllowrestore_Visible = 1;
               AssignProp(sPrefix, false, bttAllowrestore_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttAllowrestore_Visible), 5, 0), true);
            }
            else
            {
               bttStoprestore_Visible = 1;
               AssignProp(sPrefix, false, bttStoprestore_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttStoprestore_Visible), 5, 0), true);
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV22restoreSignatures", AV22restoreSignatures);
      }

      private void E153E2( )
      {
         /* Gridrestoresignatures_Load Routine */
         returnInSub = false;
         AV30GXV1 = 1;
         while ( AV30GXV1 <= AV22restoreSignatures.Count )
         {
            AV22restoreSignatures.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV22restoreSignatures.Item(AV30GXV1));
            if ( (DateTime.MinValue==((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)(AV22restoreSignatures.CurrentItem)).gxTpr_Restoresigneddatetime) )
            {
               AV29signedAt = "";
               AssignAttri(sPrefix, false, edtavSignedat_Internalname, AV29signedAt);
            }
            else
            {
               AV29signedAt = context.localUtil.TToC( ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)(AV22restoreSignatures.CurrentItem)).gxTpr_Restoresigneddatetime, 8, 5, 1, 2, "/", ":", " ");
               AssignAttri(sPrefix, false, edtavSignedat_Internalname, AV29signedAt);
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 11;
            }
            if ( ( subGridrestoresignatures_Islastpage == 1 ) || ( 20 == 0 ) || ( ( GRIDRESTORESIGNATURES_nCurrentRecord >= GRIDRESTORESIGNATURES_nFirstRecordOnPage ) && ( GRIDRESTORESIGNATURES_nCurrentRecord < GRIDRESTORESIGNATURES_nFirstRecordOnPage + subGridrestoresignatures_fnc_Recordsperpage( ) ) ) )
            {
               sendrow_112( ) ;
            }
            GRIDRESTORESIGNATURES_nEOF = (short)(((subGridrestoresignatures_Rows==0)||(GRIDRESTORESIGNATURES_nCurrentRecord<GRIDRESTORESIGNATURES_nFirstRecordOnPage+subGridrestoresignatures_fnc_Recordsperpage( )) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, sPrefix+"GRIDRESTORESIGNATURES_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDRESTORESIGNATURES_nEOF), 1, 0, ".", "")));
            GRIDRESTORESIGNATURES_nCurrentRecord = (long)(GRIDRESTORESIGNATURES_nCurrentRecord+1);
            if ( isFullAjaxMode( ) && ! bGXsfl_11_Refreshing )
            {
               DoAjaxLoad(11, GridrestoresignaturesRow);
            }
            AV30GXV1 = (int)(AV30GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E133E2( )
      {
         /* Extensions\Web\Dialog_Onconfirmclosed Routine */
         returnInSub = false;
         if ( AV28UserResponse )
         {
            GXt_char1 = AV9error;
            new GeneXus.Programs.wallet.registered.setwalletbackuprestorestopped(context ).execute(  AV17groupId,  AV7confirmStop, out  GXt_char1) ;
            AV9error = GXt_char1;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV9error)) )
            {
               if ( AV7confirmStop )
               {
                  this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Wallet Backup",(string)"The restore was STOPPED and all members were notified"}, true);
               }
               else
               {
                  this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Wallet Backup",(string)"The restore is allowed again and all members were notified"}, true);
               }
            }
            else
            {
               GX_msglist.addItem(AV9error);
            }
            context.DoAjaxRefreshCmp(sPrefix);
         }
         /*  Sending Event outputs  */
         if ( gx_BV11 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV22restoreSignatures", AV22restoreSignatures);
            nGXsfl_11_bak_idx = nGXsfl_11_idx;
            gxgrGridrestoresignatures_refresh( subGridrestoresignatures_Rows, AV17groupId, AV22restoreSignatures, sPrefix) ;
            nGXsfl_11_idx = nGXsfl_11_bak_idx;
            sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
            SubsflControlProps_112( ) ;
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV17groupId = (Guid)getParm(obj,0);
         AssignAttri(sPrefix, false, "AV17groupId", AV17groupId.ToString());
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
         PA3E2( ) ;
         WS3E2( ) ;
         WE3E2( ) ;
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
         sCtrlAV17groupId = (string)((string)getParm(obj,0));
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA3E2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\registered\\walletbackuprestoresignatures", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA3E2( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
            AV17groupId = (Guid)getParm(obj,2);
            AssignAttri(sPrefix, false, "AV17groupId", AV17groupId.ToString());
         }
         wcpOAV17groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"wcpOAV17groupId"));
         if ( ! GetJustCreated( ) && ( ( AV17groupId != wcpOAV17groupId ) ) )
         {
            setjustcreated();
         }
         wcpOAV17groupId = AV17groupId;
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
         sCtrlAV17groupId = cgiGet( sPrefix+"AV17groupId_CTRL");
         if ( StringUtil.Len( sCtrlAV17groupId) > 0 )
         {
            AV17groupId = StringUtil.StrToGuid( cgiGet( sCtrlAV17groupId));
            AssignAttri(sPrefix, false, "AV17groupId", AV17groupId.ToString());
         }
         else
         {
            AV17groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"AV17groupId_PARM"));
         }
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
         PA3E2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS3E2( ) ;
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
         WS3E2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"AV17groupId_PARM", AV17groupId.ToString());
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV17groupId)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV17groupId_CTRL", StringUtil.RTrim( sCtrlAV17groupId));
         }
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
         WE3E2( ) ;
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
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202610714151597", true, true, false);
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
         context.AddJavascriptSource("wallet/registered/walletbackuprestoresignatures.js", "?202610714151597", false, true, false);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true, false);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_112( )
      {
         edtavCtlcontactprivatename_Internalname = sPrefix+"CTLCONTACTPRIVATENAME_"+sGXsfl_11_idx;
         edtavCtlnumshares_Internalname = sPrefix+"CTLNUMSHARES_"+sGXsfl_11_idx;
         edtavSignedat_Internalname = sPrefix+"vSIGNEDAT_"+sGXsfl_11_idx;
         edtavCtlcontactusername_Internalname = sPrefix+"CTLCONTACTUSERNAME_"+sGXsfl_11_idx;
      }

      protected void SubsflControlProps_fel_112( )
      {
         edtavCtlcontactprivatename_Internalname = sPrefix+"CTLCONTACTPRIVATENAME_"+sGXsfl_11_fel_idx;
         edtavCtlnumshares_Internalname = sPrefix+"CTLNUMSHARES_"+sGXsfl_11_fel_idx;
         edtavSignedat_Internalname = sPrefix+"vSIGNEDAT_"+sGXsfl_11_fel_idx;
         edtavCtlcontactusername_Internalname = sPrefix+"CTLCONTACTUSERNAME_"+sGXsfl_11_fel_idx;
      }

      protected void sendrow_112( )
      {
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
         WB3E0( ) ;
         if ( ( 20 * 1 == 0 ) || ( nGXsfl_11_idx <= subGridrestoresignatures_fnc_Recordsperpage( ) * 1 ) )
         {
            GridrestoresignaturesRow = GXWebRow.GetNew(context,GridrestoresignaturesContainer);
            if ( subGridrestoresignatures_Backcolorstyle == 0 )
            {
               /* None style subfile background logic. */
               subGridrestoresignatures_Backstyle = 0;
               if ( StringUtil.StrCmp(subGridrestoresignatures_Class, "") != 0 )
               {
                  subGridrestoresignatures_Linesclass = subGridrestoresignatures_Class+"Odd";
               }
            }
            else if ( subGridrestoresignatures_Backcolorstyle == 1 )
            {
               /* Uniform style subfile background logic. */
               subGridrestoresignatures_Backstyle = 0;
               subGridrestoresignatures_Backcolor = subGridrestoresignatures_Allbackcolor;
               if ( StringUtil.StrCmp(subGridrestoresignatures_Class, "") != 0 )
               {
                  subGridrestoresignatures_Linesclass = subGridrestoresignatures_Class+"Uniform";
               }
            }
            else if ( subGridrestoresignatures_Backcolorstyle == 2 )
            {
               /* Header style subfile background logic. */
               subGridrestoresignatures_Backstyle = 1;
               if ( StringUtil.StrCmp(subGridrestoresignatures_Class, "") != 0 )
               {
                  subGridrestoresignatures_Linesclass = subGridrestoresignatures_Class+"Odd";
               }
               subGridrestoresignatures_Backcolor = (int)(0x0);
            }
            else if ( subGridrestoresignatures_Backcolorstyle == 3 )
            {
               /* Report style subfile background logic. */
               subGridrestoresignatures_Backstyle = 1;
               if ( ((int)((nGXsfl_11_idx) % (2))) == 0 )
               {
                  subGridrestoresignatures_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subGridrestoresignatures_Class, "") != 0 )
                  {
                     subGridrestoresignatures_Linesclass = subGridrestoresignatures_Class+"Even";
                  }
               }
               else
               {
                  subGridrestoresignatures_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subGridrestoresignatures_Class, "") != 0 )
                  {
                     subGridrestoresignatures_Linesclass = subGridrestoresignatures_Class+"Odd";
                  }
               }
            }
            if ( GridrestoresignaturesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<tr ") ;
               context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
               context.WriteHtmlText( " gxrow=\""+sGXsfl_11_idx+"\">") ;
            }
            /* Subfile cell */
            if ( GridrestoresignaturesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 12,'" + sPrefix + "',false,'" + sGXsfl_11_idx + "',11)\"";
            ROClassString = "Attribute";
            GridrestoresignaturesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactprivatename_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV22restoreSignatures.Item(AV30GXV1)).gxTpr_Contactprivatename),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,12);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactprivatename_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlcontactprivatename_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridrestoresignaturesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'" + sPrefix + "',false,'" + sGXsfl_11_idx + "',11)\"";
            ROClassString = "Attribute";
            GridrestoresignaturesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlnumshares_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV22restoreSignatures.Item(AV30GXV1)).gxTpr_Numshares), 4, 0, ".", "")),StringUtil.LTrim( ((edtavCtlnumshares_Enabled!=0) ? context.localUtil.Format( (decimal)(((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV22restoreSignatures.Item(AV30GXV1)).gxTpr_Numshares), "ZZZ9") : context.localUtil.Format( (decimal)(((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV22restoreSignatures.Item(AV30GXV1)).gxTpr_Numshares), "ZZZ9")))," dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,13);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlnumshares_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlnumshares_Enabled,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridrestoresignaturesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 14,'" + sPrefix + "',false,'" + sGXsfl_11_idx + "',11)\"";
            ROClassString = "Attribute";
            GridrestoresignaturesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavSignedat_Internalname,StringUtil.RTrim( AV29signedAt),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,14);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavSignedat_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavSignedat_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)30,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridrestoresignaturesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 15,'" + sPrefix + "',false,'" + sGXsfl_11_idx + "',11)\"";
            ROClassString = "Attribute";
            GridrestoresignaturesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactusername_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV22restoreSignatures.Item(AV30GXV1)).gxTpr_Contactusername),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,15);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactusername_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlcontactusername_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            send_integrity_lvl_hashes3E2( ) ;
            GridrestoresignaturesContainer.AddRow(GridrestoresignaturesRow);
            nGXsfl_11_idx = ((subGridrestoresignatures_Islastpage==1)&&(nGXsfl_11_idx+1>subGridrestoresignatures_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_idx+1);
            sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
            SubsflControlProps_112( ) ;
         }
         /* End function sendrow_112 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl11( )
      {
         if ( GridrestoresignaturesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"GridrestoresignaturesContainer"+"DivS\" data-gxgridid=\"11\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGridrestoresignatures_Internalname, subGridrestoresignatures_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGridrestoresignatures_Backcolorstyle == 0 )
            {
               subGridrestoresignatures_Titlebackstyle = 0;
               if ( StringUtil.Len( subGridrestoresignatures_Class) > 0 )
               {
                  subGridrestoresignatures_Linesclass = subGridrestoresignatures_Class+"Title";
               }
            }
            else
            {
               subGridrestoresignatures_Titlebackstyle = 1;
               if ( subGridrestoresignatures_Backcolorstyle == 1 )
               {
                  subGridrestoresignatures_Titlebackcolor = subGridrestoresignatures_Allbackcolor;
                  if ( StringUtil.Len( subGridrestoresignatures_Class) > 0 )
                  {
                     subGridrestoresignatures_Linesclass = subGridrestoresignatures_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGridrestoresignatures_Class) > 0 )
                  {
                     subGridrestoresignatures_Linesclass = subGridrestoresignatures_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Member") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Shares") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Signed Date Time") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "User") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridrestoresignaturesContainer.AddObjectProperty("GridName", "Gridrestoresignatures");
         }
         else
         {
            GridrestoresignaturesContainer.AddObjectProperty("GridName", "Gridrestoresignatures");
            GridrestoresignaturesContainer.AddObjectProperty("Header", subGridrestoresignatures_Header);
            GridrestoresignaturesContainer.AddObjectProperty("Class", "Grid");
            GridrestoresignaturesContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridrestoresignaturesContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridrestoresignaturesContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridrestoresignatures_Backcolorstyle), 1, 0, ".", "")));
            GridrestoresignaturesContainer.AddObjectProperty("CmpContext", sPrefix);
            GridrestoresignaturesContainer.AddObjectProperty("InMasterPage", "false");
            GridrestoresignaturesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridrestoresignaturesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlcontactprivatename_Enabled), 5, 0, ".", "")));
            GridrestoresignaturesContainer.AddColumnProperties(GridrestoresignaturesColumn);
            GridrestoresignaturesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridrestoresignaturesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlnumshares_Enabled), 5, 0, ".", "")));
            GridrestoresignaturesContainer.AddColumnProperties(GridrestoresignaturesColumn);
            GridrestoresignaturesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridrestoresignaturesColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV29signedAt)));
            GridrestoresignaturesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavSignedat_Enabled), 5, 0, ".", "")));
            GridrestoresignaturesContainer.AddColumnProperties(GridrestoresignaturesColumn);
            GridrestoresignaturesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridrestoresignaturesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlcontactusername_Enabled), 5, 0, ".", "")));
            GridrestoresignaturesContainer.AddColumnProperties(GridrestoresignaturesColumn);
            GridrestoresignaturesContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridrestoresignatures_Selectedindex), 4, 0, ".", "")));
            GridrestoresignaturesContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridrestoresignatures_Allowselection), 1, 0, ".", "")));
            GridrestoresignaturesContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridrestoresignatures_Selectioncolor), 9, 0, ".", "")));
            GridrestoresignaturesContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridrestoresignatures_Allowhovering), 1, 0, ".", "")));
            GridrestoresignaturesContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridrestoresignatures_Hoveringcolor), 9, 0, ".", "")));
            GridrestoresignaturesContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridrestoresignatures_Allowcollapsing), 1, 0, ".", "")));
            GridrestoresignaturesContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridrestoresignatures_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         edtavRestorestatus_Internalname = sPrefix+"vRESTORESTATUS";
         edtavCtlcontactprivatename_Internalname = sPrefix+"CTLCONTACTPRIVATENAME";
         edtavCtlnumshares_Internalname = sPrefix+"CTLNUMSHARES";
         edtavSignedat_Internalname = sPrefix+"vSIGNEDAT";
         edtavCtlcontactusername_Internalname = sPrefix+"CTLCONTACTUSERNAME";
         bttStoprestore_Internalname = sPrefix+"STOPRESTORE";
         bttAllowrestore_Internalname = sPrefix+"ALLOWRESTORE";
         divMaintable_Internalname = sPrefix+"MAINTABLE";
         Form.Internalname = sPrefix+"FORM";
         subGridrestoresignatures_Internalname = sPrefix+"GRIDRESTORESIGNATURES";
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
         subGridrestoresignatures_Allowcollapsing = 0;
         subGridrestoresignatures_Allowselection = 0;
         subGridrestoresignatures_Header = "";
         edtavCtlcontactusername_Jsonclick = "";
         edtavCtlcontactusername_Enabled = 0;
         edtavSignedat_Jsonclick = "";
         edtavSignedat_Enabled = 0;
         edtavCtlnumshares_Jsonclick = "";
         edtavCtlnumshares_Enabled = 0;
         edtavCtlcontactprivatename_Jsonclick = "";
         edtavCtlcontactprivatename_Enabled = 0;
         subGridrestoresignatures_Class = "Grid";
         subGridrestoresignatures_Backcolorstyle = 0;
         bttAllowrestore_Visible = 1;
         bttStoprestore_Visible = 1;
         edtavRestorestatus_Enabled = 1;
         edtavCtlcontactusername_Enabled = -1;
         edtavCtlnumshares_Enabled = -1;
         edtavCtlcontactprivatename_Enabled = -1;
         subGridrestoresignatures_Rows = 20;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDRESTORESIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"GRIDRESTORESIGNATURES_nEOF","type":"int"},{"av":"subGridrestoresignatures_Rows","ctrl":"GRIDRESTORESIGNATURES","prop":"Rows"},{"av":"AV22restoreSignatures","fld":"vRESTORESIGNATURES","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"nRC_GXsfl_11","ctrl":"GRIDRESTORESIGNATURES","prop":"GridRC","grid":11,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV17groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV22restoreSignatures","fld":"vRESTORESIGNATURES","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDRESTORESIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDRESTORESIGNATURES","prop":"GridRC","grid":11,"type":"int"},{"av":"AV23restoreStatus","fld":"vRESTORESTATUS","type":"char"},{"ctrl":"STOPRESTORE","prop":"Visible"},{"ctrl":"ALLOWRESTORE","prop":"Visible"}]}""");
         setEventMetadata("GRIDRESTORESIGNATURES.LOAD","""{"handler":"E153E2","iparms":[{"av":"AV22restoreSignatures","fld":"vRESTORESIGNATURES","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDRESTORESIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDRESTORESIGNATURES","prop":"GridRC","grid":11,"type":"int"}]""");
         setEventMetadata("GRIDRESTORESIGNATURES.LOAD",""","oparms":[{"av":"AV29signedAt","fld":"vSIGNEDAT","type":"char"}]}""");
         setEventMetadata("'STOP RESTORE'","""{"handler":"E113E1","iparms":[]""");
         setEventMetadata("'STOP RESTORE'",""","oparms":[{"av":"AV7confirmStop","fld":"vCONFIRMSTOP","type":"boolean"}]}""");
         setEventMetadata("'ALLOW RESTORE'","""{"handler":"E123E1","iparms":[]""");
         setEventMetadata("'ALLOW RESTORE'",""","oparms":[{"av":"AV7confirmStop","fld":"vCONFIRMSTOP","type":"boolean"}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED","""{"handler":"E133E2","iparms":[{"av":"GRIDRESTORESIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"GRIDRESTORESIGNATURES_nEOF","type":"int"},{"av":"subGridrestoresignatures_Rows","ctrl":"GRIDRESTORESIGNATURES","prop":"Rows"},{"av":"AV17groupId","fld":"vGROUPID","type":"guid"},{"av":"AV22restoreSignatures","fld":"vRESTORESIGNATURES","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"nRC_GXsfl_11","ctrl":"GRIDRESTORESIGNATURES","prop":"GridRC","grid":11,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV28UserResponse","fld":"vUSERRESPONSE","type":"boolean"},{"av":"AV7confirmStop","fld":"vCONFIRMSTOP","type":"boolean"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED",""","oparms":[{"av":"AV22restoreSignatures","fld":"vRESTORESIGNATURES","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDRESTORESIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDRESTORESIGNATURES","prop":"GridRC","grid":11,"type":"int"},{"av":"AV23restoreStatus","fld":"vRESTORESTATUS","type":"char"},{"ctrl":"STOPRESTORE","prop":"Visible"},{"ctrl":"ALLOWRESTORE","prop":"Visible"}]}""");
         setEventMetadata("GRIDRESTORESIGNATURES_FIRSTPAGE","""{"handler":"subgridrestoresignatures_firstpage","iparms":[{"av":"GRIDRESTORESIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"GRIDRESTORESIGNATURES_nEOF","type":"int"},{"av":"subGridrestoresignatures_Rows","ctrl":"GRIDRESTORESIGNATURES","prop":"Rows"},{"av":"AV22restoreSignatures","fld":"vRESTORESIGNATURES","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"nRC_GXsfl_11","ctrl":"GRIDRESTORESIGNATURES","prop":"GridRC","grid":11,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV17groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("GRIDRESTORESIGNATURES_FIRSTPAGE",""","oparms":[{"av":"AV22restoreSignatures","fld":"vRESTORESIGNATURES","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDRESTORESIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDRESTORESIGNATURES","prop":"GridRC","grid":11,"type":"int"},{"av":"AV23restoreStatus","fld":"vRESTORESTATUS","type":"char"},{"ctrl":"STOPRESTORE","prop":"Visible"},{"ctrl":"ALLOWRESTORE","prop":"Visible"}]}""");
         setEventMetadata("GRIDRESTORESIGNATURES_PREVPAGE","""{"handler":"subgridrestoresignatures_previouspage","iparms":[{"av":"GRIDRESTORESIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"GRIDRESTORESIGNATURES_nEOF","type":"int"},{"av":"subGridrestoresignatures_Rows","ctrl":"GRIDRESTORESIGNATURES","prop":"Rows"},{"av":"AV22restoreSignatures","fld":"vRESTORESIGNATURES","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"nRC_GXsfl_11","ctrl":"GRIDRESTORESIGNATURES","prop":"GridRC","grid":11,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV17groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("GRIDRESTORESIGNATURES_PREVPAGE",""","oparms":[{"av":"AV22restoreSignatures","fld":"vRESTORESIGNATURES","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDRESTORESIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDRESTORESIGNATURES","prop":"GridRC","grid":11,"type":"int"},{"av":"AV23restoreStatus","fld":"vRESTORESTATUS","type":"char"},{"ctrl":"STOPRESTORE","prop":"Visible"},{"ctrl":"ALLOWRESTORE","prop":"Visible"}]}""");
         setEventMetadata("GRIDRESTORESIGNATURES_NEXTPAGE","""{"handler":"subgridrestoresignatures_nextpage","iparms":[{"av":"GRIDRESTORESIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"GRIDRESTORESIGNATURES_nEOF","type":"int"},{"av":"subGridrestoresignatures_Rows","ctrl":"GRIDRESTORESIGNATURES","prop":"Rows"},{"av":"AV22restoreSignatures","fld":"vRESTORESIGNATURES","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"nRC_GXsfl_11","ctrl":"GRIDRESTORESIGNATURES","prop":"GridRC","grid":11,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV17groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("GRIDRESTORESIGNATURES_NEXTPAGE",""","oparms":[{"av":"AV22restoreSignatures","fld":"vRESTORESIGNATURES","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDRESTORESIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDRESTORESIGNATURES","prop":"GridRC","grid":11,"type":"int"},{"av":"AV23restoreStatus","fld":"vRESTORESTATUS","type":"char"},{"ctrl":"STOPRESTORE","prop":"Visible"},{"ctrl":"ALLOWRESTORE","prop":"Visible"}]}""");
         setEventMetadata("GRIDRESTORESIGNATURES_LASTPAGE","""{"handler":"subgridrestoresignatures_lastpage","iparms":[{"av":"GRIDRESTORESIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"GRIDRESTORESIGNATURES_nEOF","type":"int"},{"av":"subGridrestoresignatures_Rows","ctrl":"GRIDRESTORESIGNATURES","prop":"Rows"},{"av":"AV22restoreSignatures","fld":"vRESTORESIGNATURES","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"nRC_GXsfl_11","ctrl":"GRIDRESTORESIGNATURES","prop":"GridRC","grid":11,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV17groupId","fld":"vGROUPID","type":"guid"}]""");
         setEventMetadata("GRIDRESTORESIGNATURES_LASTPAGE",""","oparms":[{"av":"AV22restoreSignatures","fld":"vRESTORESIGNATURES","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDRESTORESIGNATURES_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDRESTORESIGNATURES","prop":"GridRC","grid":11,"type":"int"},{"av":"AV23restoreStatus","fld":"vRESTORESTATUS","type":"char"},{"ctrl":"STOPRESTORE","prop":"Visible"},{"ctrl":"ALLOWRESTORE","prop":"Visible"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gxv4","iparms":[]}""");
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
         wcpOAV17groupId = Guid.Empty;
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         AV22restoreSignatures = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem>( context, "WalletBackupView.ContactItem", "distributedcryptography");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GX_FocusControl = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         AV23restoreStatus = "";
         GridrestoresignaturesContainer = new GXWebGrid( context);
         sStyleString = "";
         bttStoprestore_Jsonclick = "";
         bttAllowrestore_Jsonclick = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV29signedAt = "";
         AV9error = "";
         GridrestoresignaturesRow = new GXWebRow();
         GXt_char1 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sCtrlAV17groupId = "";
         subGridrestoresignatures_Linesclass = "";
         ROClassString = "";
         GridrestoresignaturesColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavRestorestatus_Enabled = 0;
         edtavCtlcontactprivatename_Enabled = 0;
         edtavCtlnumshares_Enabled = 0;
         edtavSignedat_Enabled = 0;
         edtavCtlcontactusername_Enabled = 0;
      }

      private short GRIDRESTORESIGNATURES_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGridrestoresignatures_Backcolorstyle ;
      private short nGXWrapped ;
      private short subGridrestoresignatures_Backstyle ;
      private short subGridrestoresignatures_Titlebackstyle ;
      private short subGridrestoresignatures_Allowselection ;
      private short subGridrestoresignatures_Allowhovering ;
      private short subGridrestoresignatures_Allowcollapsing ;
      private short subGridrestoresignatures_Collapsed ;
      private int nRC_GXsfl_11 ;
      private int subGridrestoresignatures_Rows ;
      private int nGXsfl_11_idx=1 ;
      private int edtavRestorestatus_Enabled ;
      private int edtavCtlcontactprivatename_Enabled ;
      private int edtavCtlnumshares_Enabled ;
      private int edtavSignedat_Enabled ;
      private int edtavCtlcontactusername_Enabled ;
      private int AV30GXV1 ;
      private int bttStoprestore_Visible ;
      private int bttAllowrestore_Visible ;
      private int subGridrestoresignatures_Islastpage ;
      private int GRIDRESTORESIGNATURES_nGridOutOfScope ;
      private int nGXsfl_11_fel_idx=1 ;
      private int nGXsfl_11_bak_idx=1 ;
      private int idxLst ;
      private int subGridrestoresignatures_Backcolor ;
      private int subGridrestoresignatures_Allbackcolor ;
      private int subGridrestoresignatures_Titlebackcolor ;
      private int subGridrestoresignatures_Selectedindex ;
      private int subGridrestoresignatures_Selectioncolor ;
      private int subGridrestoresignatures_Hoveringcolor ;
      private long GRIDRESTORESIGNATURES_nFirstRecordOnPage ;
      private long GRIDRESTORESIGNATURES_nCurrentRecord ;
      private long GRIDRESTORESIGNATURES_nRecordCount ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_11_idx="0001" ;
      private string edtavRestorestatus_Internalname ;
      private string edtavCtlcontactprivatename_Internalname ;
      private string edtavCtlnumshares_Internalname ;
      private string edtavSignedat_Internalname ;
      private string edtavCtlcontactusername_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string AV23restoreStatus ;
      private string sStyleString ;
      private string subGridrestoresignatures_Internalname ;
      private string bttStoprestore_Internalname ;
      private string bttStoprestore_Jsonclick ;
      private string bttAllowrestore_Internalname ;
      private string bttAllowrestore_Jsonclick ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV29signedAt ;
      private string sGXsfl_11_fel_idx="0001" ;
      private string AV9error ;
      private string GXt_char1 ;
      private string sCtrlAV17groupId ;
      private string subGridrestoresignatures_Class ;
      private string subGridrestoresignatures_Linesclass ;
      private string ROClassString ;
      private string edtavCtlcontactprivatename_Jsonclick ;
      private string edtavCtlnumshares_Jsonclick ;
      private string edtavSignedat_Jsonclick ;
      private string edtavCtlcontactusername_Jsonclick ;
      private string subGridrestoresignatures_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_11_Refreshing=false ;
      private bool AV28UserResponse ;
      private bool AV7confirmStop ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool gx_refresh_fired ;
      private bool returnInSub ;
      private bool AV19isOwner ;
      private bool AV24restoreStopped ;
      private bool gx_BV11 ;
      private Guid AV17groupId ;
      private Guid wcpOAV17groupId ;
      private GXWebGrid GridrestoresignaturesContainer ;
      private GXWebRow GridrestoresignaturesRow ;
      private GXWebColumn GridrestoresignaturesColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> AV22restoreSignatures ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
