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
namespace GeneXus.Programs.wallet {
   public class passwordswc : GXWebComponent
   {
      public passwordswc( )
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

      public passwordswc( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( ref Guid aP0_groupId )
      {
         this.AV31groupId = aP0_groupId;
         ExecuteImpl();
         aP0_groupId=this.AV31groupId;
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
         cmbavTagname = new GXCombobox();
         cmbavUsername = new GXCombobox();
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
                  AV31groupId = StringUtil.StrToGuid( GetPar( "groupId"));
                  AssignAttri(sPrefix, false, "AV31groupId", AV31groupId.ToString());
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix,(Guid)AV31groupId});
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
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Passwordgrid") == 0 )
               {
                  gxnrPasswordgrid_newrow_invoke( ) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Passwordgrid") == 0 )
               {
                  gxgrPasswordgrid_refresh_invoke( ) ;
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

      protected void gxnrPasswordgrid_newrow_invoke( )
      {
         nRC_GXsfl_22 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_22"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_22_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_22_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_22_idx = GetPar( "sGXsfl_22_idx");
         sPrefix = GetPar( "sPrefix");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrPasswordgrid_newrow( ) ;
         /* End function gxnrPasswordgrid_newrow_invoke */
      }

      protected void gxgrPasswordgrid_refresh_invoke( )
      {
         AV31groupId = StringUtil.StrToGuid( GetPar( "groupId"));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV17Password_tags);
         ajax_req_read_hidden_sdt(GetNextPar( ), AV29group_sdt);
         AV11error = GetPar( "error");
         cmbavTagname.FromJSonString( GetNextPar( ));
         AV20tagName = StringUtil.StrToGuid( GetPar( "tagName"));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV18Passwords);
         cmbavUsername.FromJSonString( GetNextPar( ));
         AV40userName = StringUtil.StrToGuid( GetPar( "userName"));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV26showPasswords);
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrPasswordgrid_refresh( AV31groupId, AV17Password_tags, AV29group_sdt, AV11error, AV20tagName, AV18Passwords, AV40userName, AV26showPasswords, sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrPasswordgrid_refresh_invoke */
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
            PA2H2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavCtlpasswordid_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlpasswordid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlpasswordid_Enabled), 5, 0), !bGXsfl_22_Refreshing);
               edtavDescription_Enabled = 0;
               AssignProp(sPrefix, false, edtavDescription_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDescription_Enabled), 5, 0), !bGXsfl_22_Refreshing);
               edtavCtllogin_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtllogin_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtllogin_Enabled), 5, 0), !bGXsfl_22_Refreshing);
               edtavPass_Enabled = 0;
               AssignProp(sPrefix, false, edtavPass_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavPass_Enabled), 5, 0), !bGXsfl_22_Refreshing);
               WS2H2( ) ;
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
            context.SendWebValue( "Passwords WC") ;
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
         context.AddJavascriptSource("UserControls/Wallet.UC_Clipboard_v1Render.js", "", false, true);
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
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.passwordswc", new object[] {UrlEncode(AV31groupId.ToString())}, new string[] {"groupId"}) +"\">") ;
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
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vPASSWORD_TAGS", AV17Password_tags);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vPASSWORD_TAGS", AV17Password_tags);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPASSWORD_TAGS", GetSecureSignedToken( sPrefix, AV17Password_tags, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Showpasswords", AV26showPasswords);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Showpasswords", AV26showPasswords);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_22", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_22), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV31groupId", wcpOAV31groupId.ToString());
         GxWebStd.gx_hidden_field( context, sPrefix+"vGROUPID", AV31groupId.ToString());
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vPASSWORD_TAGS", AV17Password_tags);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vPASSWORD_TAGS", AV17Password_tags);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPASSWORD_TAGS", GetSecureSignedToken( sPrefix, AV17Password_tags, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vGROUP_SDT", AV29group_sdt);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vGROUP_SDT", AV29group_sdt);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vERROR", StringUtil.RTrim( AV11error));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vPASSWORDS", AV18Passwords);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vPASSWORDS", AV18Passwords);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vSHOWPASSWORDS", AV26showPasswords);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vSHOWPASSWORDS", AV26showPasswords);
         }
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vUSERRESPONSE", AV21UserResponse);
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vPASSWORDS_AND_TAGS", AV19Passwords_and_tags);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vPASSWORDS_AND_TAGS", AV19Passwords_and_tags);
         }
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vISNEWPASSWORD", AV44isNewPassword);
      }

      protected void RenderHtmlCloseForm2H2( )
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
            if ( ! ( WebComp_Editpassword == null ) )
            {
               WebComp_Editpassword.componentjscripts();
            }
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
         return "Wallet.PasswordsWC" ;
      }

      public override string GetPgmdesc( )
      {
         return "Passwords WC" ;
      }

      protected void WB2H0( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.passwordswc");
               context.AddJavascriptSource("UserControls/Wallet.UC_Clipboard_v1Render.js", "", false, true);
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
            GxWebStd.gx_div_start( context, divTable1_Internalname, 1, 100, "%", 0, "px", "Table", "start", "top", " "+"data-gx-smarttable"+" ", "grid-template-columns:33fr 33fr 34fr;grid-template-rows:auto;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", " "+"data-gx-smarttable-cell"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", cmbavTagname.Visible, 0, "px", 0, "px", "form-group gx-form-group gx-default-form-group", "start", "top", ""+" data-gx-for=\""+cmbavTagname_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavTagname_Internalname, "Filter by Tag", "gx-form-item AttributeLabel", 1, true, "width: 25%;");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 75, "%", 0, "px", "gx-form-item gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 10,'" + sPrefix + "',false,'" + sGXsfl_22_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavTagname, cmbavTagname_Internalname, AV20tagName.ToString(), 1, cmbavTagname_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "guid", "", cmbavTagname.Visible, cmbavTagname.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,10);\"", "", true, 0, "HLP_Wallet/PasswordsWC.htm");
            cmbavTagname.CurrentValue = AV20tagName.ToString();
            AssignProp(sPrefix, false, cmbavTagname_Internalname, "Values", (string)(cmbavTagname.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", " "+"data-gx-smarttable-cell"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", cmbavUsername.Visible, 0, "px", 0, "px", "form-group gx-form-group gx-default-form-group", "start", "top", ""+" data-gx-for=\""+cmbavUsername_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavUsername_Internalname, "Filter by Contact", "gx-form-item AttributeLabel", 1, true, "width: 30%;");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 70, "%", 0, "px", "gx-form-item gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 14,'" + sPrefix + "',false,'" + sGXsfl_22_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavUsername, cmbavUsername_Internalname, AV40userName.ToString(), 1, cmbavUsername_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "guid", "", cmbavUsername.Visible, cmbavUsername.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,14);\"", "", true, 0, "HLP_Wallet/PasswordsWC.htm");
            cmbavUsername.CurrentValue = AV40userName.ToString();
            AssignProp(sPrefix, false, cmbavUsername_Internalname, "Values", (string)(cmbavUsername.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", " "+"data-gx-smarttable-cell"+" ", "display:flex;justify-content:flex-end;", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 16,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttAddanewpassword_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(22), 2, 0)+","+"null"+");", "Add a new password", bttAddanewpassword_Jsonclick, 5, "Add a new password", "", StyleString, ClassString, bttAddanewpassword_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'ADD A NEW PASSWORD\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/PasswordsWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* User Defined Control */
            ucUc_clipboard_v11.Render(context, "wallet.uc_clipboard_v1", Uc_clipboard_v11_Internalname, sPrefix+"UC_CLIPBOARD_V11Container");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            PasswordgridContainer.SetWrapped(nGXWrapped);
            StartGridControl22( ) ;
         }
         if ( wbEnd == 22 )
         {
            wbEnd = 0;
            nRC_GXsfl_22 = (int)(nGXsfl_22_idx-1);
            if ( PasswordgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV45GXV1 = nGXsfl_22_idx;
               if ( subPasswordgrid_Visible != 0 )
               {
                  sStyleString = "";
               }
               else
               {
                  sStyleString = " style=\"display:none;\"";
               }
               context.WriteHtmlText( "<div id=\""+sPrefix+"PasswordgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Passwordgrid", PasswordgridContainer, subPasswordgrid_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"PasswordgridContainerData", PasswordgridContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"PasswordgridContainerData"+"V", PasswordgridContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"PasswordgridContainerData"+"V"+"\" value='"+PasswordgridContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            if ( ! isFullAjaxMode( ) )
            {
               /* WebComponent */
               GxWebStd.gx_hidden_field( context, sPrefix+"W0037"+"", StringUtil.RTrim( WebComp_Editpassword_Component));
               context.WriteHtmlText( "<div") ;
               GxWebStd.ClassAttribute( context, "gxwebcomponent");
               context.WriteHtmlText( " id=\""+sPrefix+"gxHTMLWrpW0037"+""+"\""+((WebComp_Editpassword_Visible==1) ? "" : " style=\"display:none;\"")) ;
               context.WriteHtmlText( ">") ;
               if ( bGXsfl_22_Refreshing )
               {
                  if ( StringUtil.Len( WebComp_Editpassword_Component) != 0 )
                  {
                     if ( StringUtil.StrCmp(StringUtil.Lower( OldEditpassword), StringUtil.Lower( WebComp_Editpassword_Component)) != 0 )
                     {
                        context.httpAjaxContext.ajax_rspStartCmp(sPrefix+"gxHTMLWrpW0037"+"");
                     }
                     WebComp_Editpassword.componentdraw();
                     if ( StringUtil.StrCmp(StringUtil.Lower( OldEditpassword), StringUtil.Lower( WebComp_Editpassword_Component)) != 0 )
                     {
                        context.httpAjaxContext.ajax_rspEndCmp();
                     }
                  }
               }
               context.WriteHtmlText( "</div>") ;
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 22 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( PasswordgridContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV45GXV1 = nGXsfl_22_idx;
                  if ( subPasswordgrid_Visible != 0 )
                  {
                     sStyleString = "";
                  }
                  else
                  {
                     sStyleString = " style=\"display:none;\"";
                  }
                  context.WriteHtmlText( "<div id=\""+sPrefix+"PasswordgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Passwordgrid", PasswordgridContainer, subPasswordgrid_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"PasswordgridContainerData", PasswordgridContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"PasswordgridContainerData"+"V", PasswordgridContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"PasswordgridContainerData"+"V"+"\" value='"+PasswordgridContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START2H2( )
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
            Form.Meta.addItem("description", "Passwords WC", 0) ;
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
               STRUP2H0( ) ;
            }
         }
      }

      protected void WS2H2( )
      {
         START2H2( ) ;
         EVT2H2( ) ;
      }

      protected void EVT2H2( )
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
                                 STRUP2H0( ) ;
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
                           else if ( StringUtil.StrCmp(sEvt, "'ADD A NEW PASSWORD'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2H0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Add a new password' */
                                    E112H2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2H0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E122H2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GLOBALEVENTS.DONEWITHPASSWORD") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2H0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E132H2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VTAGNAME.CONTROLVALUECHANGED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2H0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E142H2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VUSERNAME.CONTROLVALUECHANGED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2H0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E152H2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2H0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavCtlpasswordid_Internalname;
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
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 17), "'DELETE PASSWORD'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 15), "'EDIT PASSWORD'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 11), "'VIEW NOTE'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 17), "PASSWORDGRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 12), "'COPY LOGIN'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 15), "'COPY PASSWORD'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 20), "'COPY AUTHENTICATOR'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 8), "'WWTAGS'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 12), "'WWCONTACTS'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 12), "'COPY LOGIN'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 15), "'COPY PASSWORD'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 20), "'COPY AUTHENTICATOR'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 11), "'VIEW NOTE'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 15), "'EDIT PASSWORD'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 17), "'DELETE PASSWORD'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 8), "'WWTAGS'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 12), "'WWCONTACTS'") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2H0( ) ;
                              }
                              nGXsfl_22_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_22_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_22_idx), 4, 0), 4, "0");
                              SubsflControlProps_222( ) ;
                              AV45GXV1 = nGXsfl_22_idx;
                              if ( ( AV26showPasswords.Count >= AV45GXV1 ) && ( AV45GXV1 > 0 ) )
                              {
                                 AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV45GXV1));
                                 AV9description = cgiGet( edtavDescription_Internalname);
                                 AssignAttri(sPrefix, false, edtavDescription_Internalname, AV9description);
                                 AV6copyLogin = cgiGet( edtavCopylogin_Internalname);
                                 AssignProp(sPrefix, false, edtavCopylogin_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV6copyLogin)) ? AV61Copylogin_GXI : context.convertURL( context.PathToRelativeUrl( AV6copyLogin))), !bGXsfl_22_Refreshing);
                                 AssignProp(sPrefix, false, edtavCopylogin_Internalname, "SrcSet", context.GetImageSrcSet( AV6copyLogin), true);
                                 AV16pass = cgiGet( edtavPass_Internalname);
                                 AssignAttri(sPrefix, false, edtavPass_Internalname, AV16pass);
                                 AV7copyPassword = cgiGet( edtavCopypassword_Internalname);
                                 AssignProp(sPrefix, false, edtavCopypassword_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV7copyPassword)) ? AV62Copypassword_GXI : context.convertURL( context.PathToRelativeUrl( AV7copyPassword))), !bGXsfl_22_Refreshing);
                                 AssignProp(sPrefix, false, edtavCopypassword_Internalname, "SrcSet", context.GetImageSrcSet( AV7copyPassword), true);
                                 AV5copyAuthenticator = cgiGet( edtavCopyauthenticator_Internalname);
                                 AssignProp(sPrefix, false, edtavCopyauthenticator_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV5copyAuthenticator)) ? AV63Copyauthenticator_GXI : context.convertURL( context.PathToRelativeUrl( AV5copyAuthenticator))), !bGXsfl_22_Refreshing);
                                 AssignProp(sPrefix, false, edtavCopyauthenticator_Internalname, "SrcSet", context.GetImageSrcSet( AV5copyAuthenticator), true);
                                 AV43viewNote = cgiGet( edtavViewnote_Internalname);
                                 AssignProp(sPrefix, false, edtavViewnote_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV43viewNote)) ? AV65Viewnote_GXI : context.convertURL( context.PathToRelativeUrl( AV43viewNote))), !bGXsfl_22_Refreshing);
                                 AssignProp(sPrefix, false, edtavViewnote_Internalname, "SrcSet", context.GetImageSrcSet( AV43viewNote), true);
                                 AV10editImage = cgiGet( edtavEditimage_Internalname);
                                 AssignProp(sPrefix, false, edtavEditimage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV10editImage)) ? AV60Editimage_GXI : context.convertURL( context.PathToRelativeUrl( AV10editImage))), !bGXsfl_22_Refreshing);
                                 AssignProp(sPrefix, false, edtavEditimage_Internalname, "SrcSet", context.GetImageSrcSet( AV10editImage), true);
                                 AV8deleteImage = cgiGet( edtavDeleteimage_Internalname);
                                 AssignProp(sPrefix, false, edtavDeleteimage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV8deleteImage)) ? AV59Deleteimage_GXI : context.convertURL( context.PathToRelativeUrl( AV8deleteImage))), !bGXsfl_22_Refreshing);
                                 AssignProp(sPrefix, false, edtavDeleteimage_Internalname, "SrcSet", context.GetImageSrcSet( AV8deleteImage), true);
                                 AV23wwTags = cgiGet( edtavWwtags_Internalname);
                                 AssignProp(sPrefix, false, edtavWwtags_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV23wwTags)) ? AV64Wwtags_GXI : context.convertURL( context.PathToRelativeUrl( AV23wwTags))), !bGXsfl_22_Refreshing);
                                 AssignProp(sPrefix, false, edtavWwtags_Internalname, "SrcSet", context.GetImageSrcSet( AV23wwTags), true);
                                 AV41wwConctacts = cgiGet( edtavWwconctacts_Internalname);
                                 AssignProp(sPrefix, false, edtavWwconctacts_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV41wwConctacts)) ? AV48Wwconctacts_GXI : context.convertURL( context.PathToRelativeUrl( AV41wwConctacts))), !bGXsfl_22_Refreshing);
                                 AssignProp(sPrefix, false, edtavWwconctacts_Internalname, "SrcSet", context.GetImageSrcSet( AV41wwConctacts), true);
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
                                          GX_FocusControl = edtavCtlpasswordid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Refresh */
                                          E162H2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'DELETE PASSWORD'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlpasswordid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'Delete Password' */
                                          E172H2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'EDIT PASSWORD'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlpasswordid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'Edit Password' */
                                          E182H2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'VIEW NOTE'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlpasswordid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'view Note' */
                                          E192H2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "PASSWORDGRID.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlpasswordid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Passwordgrid.Load */
                                          E202H2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'COPY LOGIN'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlpasswordid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'Copy login' */
                                          E212H2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'COPY PASSWORD'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlpasswordid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'Copy password' */
                                          E222H2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'COPY AUTHENTICATOR'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlpasswordid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'Copy authenticator' */
                                          E232H2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'WWTAGS'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlpasswordid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'wwtags' */
                                          E242H2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'WWCONTACTS'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlpasswordid_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'wwContacts' */
                                          E252H2 ();
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
                                       STRUP2H0( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlpasswordid_Internalname;
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
                     else if ( StringUtil.StrCmp(sEvtType, "W") == 0 )
                     {
                        sEvtType = StringUtil.Left( sEvt, 4);
                        sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-4));
                        nCmpId = (short)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                        if ( nCmpId == 37 )
                        {
                           OldEditpassword = cgiGet( sPrefix+"W0037");
                           if ( ( StringUtil.Len( OldEditpassword) == 0 ) || ( StringUtil.StrCmp(OldEditpassword, WebComp_Editpassword_Component) != 0 ) )
                           {
                              WebComp_Editpassword = getWebComponent(GetType(), "GeneXus.Programs", OldEditpassword, new Object[] {context} );
                              WebComp_Editpassword.ComponentInit();
                              WebComp_Editpassword.Name = "OldEditpassword";
                              WebComp_Editpassword_Component = OldEditpassword;
                           }
                           if ( StringUtil.Len( WebComp_Editpassword_Component) != 0 )
                           {
                              WebComp_Editpassword.componentprocess(sPrefix+"W0037", "", sEvt);
                           }
                           WebComp_Editpassword_Component = OldEditpassword;
                        }
                     }
                     context.wbHandled = 1;
                  }
               }
            }
         }
      }

      protected void WE2H2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm2H2( ) ;
            }
         }
      }

      protected void PA2H2( )
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
               GX_FocusControl = cmbavTagname_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrPasswordgrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_222( ) ;
         while ( nGXsfl_22_idx <= nRC_GXsfl_22 )
         {
            sendrow_222( ) ;
            nGXsfl_22_idx = ((subPasswordgrid_Islastpage==1)&&(nGXsfl_22_idx+1>subPasswordgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_22_idx+1);
            sGXsfl_22_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_22_idx), 4, 0), 4, "0");
            SubsflControlProps_222( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( PasswordgridContainer)) ;
         /* End function gxnrPasswordgrid_newrow */
      }

      protected void gxgrPasswordgrid_refresh( Guid AV31groupId ,
                                               GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> AV17Password_tags ,
                                               GeneXus.Programs.wallet.registered.SdtGroup_SDT AV29group_sdt ,
                                               string AV11error ,
                                               Guid AV20tagName ,
                                               GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> AV18Passwords ,
                                               Guid AV40userName ,
                                               GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> AV26showPasswords ,
                                               string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         PASSWORDGRID_nCurrentRecord = 0;
         RF2H2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrPasswordgrid_refresh */
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
         if ( cmbavTagname.ItemCount > 0 )
         {
            AV20tagName = StringUtil.StrToGuid( cmbavTagname.getValidValue(AV20tagName.ToString()));
            AssignAttri(sPrefix, false, "AV20tagName", AV20tagName.ToString());
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavTagname.CurrentValue = AV20tagName.ToString();
            AssignProp(sPrefix, false, cmbavTagname_Internalname, "Values", cmbavTagname.ToJavascriptSource(), true);
         }
         if ( cmbavUsername.ItemCount > 0 )
         {
            AV40userName = StringUtil.StrToGuid( cmbavUsername.getValidValue(AV40userName.ToString()));
            AssignAttri(sPrefix, false, "AV40userName", AV40userName.ToString());
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavUsername.CurrentValue = AV40userName.ToString();
            AssignProp(sPrefix, false, cmbavUsername_Internalname, "Values", cmbavUsername.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF2H2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtlpasswordid_Enabled = 0;
         edtavDescription_Enabled = 0;
         edtavCtllogin_Enabled = 0;
         edtavPass_Enabled = 0;
      }

      protected void RF2H2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            PasswordgridContainer.ClearRows();
         }
         wbStart = 22;
         /* Execute user event: Refresh */
         E162H2 ();
         nGXsfl_22_idx = 1;
         sGXsfl_22_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_22_idx), 4, 0), 4, "0");
         SubsflControlProps_222( ) ;
         bGXsfl_22_Refreshing = true;
         PasswordgridContainer.AddObjectProperty("GridName", "Passwordgrid");
         PasswordgridContainer.AddObjectProperty("CmpContext", sPrefix);
         PasswordgridContainer.AddObjectProperty("InMasterPage", "false");
         PasswordgridContainer.AddObjectProperty("Class", "Grid");
         PasswordgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         PasswordgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         PasswordgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subPasswordgrid_Backcolorstyle), 1, 0, ".", "")));
         PasswordgridContainer.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(subPasswordgrid_Visible), 5, 0, ".", "")));
         PasswordgridContainer.PageSize = subPasswordgrid_fnc_Recordsperpage( );
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            if ( WebComp_Editpassword_Visible != 0 )
            {
               if ( StringUtil.Len( WebComp_Editpassword_Component) != 0 )
               {
                  WebComp_Editpassword.componentstart();
               }
            }
         }
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_222( ) ;
            /* Execute user event: Passwordgrid.Load */
            E202H2 ();
            wbEnd = 22;
            WB2H0( ) ;
         }
         bGXsfl_22_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes2H2( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vPASSWORD_TAGS", AV17Password_tags);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vPASSWORD_TAGS", AV17Password_tags);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPASSWORD_TAGS", GetSecureSignedToken( sPrefix, AV17Password_tags, context));
      }

      protected int subPasswordgrid_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subPasswordgrid_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subPasswordgrid_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subPasswordgrid_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtlpasswordid_Enabled = 0;
         edtavDescription_Enabled = 0;
         edtavCtllogin_Enabled = 0;
         edtavPass_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP2H0( )
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
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Showpasswords"), AV26showPasswords);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vSHOWPASSWORDS"), AV26showPasswords);
            /* Read saved values. */
            nRC_GXsfl_22 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_22"), ".", ","), 18, MidpointRounding.ToEven));
            wcpOAV31groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"wcpOAV31groupId"));
            nRC_GXsfl_22 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_22"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_22_fel_idx = 0;
            while ( nGXsfl_22_fel_idx < nRC_GXsfl_22 )
            {
               nGXsfl_22_fel_idx = ((subPasswordgrid_Islastpage==1)&&(nGXsfl_22_fel_idx+1>subPasswordgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_22_fel_idx+1);
               sGXsfl_22_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_22_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_222( ) ;
               AV45GXV1 = nGXsfl_22_fel_idx;
               if ( ( AV26showPasswords.Count >= AV45GXV1 ) && ( AV45GXV1 > 0 ) )
               {
                  AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV45GXV1));
                  AV9description = cgiGet( edtavDescription_Internalname);
                  AV6copyLogin = cgiGet( edtavCopylogin_Internalname);
                  AV16pass = cgiGet( edtavPass_Internalname);
                  AV7copyPassword = cgiGet( edtavCopypassword_Internalname);
                  AV5copyAuthenticator = cgiGet( edtavCopyauthenticator_Internalname);
                  AV43viewNote = cgiGet( edtavViewnote_Internalname);
                  AV10editImage = cgiGet( edtavEditimage_Internalname);
                  AV8deleteImage = cgiGet( edtavDeleteimage_Internalname);
                  AV23wwTags = cgiGet( edtavWwtags_Internalname);
                  AV41wwConctacts = cgiGet( edtavWwconctacts_Internalname);
               }
            }
            if ( nGXsfl_22_fel_idx == 0 )
            {
               nGXsfl_22_idx = 1;
               sGXsfl_22_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_22_idx), 4, 0), 4, "0");
               SubsflControlProps_222( ) ;
            }
            nGXsfl_22_fel_idx = 1;
            /* Read variables values. */
            cmbavTagname.Name = cmbavTagname_Internalname;
            cmbavTagname.CurrentValue = cgiGet( cmbavTagname_Internalname);
            AV20tagName = StringUtil.StrToGuid( cgiGet( cmbavTagname_Internalname));
            AssignAttri(sPrefix, false, "AV20tagName", AV20tagName.ToString());
            cmbavUsername.Name = cmbavUsername_Internalname;
            cmbavUsername.CurrentValue = cgiGet( cmbavUsername_Internalname);
            AV40userName = StringUtil.StrToGuid( cgiGet( cmbavUsername_Internalname));
            AssignAttri(sPrefix, false, "AV40userName", AV40userName.ToString());
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void E162H2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         if ( ! (Guid.Empty==AV31groupId) )
         {
            AV29group_sdt.FromJSonString(AV22websession.Get("Group_EDIT"), null);
            if ( AV29group_sdt.gxTpr_Amigroupowner )
            {
               GXt_SdtGroup_SDT1 = AV29group_sdt;
               new GeneXus.Programs.wallet.registered.getlocalgroupbyid(context ).execute(  AV31groupId, out  GXt_SdtGroup_SDT1) ;
               AV29group_sdt = GXt_SdtGroup_SDT1;
               AV22websession.Set("Group_EDIT", AV29group_sdt.ToJSonString(false, true));
               edtavWwconctacts_gximage = "GeneXusUnanimo_dropdown_avatar";
               AssignProp(sPrefix, false, edtavWwconctacts_Internalname, "gximage", edtavWwconctacts_gximage, !bGXsfl_22_Refreshing);
               AV41wwConctacts = context.GetImagePath( "6d12a80e-3119-452a-846e-cd08e2c77195", "", context.GetTheme( ));
               AssignProp(sPrefix, false, edtavWwconctacts_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV41wwConctacts)) ? AV48Wwconctacts_GXI : context.convertURL( context.PathToRelativeUrl( AV41wwConctacts))), !bGXsfl_22_Refreshing);
               AssignProp(sPrefix, false, edtavWwconctacts_Internalname, "SrcSet", context.GetImageSrcSet( AV41wwConctacts), true);
               AV48Wwconctacts_GXI = GXDbFile.PathToUrl( context.GetImagePath( "6d12a80e-3119-452a-846e-cd08e2c77195", "", context.GetTheme( )), context);
               AssignProp(sPrefix, false, edtavWwconctacts_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV41wwConctacts)) ? AV48Wwconctacts_GXI : context.convertURL( context.PathToRelativeUrl( AV41wwConctacts))), !bGXsfl_22_Refreshing);
               AssignProp(sPrefix, false, edtavWwconctacts_Internalname, "SrcSet", context.GetImageSrcSet( AV41wwConctacts), true);
            }
            else
            {
               bttAddanewpassword_Visible = 0;
               AssignProp(sPrefix, false, bttAddanewpassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttAddanewpassword_Visible), 5, 0), true);
               cmbavUsername.Visible = 0;
               AssignProp(sPrefix, false, cmbavUsername_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavUsername.Visible), 5, 0), true);
               edtavWwconctacts_Visible = 0;
               AssignProp(sPrefix, false, edtavWwconctacts_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavWwconctacts_Visible), 5, 0), !bGXsfl_22_Refreshing);
               edtavWwtags_Visible = 0;
               AssignProp(sPrefix, false, edtavWwtags_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavWwtags_Visible), 5, 0), !bGXsfl_22_Refreshing);
               edtavDeleteimage_Visible = 0;
               AssignProp(sPrefix, false, edtavDeleteimage_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavDeleteimage_Visible), 5, 0), !bGXsfl_22_Refreshing);
               edtavEditimage_Visible = 0;
               AssignProp(sPrefix, false, edtavEditimage_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEditimage_Visible), 5, 0), !bGXsfl_22_Refreshing);
            }
         }
         else
         {
            AV22websession.Set("Group_EDIT", "");
            edtavWwconctacts_Visible = 0;
            AssignProp(sPrefix, false, edtavWwconctacts_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavWwconctacts_Visible), 5, 0), !bGXsfl_22_Refreshing);
            cmbavUsername.Visible = 0;
            AssignProp(sPrefix, false, cmbavUsername_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavUsername.Visible), 5, 0), true);
         }
         /* Execute user subroutine: 'READ PASSWORDS_AND_TAGS' */
         S112 ();
         if (returnInSub) return;
         WebComp_Editpassword_Visible = 0;
         AssignProp(sPrefix, false, sPrefix+"gxHTMLWrpW0037"+"", "Visible", StringUtil.LTrimStr( (decimal)(WebComp_Editpassword_Visible), 5, 0), true);
         cmbavTagname.removeAllItems();
         cmbavTagname.addItem(StringUtil.StrToGuid( "").ToString(), "Select a Tag", 0);
         AV49GXV4 = 1;
         while ( AV49GXV4 <= AV17Password_tags.Count )
         {
            AV24oneTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV17Password_tags.Item(AV49GXV4));
            cmbavTagname.addItem(AV24oneTag.gxTpr_Tagid.ToString(), AV24oneTag.gxTpr_Name, 0);
            AV49GXV4 = (int)(AV49GXV4+1);
         }
         if ( ! (Guid.Empty==AV31groupId) && AV29group_sdt.gxTpr_Amigroupowner )
         {
            cmbavUsername.removeAllItems();
            cmbavUsername.addItem(StringUtil.StrToGuid( "").ToString(), "Select a Contact", 0);
            AV50GXV5 = 1;
            while ( AV50GXV5 <= AV29group_sdt.gxTpr_Contact.Count )
            {
               AV32oneContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV29group_sdt.gxTpr_Contact.Item(AV50GXV5));
               if ( ! (DateTime.MinValue==AV32oneContact.gxTpr_Contactinvitacionaccepted) )
               {
                  cmbavUsername.addItem(AV32oneContact.gxTpr_Contactid.ToString(), AV32oneContact.gxTpr_Contactprivatename, 0);
               }
               AV50GXV5 = (int)(AV50GXV5+1);
            }
         }
         /* Execute user subroutine: 'LOAD PASSORD FILTERED' */
         S122 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV29group_sdt", AV29group_sdt);
         cmbavTagname.CurrentValue = AV20tagName.ToString();
         AssignProp(sPrefix, false, cmbavTagname_Internalname, "Values", cmbavTagname.ToJavascriptSource(), true);
         cmbavUsername.CurrentValue = AV40userName.ToString();
         AssignProp(sPrefix, false, cmbavUsername_Internalname, "Values", cmbavUsername.ToJavascriptSource(), true);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV19Passwords_and_tags", AV19Passwords_and_tags);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV18Passwords", AV18Passwords);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV17Password_tags", AV17Password_tags);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV26showPasswords", AV26showPasswords);
      }

      protected void E112H2( )
      {
         /* 'Add a new password' Routine */
         returnInSub = false;
         if ( (Guid.Empty==AV31groupId) || AV29group_sdt.gxTpr_Amigroupowner )
         {
            cmbavTagname.Visible = 0;
            AssignProp(sPrefix, false, cmbavTagname_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavTagname.Visible), 5, 0), true);
            bttAddanewpassword_Visible = 0;
            AssignProp(sPrefix, false, bttAddanewpassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttAddanewpassword_Visible), 5, 0), true);
            subPasswordgrid_Visible = 0;
            AssignProp(sPrefix, false, sPrefix+"PasswordgridContainerDiv", "Visible", StringUtil.LTrimStr( (decimal)(subPasswordgrid_Visible), 5, 0), true);
            WebComp_Editpassword_Visible = 1;
            AssignProp(sPrefix, false, sPrefix+"gxHTMLWrpW0037"+"", "Visible", StringUtil.LTrimStr( (decimal)(WebComp_Editpassword_Visible), 5, 0), true);
            AV15onePassword = new GeneXus.Programs.wallet.SdtPassword(context);
            AV22websession.Set("ONE_PASSWORD_TO_ENCR", "");
            /* Object Property */
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               bDynCreated_Editpassword = true;
            }
            if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Editpassword_Component), StringUtil.Lower( "Wallet.EditPassword")) != 0 )
            {
               WebComp_Editpassword = getWebComponent(GetType(), "GeneXus.Programs", "wallet.editpassword", new Object[] {context} );
               WebComp_Editpassword.ComponentInit();
               WebComp_Editpassword.Name = "Wallet.EditPassword";
               WebComp_Editpassword_Component = "Wallet.EditPassword";
            }
            if ( StringUtil.Len( WebComp_Editpassword_Component) != 0 )
            {
               WebComp_Editpassword.setjustcreated();
               WebComp_Editpassword.componentprepare(new Object[] {(string)sPrefix+"W0037",(string)""});
               WebComp_Editpassword.componentbind(new Object[] {});
            }
            if ( isFullAjaxMode( ) || isAjaxCallMode( ) && bDynCreated_Editpassword )
            {
               context.httpAjaxContext.ajax_rspStartCmp(sPrefix+"gxHTMLWrpW0037"+"");
               WebComp_Editpassword.componentdraw();
               context.httpAjaxContext.ajax_rspEndCmp();
            }
         }
         /*  Sending Event outputs  */
      }

      protected void E172H2( )
      {
         AV45GXV1 = nGXsfl_22_idx;
         if ( ( AV45GXV1 > 0 ) && ( AV26showPasswords.Count >= AV45GXV1 ) )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV45GXV1));
         }
         /* 'Delete Password' Routine */
         returnInSub = false;
         this.executeExternalObjectMethod(sPrefix, false, "gx.extensions.web.dialogs", "showConfirm", new Object[] {"Are you sure you want to Delete "+StringUtil.Trim( ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).gxTpr_Description)+" Password?"}, false);
      }

      protected void E122H2( )
      {
         AV45GXV1 = nGXsfl_22_idx;
         if ( ( AV45GXV1 > 0 ) && ( AV26showPasswords.Count >= AV45GXV1 ) )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV45GXV1));
         }
         /* Extensions\Web\Dialog_Onconfirmclosed Routine */
         returnInSub = false;
         if ( AV21UserResponse )
         {
            AV51GXV6 = 1;
            while ( AV51GXV6 <= AV26showPasswords.Count )
            {
               AV12findPassword = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV51GXV6));
               if ( AV12findPassword.gxTpr_Passwordid == ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).gxTpr_Passwordid )
               {
                  AV26showPasswords.RemoveItem(AV26showPasswords.IndexOf(AV12findPassword));
                  gx_BV22 = true;
               }
               AV51GXV6 = (int)(AV51GXV6+1);
            }
            AV19Passwords_and_tags.gxTpr_Password = AV26showPasswords;
            /* Execute user subroutine: 'SAVE PASSWORDS_AND_TAGS' */
            S132 ();
            if (returnInSub) return;
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV26showPasswords", AV26showPasswords);
         nGXsfl_22_bak_idx = nGXsfl_22_idx;
         gxgrPasswordgrid_refresh( AV31groupId, AV17Password_tags, AV29group_sdt, AV11error, AV20tagName, AV18Passwords, AV40userName, AV26showPasswords, sPrefix) ;
         nGXsfl_22_idx = nGXsfl_22_bak_idx;
         sGXsfl_22_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_22_idx), 4, 0), 4, "0");
         SubsflControlProps_222( ) ;
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV19Passwords_and_tags", AV19Passwords_and_tags);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV29group_sdt", AV29group_sdt);
      }

      protected void E182H2( )
      {
         AV45GXV1 = nGXsfl_22_idx;
         if ( ( AV45GXV1 > 0 ) && ( AV26showPasswords.Count >= AV45GXV1 ) )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV45GXV1));
         }
         /* 'Edit Password' Routine */
         returnInSub = false;
         if ( (Guid.Empty==AV31groupId) || AV29group_sdt.gxTpr_Amigroupowner )
         {
            cmbavTagname.Visible = 0;
            AssignProp(sPrefix, false, cmbavTagname_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavTagname.Visible), 5, 0), true);
            cmbavUsername.Visible = 0;
            AssignProp(sPrefix, false, cmbavUsername_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavUsername.Visible), 5, 0), true);
            bttAddanewpassword_Visible = 0;
            AssignProp(sPrefix, false, bttAddanewpassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttAddanewpassword_Visible), 5, 0), true);
            subPasswordgrid_Visible = 0;
            AssignProp(sPrefix, false, sPrefix+"PasswordgridContainerDiv", "Visible", StringUtil.LTrimStr( (decimal)(subPasswordgrid_Visible), 5, 0), true);
            WebComp_Editpassword_Visible = 1;
            AssignProp(sPrefix, false, sPrefix+"gxHTMLWrpW0037"+"", "Visible", StringUtil.LTrimStr( (decimal)(WebComp_Editpassword_Visible), 5, 0), true);
            AV22websession.Set("ONE_PASSWORD_TO_ENCR", ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).ToJSonString(false, true));
            /* Object Property */
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               bDynCreated_Editpassword = true;
            }
            if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Editpassword_Component), StringUtil.Lower( "Wallet.EditPassword")) != 0 )
            {
               WebComp_Editpassword = getWebComponent(GetType(), "GeneXus.Programs", "wallet.editpassword", new Object[] {context} );
               WebComp_Editpassword.ComponentInit();
               WebComp_Editpassword.Name = "Wallet.EditPassword";
               WebComp_Editpassword_Component = "Wallet.EditPassword";
            }
            if ( StringUtil.Len( WebComp_Editpassword_Component) != 0 )
            {
               WebComp_Editpassword.setjustcreated();
               WebComp_Editpassword.componentprepare(new Object[] {(string)sPrefix+"W0037",(string)""});
               WebComp_Editpassword.componentbind(new Object[] {});
            }
            if ( isFullAjaxMode( ) || isAjaxCallMode( ) && bDynCreated_Editpassword )
            {
               context.httpAjaxContext.ajax_rspStartCmp(sPrefix+"gxHTMLWrpW0037"+"");
               WebComp_Editpassword.componentdraw();
               context.httpAjaxContext.ajax_rspEndCmp();
            }
         }
         /*  Sending Event outputs  */
      }

      protected void E132H2( )
      {
         /* GlobalEvents_Donewithpassword Routine */
         returnInSub = false;
         AV15onePassword.FromJSonString(AV22websession.Get("ONE_PASSWORD_TO_ENCR"), null);
         if ( ! (Guid.Empty==AV15onePassword.gxTpr_Passwordid) )
         {
            AV22websession.Set("ONE_PASSWORD_TO_ENCR", "");
            AV52GXV7 = 1;
            while ( AV52GXV7 <= AV18Passwords.Count )
            {
               AV12findPassword = ((GeneXus.Programs.wallet.SdtPassword)AV18Passwords.Item(AV52GXV7));
               if ( AV12findPassword.gxTpr_Passwordid == AV15onePassword.gxTpr_Passwordid )
               {
                  AV18Passwords.RemoveItem(AV18Passwords.IndexOf(AV12findPassword));
               }
               AV52GXV7 = (int)(AV52GXV7+1);
            }
            AV18Passwords.Add(AV15onePassword, 0);
            AV19Passwords_and_tags.gxTpr_Password = AV18Passwords;
            if ( ! AV44isNewPassword )
            {
               new GeneXus.Programs.wallet.savepasswordforgroupusers(context ).execute( ) ;
            }
            /* Execute user subroutine: 'SAVE PASSWORDS_AND_TAGS' */
            S132 ();
            if (returnInSub) return;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            GX_msglist.addItem(AV11error);
         }
         if ( AV29group_sdt.gxTpr_Grouptype == 40 )
         {
            cmbavUsername.Visible = 1;
            AssignProp(sPrefix, false, cmbavUsername_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavUsername.Visible), 5, 0), true);
         }
         cmbavTagname.Visible = 1;
         AssignProp(sPrefix, false, cmbavTagname_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavTagname.Visible), 5, 0), true);
         bttAddanewpassword_Visible = 1;
         AssignProp(sPrefix, false, bttAddanewpassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttAddanewpassword_Visible), 5, 0), true);
         subPasswordgrid_Visible = 1;
         AssignProp(sPrefix, false, sPrefix+"PasswordgridContainerDiv", "Visible", StringUtil.LTrimStr( (decimal)(subPasswordgrid_Visible), 5, 0), true);
         WebComp_Editpassword_Visible = 0;
         AssignProp(sPrefix, false, sPrefix+"gxHTMLWrpW0037"+"", "Visible", StringUtil.LTrimStr( (decimal)(WebComp_Editpassword_Visible), 5, 0), true);
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV18Passwords", AV18Passwords);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV19Passwords_and_tags", AV19Passwords_and_tags);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV29group_sdt", AV29group_sdt);
         cmbavTagname.CurrentValue = AV20tagName.ToString();
         AssignProp(sPrefix, false, cmbavTagname_Internalname, "Values", cmbavTagname.ToJavascriptSource(), true);
         cmbavUsername.CurrentValue = AV40userName.ToString();
         AssignProp(sPrefix, false, cmbavUsername_Internalname, "Values", cmbavUsername.ToJavascriptSource(), true);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV17Password_tags", AV17Password_tags);
         if ( gx_BV22 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV26showPasswords", AV26showPasswords);
            nGXsfl_22_bak_idx = nGXsfl_22_idx;
            gxgrPasswordgrid_refresh( AV31groupId, AV17Password_tags, AV29group_sdt, AV11error, AV20tagName, AV18Passwords, AV40userName, AV26showPasswords, sPrefix) ;
            nGXsfl_22_idx = nGXsfl_22_bak_idx;
            sGXsfl_22_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_22_idx), 4, 0), 4, "0");
            SubsflControlProps_222( ) ;
         }
      }

      protected void S122( )
      {
         /* 'LOAD PASSORD FILTERED' Routine */
         returnInSub = false;
         AV38showPasswordsTags.Clear();
         if ( (Guid.Empty==AV20tagName) )
         {
            AV38showPasswordsTags = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>)(AV18Passwords.Clone());
         }
         else
         {
            AV53GXV8 = 1;
            while ( AV53GXV8 <= AV18Passwords.Count )
            {
               AV15onePassword = ((GeneXus.Programs.wallet.SdtPassword)AV18Passwords.Item(AV53GXV8));
               AV25tagFound = false;
               AV54GXV9 = 1;
               while ( AV54GXV9 <= AV15onePassword.gxTpr_Password_tag.Count )
               {
                  AV24oneTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV15onePassword.gxTpr_Password_tag.Item(AV54GXV9));
                  if ( AV24oneTag.gxTpr_Tagid == AV20tagName )
                  {
                     AV25tagFound = true;
                     if (true) break;
                  }
                  AV54GXV9 = (int)(AV54GXV9+1);
               }
               if ( AV25tagFound )
               {
                  AV38showPasswordsTags.Add(AV15onePassword, 0);
               }
               AV53GXV8 = (int)(AV53GXV8+1);
            }
         }
         AV37showPasswordsNames.Clear();
         if ( (Guid.Empty==AV40userName) )
         {
            AV37showPasswordsNames = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>)(AV18Passwords.Clone());
         }
         else
         {
            AV55GXV10 = 1;
            while ( AV55GXV10 <= AV18Passwords.Count )
            {
               AV15onePassword = ((GeneXus.Programs.wallet.SdtPassword)AV18Passwords.Item(AV55GXV10));
               AV39userFound = false;
               AV56GXV11 = 1;
               while ( AV56GXV11 <= AV15onePassword.gxTpr_Contact.Count )
               {
                  AV35passwordContactId = ((Guid)AV15onePassword.gxTpr_Contact.Item(AV56GXV11));
                  if ( AV35passwordContactId == AV40userName )
                  {
                     AV39userFound = true;
                     if (true) break;
                  }
                  AV56GXV11 = (int)(AV56GXV11+1);
               }
               if ( AV39userFound )
               {
                  AV37showPasswordsNames.Add(AV15onePassword, 0);
               }
               AV55GXV10 = (int)(AV55GXV10+1);
            }
         }
         AV26showPasswords.Clear();
         gx_BV22 = true;
         AV57GXV12 = 1;
         while ( AV57GXV12 <= AV38showPasswordsTags.Count )
         {
            AV34onePasswordTags = ((GeneXus.Programs.wallet.SdtPassword)AV38showPasswordsTags.Item(AV57GXV12));
            AV58GXV13 = 1;
            while ( AV58GXV13 <= AV37showPasswordsNames.Count )
            {
               AV33onePasswordNames = ((GeneXus.Programs.wallet.SdtPassword)AV37showPasswordsNames.Item(AV58GXV13));
               if ( AV34onePasswordTags.gxTpr_Passwordid == AV33onePasswordNames.gxTpr_Passwordid )
               {
                  AV26showPasswords.Add(AV33onePasswordNames, 0);
                  gx_BV22 = true;
               }
               AV58GXV13 = (int)(AV58GXV13+1);
            }
            AV57GXV12 = (int)(AV57GXV12+1);
         }
      }

      protected void E142H2( )
      {
         /* Tagname_Controlvaluechanged Routine */
         returnInSub = false;
         /* Execute user subroutine: 'LOAD PASSORD FILTERED' */
         S122 ();
         if (returnInSub) return;
         gxgrPasswordgrid_refresh( AV31groupId, AV17Password_tags, AV29group_sdt, AV11error, AV20tagName, AV18Passwords, AV40userName, AV26showPasswords, sPrefix) ;
         /*  Sending Event outputs  */
         if ( gx_BV22 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV26showPasswords", AV26showPasswords);
            nGXsfl_22_bak_idx = nGXsfl_22_idx;
            gxgrPasswordgrid_refresh( AV31groupId, AV17Password_tags, AV29group_sdt, AV11error, AV20tagName, AV18Passwords, AV40userName, AV26showPasswords, sPrefix) ;
            nGXsfl_22_idx = nGXsfl_22_bak_idx;
            sGXsfl_22_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_22_idx), 4, 0), 4, "0");
            SubsflControlProps_222( ) ;
         }
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV29group_sdt", AV29group_sdt);
         cmbavTagname.CurrentValue = AV20tagName.ToString();
         AssignProp(sPrefix, false, cmbavTagname_Internalname, "Values", cmbavTagname.ToJavascriptSource(), true);
         cmbavUsername.CurrentValue = AV40userName.ToString();
         AssignProp(sPrefix, false, cmbavUsername_Internalname, "Values", cmbavUsername.ToJavascriptSource(), true);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV19Passwords_and_tags", AV19Passwords_and_tags);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV18Passwords", AV18Passwords);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV17Password_tags", AV17Password_tags);
      }

      protected void E152H2( )
      {
         /* Username_Controlvaluechanged Routine */
         returnInSub = false;
         /* Execute user subroutine: 'LOAD PASSORD FILTERED' */
         S122 ();
         if (returnInSub) return;
         gxgrPasswordgrid_refresh( AV31groupId, AV17Password_tags, AV29group_sdt, AV11error, AV20tagName, AV18Passwords, AV40userName, AV26showPasswords, sPrefix) ;
         /*  Sending Event outputs  */
         if ( gx_BV22 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV26showPasswords", AV26showPasswords);
            nGXsfl_22_bak_idx = nGXsfl_22_idx;
            gxgrPasswordgrid_refresh( AV31groupId, AV17Password_tags, AV29group_sdt, AV11error, AV20tagName, AV18Passwords, AV40userName, AV26showPasswords, sPrefix) ;
            nGXsfl_22_idx = nGXsfl_22_bak_idx;
            sGXsfl_22_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_22_idx), 4, 0), 4, "0");
            SubsflControlProps_222( ) ;
         }
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV29group_sdt", AV29group_sdt);
         cmbavTagname.CurrentValue = AV20tagName.ToString();
         AssignProp(sPrefix, false, cmbavTagname_Internalname, "Values", cmbavTagname.ToJavascriptSource(), true);
         cmbavUsername.CurrentValue = AV40userName.ToString();
         AssignProp(sPrefix, false, cmbavUsername_Internalname, "Values", cmbavUsername.ToJavascriptSource(), true);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV19Passwords_and_tags", AV19Passwords_and_tags);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV18Passwords", AV18Passwords);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV17Password_tags", AV17Password_tags);
      }

      protected void E192H2( )
      {
         AV45GXV1 = nGXsfl_22_idx;
         if ( ( AV45GXV1 > 0 ) && ( AV26showPasswords.Count >= AV45GXV1 ) )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV45GXV1));
         }
         /* 'view Note' Routine */
         returnInSub = false;
         AV22websession.Set("ONE_PASSWORD_TO_ENCR", ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).ToJSonString(false, true));
         context.PopUp(formatLink("wallet.viewnote") , new Object[] {});
         /*  Sending Event outputs  */
      }

      private void E202H2( )
      {
         /* Passwordgrid_Load Routine */
         returnInSub = false;
         AV45GXV1 = 1;
         while ( AV45GXV1 <= AV26showPasswords.Count )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV45GXV1));
            edtavDeleteimage_gximage = "GeneXusUnanimo_delete_light";
            AV8deleteImage = context.GetImagePath( "db0f63cd-dde8-4bf7-aca2-01cdf8d3c157", "", context.GetTheme( ));
            AssignAttri(sPrefix, false, edtavDeleteimage_Internalname, AV8deleteImage);
            AV59Deleteimage_GXI = GXDbFile.PathToUrl( context.GetImagePath( "db0f63cd-dde8-4bf7-aca2-01cdf8d3c157", "", context.GetTheme( )), context);
            edtavEditimage_gximage = "GeneXusUnanimo_edit_light";
            AV10editImage = context.GetImagePath( "f20f0a60-bf65-4ff0-a2d0-9392c759340b", "", context.GetTheme( ));
            AssignAttri(sPrefix, false, edtavEditimage_Internalname, AV10editImage);
            AV60Editimage_GXI = GXDbFile.PathToUrl( context.GetImagePath( "f20f0a60-bf65-4ff0-a2d0-9392c759340b", "", context.GetTheme( )), context);
            edtavCopylogin_gximage = "Wallet_copy";
            AV6copyLogin = context.GetImagePath( "d7ac116e-c7e5-4d68-ae44-0790c20de236", "", context.GetTheme( ));
            AssignAttri(sPrefix, false, edtavCopylogin_Internalname, AV6copyLogin);
            AV61Copylogin_GXI = GXDbFile.PathToUrl( context.GetImagePath( "d7ac116e-c7e5-4d68-ae44-0790c20de236", "", context.GetTheme( )), context);
            edtavCopypassword_gximage = "Wallet_copy";
            AV7copyPassword = context.GetImagePath( "d7ac116e-c7e5-4d68-ae44-0790c20de236", "", context.GetTheme( ));
            AssignAttri(sPrefix, false, edtavCopypassword_Internalname, AV7copyPassword);
            AV62Copypassword_GXI = GXDbFile.PathToUrl( context.GetImagePath( "d7ac116e-c7e5-4d68-ae44-0790c20de236", "", context.GetTheme( )), context);
            edtavCopyauthenticator_gximage = "Wallet_copy";
            AV5copyAuthenticator = context.GetImagePath( "d7ac116e-c7e5-4d68-ae44-0790c20de236", "", context.GetTheme( ));
            AssignAttri(sPrefix, false, edtavCopyauthenticator_Internalname, AV5copyAuthenticator);
            AV63Copyauthenticator_GXI = GXDbFile.PathToUrl( context.GetImagePath( "d7ac116e-c7e5-4d68-ae44-0790c20de236", "", context.GetTheme( )), context);
            edtavWwtags_gximage = "Wallet_tag";
            AV23wwTags = context.GetImagePath( "dd232ae9-7426-400c-a260-b9f5237f9ac5", "", context.GetTheme( ));
            AssignAttri(sPrefix, false, edtavWwtags_Internalname, AV23wwTags);
            AV64Wwtags_GXI = GXDbFile.PathToUrl( context.GetImagePath( "dd232ae9-7426-400c-a260-b9f5237f9ac5", "", context.GetTheme( )), context);
            edtavViewnote_gximage = "GeneXusUnanimo_info_ico_purple";
            AV43viewNote = context.GetImagePath( "b21dc1b3-b96d-4c02-bbdd-14e2b54cccc4", "", context.GetTheme( ));
            AssignAttri(sPrefix, false, edtavViewnote_Internalname, AV43viewNote);
            AV65Viewnote_GXI = GXDbFile.PathToUrl( context.GetImagePath( "b21dc1b3-b96d-4c02-bbdd-14e2b54cccc4", "", context.GetTheme( )), context);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).gxTpr_Note)) )
            {
               edtavViewnote_Visible = 0;
            }
            else
            {
               edtavViewnote_Visible = 1;
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).gxTpr_Based32key)) )
            {
               edtavCopyauthenticator_Visible = 0;
            }
            else
            {
               edtavCopyauthenticator_Visible = 1;
            }
            AV16pass = ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).gxTpr_Password;
            AssignAttri(sPrefix, false, edtavPass_Internalname, AV16pass);
            AV9description = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).gxTpr_Description);
            AssignAttri(sPrefix, false, edtavDescription_Internalname, AV9description);
            edtavDescription_Link = StringUtil.Trim( ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).gxTpr_Url);
            edtavDescription_Linktarget = "_blank";
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 22;
            }
            sendrow_222( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_22_Refreshing )
            {
               DoAjaxLoad(22, PasswordgridRow);
            }
            AV45GXV1 = (int)(AV45GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E212H2( )
      {
         AV45GXV1 = nGXsfl_22_idx;
         if ( ( AV45GXV1 > 0 ) && ( AV26showPasswords.Count >= AV45GXV1 ) )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV45GXV1));
         }
         /* 'Copy login' Routine */
         returnInSub = false;
         this.executeUsercontrolMethod(sPrefix, false, "UC_CLIPBOARD_V11Container", "setText", "", new Object[] {StringUtil.Trim( ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).gxTpr_Login)});
         this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Login Copied",(string)""}, true);
      }

      protected void E222H2( )
      {
         AV45GXV1 = nGXsfl_22_idx;
         if ( ( AV45GXV1 > 0 ) && ( AV26showPasswords.Count >= AV45GXV1 ) )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV45GXV1));
         }
         /* 'Copy password' Routine */
         returnInSub = false;
         this.executeUsercontrolMethod(sPrefix, false, "UC_CLIPBOARD_V11Container", "setText", "", new Object[] {StringUtil.Trim( ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).gxTpr_Password)});
         this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Password Copied",(string)""}, true);
      }

      protected void E232H2( )
      {
         AV45GXV1 = nGXsfl_22_idx;
         if ( ( AV45GXV1 > 0 ) && ( AV26showPasswords.Count >= AV45GXV1 ) )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV45GXV1));
         }
         /* 'Copy authenticator' Routine */
         returnInSub = false;
         GXt_char2 = AV11error;
         new GeneXus.Programs.googleauthenticator.getcurrentpin(context ).execute(  StringUtil.Trim( ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).gxTpr_Based32key), out  AV13generatedPIN, out  GXt_char2) ;
         AV11error = GXt_char2;
         AssignAttri(sPrefix, false, "AV11error", AV11error);
         this.executeUsercontrolMethod(sPrefix, false, "UC_CLIPBOARD_V11Container", "setText", "", new Object[] {StringUtil.Trim( AV13generatedPIN)});
         this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"PIN Copied",(string)""}, true);
         /*  Sending Event outputs  */
      }

      protected void E242H2( )
      {
         AV45GXV1 = nGXsfl_22_idx;
         if ( ( AV45GXV1 > 0 ) && ( AV26showPasswords.Count >= AV45GXV1 ) )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV45GXV1));
         }
         /* 'wwtags' Routine */
         returnInSub = false;
         AV22websession.Set("ONE_PASSWORD_TO_WWTAGS", ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).ToJSonString(false, true));
         context.PopUp(formatLink("wallet.wwtags") , new Object[] {});
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV29group_sdt", AV29group_sdt);
         cmbavTagname.CurrentValue = AV20tagName.ToString();
         AssignProp(sPrefix, false, cmbavTagname_Internalname, "Values", cmbavTagname.ToJavascriptSource(), true);
         cmbavUsername.CurrentValue = AV40userName.ToString();
         AssignProp(sPrefix, false, cmbavUsername_Internalname, "Values", cmbavUsername.ToJavascriptSource(), true);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV19Passwords_and_tags", AV19Passwords_and_tags);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV18Passwords", AV18Passwords);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV17Password_tags", AV17Password_tags);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV26showPasswords", AV26showPasswords);
         nGXsfl_22_bak_idx = nGXsfl_22_idx;
         gxgrPasswordgrid_refresh( AV31groupId, AV17Password_tags, AV29group_sdt, AV11error, AV20tagName, AV18Passwords, AV40userName, AV26showPasswords, sPrefix) ;
         nGXsfl_22_idx = nGXsfl_22_bak_idx;
         sGXsfl_22_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_22_idx), 4, 0), 4, "0");
         SubsflControlProps_222( ) ;
      }

      protected void E252H2( )
      {
         AV45GXV1 = nGXsfl_22_idx;
         if ( ( AV45GXV1 > 0 ) && ( AV26showPasswords.Count >= AV45GXV1 ) )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV45GXV1));
         }
         /* 'wwContacts' Routine */
         returnInSub = false;
         AV22websession.Set("ONE_PASSWORD_TO_WWCONTACTS", ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).ToJSonString(false, true));
         context.PopUp(formatLink("wallet.registered.wwcontacts") , new Object[] {});
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV29group_sdt", AV29group_sdt);
         cmbavTagname.CurrentValue = AV20tagName.ToString();
         AssignProp(sPrefix, false, cmbavTagname_Internalname, "Values", cmbavTagname.ToJavascriptSource(), true);
         cmbavUsername.CurrentValue = AV40userName.ToString();
         AssignProp(sPrefix, false, cmbavUsername_Internalname, "Values", cmbavUsername.ToJavascriptSource(), true);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV19Passwords_and_tags", AV19Passwords_and_tags);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV18Passwords", AV18Passwords);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV17Password_tags", AV17Password_tags);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV26showPasswords", AV26showPasswords);
         nGXsfl_22_bak_idx = nGXsfl_22_idx;
         gxgrPasswordgrid_refresh( AV31groupId, AV17Password_tags, AV29group_sdt, AV11error, AV20tagName, AV18Passwords, AV40userName, AV26showPasswords, sPrefix) ;
         nGXsfl_22_idx = nGXsfl_22_bak_idx;
         sGXsfl_22_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_22_idx), 4, 0), 4, "0");
         SubsflControlProps_222( ) ;
      }

      protected void S112( )
      {
         /* 'READ PASSWORDS_AND_TAGS' Routine */
         returnInSub = false;
         AV29group_sdt.FromJSonString(AV22websession.Get("Group_EDIT"), null);
         if ( AV29group_sdt.gxTpr_Grouptype == 40 )
         {
            if ( AV29group_sdt.gxTpr_Amigroupowner )
            {
               GXt_char2 = AV11error;
               new GeneXus.Programs.distributedcryptographylib.decryptjson(context ).execute(  AV29group_sdt.gxTpr_Encryptedtextshare,  AV29group_sdt.gxTpr_Encpassword, out  AV27clearText, out  GXt_char2) ;
               AV11error = GXt_char2;
               AssignAttri(sPrefix, false, "AV11error", AV11error);
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  if ( StringUtil.StrCmp(AV27clearText, "_empty_") == 0 )
                  {
                     AV27clearText = "";
                  }
                  AV19Passwords_and_tags.FromJSonString(AV27clearText, null);
                  AV18Passwords = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>)(AV19Passwords_and_tags.gxTpr_Password.Clone());
                  AV17Password_tags = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>)(AV19Passwords_and_tags.gxTpr_Password_tag.Clone());
               }
               else
               {
                  GX_msglist.addItem(AV11error);
               }
            }
            else
            {
               GXt_SdtExternalUser3 = AV28externalUser;
               new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser3) ;
               AV28externalUser = GXt_SdtExternalUser3;
               GXt_char2 = AV11error;
               new GeneXus.Programs.wallet.registered.getgroupbyid(context ).execute(  AV29group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid,  AV29group_sdt.gxTpr_Othergroup.gxTpr_Encpassword, out  AV29group_sdt, out  GXt_char2) ;
               AV11error = GXt_char2;
               AssignAttri(sPrefix, false, "AV11error", AV11error);
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  AV66GXV14 = 1;
                  while ( AV66GXV14 <= AV29group_sdt.gxTpr_Contact.Count )
                  {
                     AV30groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV29group_sdt.gxTpr_Contact.Item(AV66GXV14));
                     if ( StringUtil.StrCmp(StringUtil.Trim( AV30groupContact.gxTpr_Contactusername), StringUtil.Trim( AV28externalUser.gxTpr_Userinfo.gxTpr_Username)) == 0 )
                     {
                        GXt_char2 = AV11error;
                        new GeneXus.Programs.distributedcryptographylib.decryptjsonfor(context ).execute(  AV30groupContact.gxTpr_Contactencryptedtext,  AV30groupContact.gxTpr_Contactencryptedkey,  AV28externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey, out  AV42passwords_and_tags_Json, out  GXt_char2) ;
                        AV11error = GXt_char2;
                        AssignAttri(sPrefix, false, "AV11error", AV11error);
                        AV19Passwords_and_tags.FromJSonString(AV42passwords_and_tags_Json, null);
                        AV18Passwords = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>)(AV19Passwords_and_tags.gxTpr_Password.Clone());
                        AV17Password_tags = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>)(AV19Passwords_and_tags.gxTpr_Password_tag.Clone());
                        if (true) break;
                     }
                     AV66GXV14 = (int)(AV66GXV14+1);
                  }
               }
               else
               {
                  GX_msglist.addItem("We have problems reading the group from the server: "+AV11error);
               }
            }
         }
         else
         {
            AV19Passwords_and_tags.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "encpasswords.enc", out  AV11error), null);
            AV18Passwords = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>)(AV19Passwords_and_tags.gxTpr_Password.Clone());
            AV17Password_tags = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>)(AV19Passwords_and_tags.gxTpr_Password_tag.Clone());
         }
      }

      protected void S132( )
      {
         /* 'SAVE PASSWORDS_AND_TAGS' Routine */
         returnInSub = false;
         AV29group_sdt.FromJSonString(AV22websession.Get("Group_EDIT"), null);
         if ( AV29group_sdt.gxTpr_Grouptype == 40 )
         {
            if ( AV29group_sdt.gxTpr_Amigroupowner )
            {
               GXt_char2 = AV11error;
               GXt_char4 = AV29group_sdt.gxTpr_Encpassword;
               GXt_char5 = AV29group_sdt.gxTpr_Encryptedtextshare;
               new GeneXus.Programs.distributedcryptographylib.encryptjson(context ).execute(  AV19Passwords_and_tags.ToJSonString(false, true),  "", out  GXt_char4, out  GXt_char5, out  GXt_char2) ;
               AV29group_sdt.gxTpr_Encpassword = GXt_char4;
               AV29group_sdt.gxTpr_Encryptedtextshare = GXt_char5;
               AV11error = GXt_char2;
               AssignAttri(sPrefix, false, "AV11error", AV11error);
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
               {
                  GXt_char5 = AV11error;
                  new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV29group_sdt,  StringUtil.Trim( AV29group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV36returnGroupId, out  GXt_char5) ;
                  AV11error = GXt_char5;
                  AssignAttri(sPrefix, false, "AV11error", AV11error);
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
                  {
                     GXt_char5 = AV11error;
                     new GeneXus.Programs.wallet.registered.updategrouponlocalfiles(context ).execute(  AV29group_sdt, out  GXt_char5) ;
                     AV11error = GXt_char5;
                     AssignAttri(sPrefix, false, "AV11error", AV11error);
                  }
                  else
                  {
                     GX_msglist.addItem("There was an error updating group on server: "+AV11error);
                  }
               }
               else
               {
                  GX_msglist.addItem("There was an error encrypting Password: "+AV11error);
               }
            }
         }
         else
         {
            GXt_char5 = AV11error;
            new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "encpasswords.enc",  AV19Passwords_and_tags.ToJSonString(false, true), out  GXt_char5) ;
            AV11error = GXt_char5;
            AssignAttri(sPrefix, false, "AV11error", AV11error);
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV31groupId = (Guid)getParm(obj,0);
         AssignAttri(sPrefix, false, "AV31groupId", AV31groupId.ToString());
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
         PA2H2( ) ;
         WS2H2( ) ;
         WE2H2( ) ;
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
         sCtrlAV31groupId = (string)((string)getParm(obj,0));
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA2H2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\passwordswc", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA2H2( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
            AV31groupId = (Guid)getParm(obj,2);
            AssignAttri(sPrefix, false, "AV31groupId", AV31groupId.ToString());
         }
         wcpOAV31groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"wcpOAV31groupId"));
         if ( ! GetJustCreated( ) && ( ( AV31groupId != wcpOAV31groupId ) ) )
         {
            setjustcreated();
         }
         wcpOAV31groupId = AV31groupId;
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
         sCtrlAV31groupId = cgiGet( sPrefix+"AV31groupId_CTRL");
         if ( StringUtil.Len( sCtrlAV31groupId) > 0 )
         {
            AV31groupId = StringUtil.StrToGuid( cgiGet( sCtrlAV31groupId));
            AssignAttri(sPrefix, false, "AV31groupId", AV31groupId.ToString());
         }
         else
         {
            AV31groupId = StringUtil.StrToGuid( cgiGet( sPrefix+"AV31groupId_PARM"));
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
         PA2H2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS2H2( ) ;
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
         WS2H2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"AV31groupId_PARM", AV31groupId.ToString());
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV31groupId)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV31groupId_CTRL", StringUtil.RTrim( sCtrlAV31groupId));
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
         WE2H2( ) ;
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
         if ( ! ( WebComp_Editpassword == null ) )
         {
            WebComp_Editpassword.componentjscripts();
         }
      }

      public override void componentthemes( )
      {
         define_styles( ) ;
      }

      protected void define_styles( )
      {
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         if ( ! ( WebComp_Editpassword == null ) )
         {
            if ( StringUtil.Len( WebComp_Editpassword_Component) != 0 )
            {
               WebComp_Editpassword.componentthemes();
            }
         }
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20251011435235", true, true);
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
         context.AddJavascriptSource("wallet/passwordswc.js", "?20251011435237", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         context.AddJavascriptSource("UserControls/Wallet.UC_Clipboard_v1Render.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_222( )
      {
         edtavCtlpasswordid_Internalname = sPrefix+"CTLPASSWORDID_"+sGXsfl_22_idx;
         edtavDescription_Internalname = sPrefix+"vDESCRIPTION_"+sGXsfl_22_idx;
         edtavCtllogin_Internalname = sPrefix+"CTLLOGIN_"+sGXsfl_22_idx;
         edtavCopylogin_Internalname = sPrefix+"vCOPYLOGIN_"+sGXsfl_22_idx;
         edtavPass_Internalname = sPrefix+"vPASS_"+sGXsfl_22_idx;
         edtavCopypassword_Internalname = sPrefix+"vCOPYPASSWORD_"+sGXsfl_22_idx;
         edtavCopyauthenticator_Internalname = sPrefix+"vCOPYAUTHENTICATOR_"+sGXsfl_22_idx;
         edtavViewnote_Internalname = sPrefix+"vVIEWNOTE_"+sGXsfl_22_idx;
         edtavEditimage_Internalname = sPrefix+"vEDITIMAGE_"+sGXsfl_22_idx;
         edtavDeleteimage_Internalname = sPrefix+"vDELETEIMAGE_"+sGXsfl_22_idx;
         edtavWwtags_Internalname = sPrefix+"vWWTAGS_"+sGXsfl_22_idx;
         edtavWwconctacts_Internalname = sPrefix+"vWWCONCTACTS_"+sGXsfl_22_idx;
      }

      protected void SubsflControlProps_fel_222( )
      {
         edtavCtlpasswordid_Internalname = sPrefix+"CTLPASSWORDID_"+sGXsfl_22_fel_idx;
         edtavDescription_Internalname = sPrefix+"vDESCRIPTION_"+sGXsfl_22_fel_idx;
         edtavCtllogin_Internalname = sPrefix+"CTLLOGIN_"+sGXsfl_22_fel_idx;
         edtavCopylogin_Internalname = sPrefix+"vCOPYLOGIN_"+sGXsfl_22_fel_idx;
         edtavPass_Internalname = sPrefix+"vPASS_"+sGXsfl_22_fel_idx;
         edtavCopypassword_Internalname = sPrefix+"vCOPYPASSWORD_"+sGXsfl_22_fel_idx;
         edtavCopyauthenticator_Internalname = sPrefix+"vCOPYAUTHENTICATOR_"+sGXsfl_22_fel_idx;
         edtavViewnote_Internalname = sPrefix+"vVIEWNOTE_"+sGXsfl_22_fel_idx;
         edtavEditimage_Internalname = sPrefix+"vEDITIMAGE_"+sGXsfl_22_fel_idx;
         edtavDeleteimage_Internalname = sPrefix+"vDELETEIMAGE_"+sGXsfl_22_fel_idx;
         edtavWwtags_Internalname = sPrefix+"vWWTAGS_"+sGXsfl_22_fel_idx;
         edtavWwconctacts_Internalname = sPrefix+"vWWCONCTACTS_"+sGXsfl_22_fel_idx;
      }

      protected void sendrow_222( )
      {
         sGXsfl_22_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_22_idx), 4, 0), 4, "0");
         SubsflControlProps_222( ) ;
         WB2H0( ) ;
         PasswordgridRow = GXWebRow.GetNew(context,PasswordgridContainer);
         if ( subPasswordgrid_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subPasswordgrid_Backstyle = 0;
            if ( StringUtil.StrCmp(subPasswordgrid_Class, "") != 0 )
            {
               subPasswordgrid_Linesclass = subPasswordgrid_Class+"Odd";
            }
         }
         else if ( subPasswordgrid_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subPasswordgrid_Backstyle = 0;
            subPasswordgrid_Backcolor = subPasswordgrid_Allbackcolor;
            if ( StringUtil.StrCmp(subPasswordgrid_Class, "") != 0 )
            {
               subPasswordgrid_Linesclass = subPasswordgrid_Class+"Uniform";
            }
         }
         else if ( subPasswordgrid_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subPasswordgrid_Backstyle = 1;
            if ( StringUtil.StrCmp(subPasswordgrid_Class, "") != 0 )
            {
               subPasswordgrid_Linesclass = subPasswordgrid_Class+"Odd";
            }
            subPasswordgrid_Backcolor = (int)(0x0);
         }
         else if ( subPasswordgrid_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subPasswordgrid_Backstyle = 1;
            if ( ((int)((nGXsfl_22_idx) % (2))) == 0 )
            {
               subPasswordgrid_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subPasswordgrid_Class, "") != 0 )
               {
                  subPasswordgrid_Linesclass = subPasswordgrid_Class+"Even";
               }
            }
            else
            {
               subPasswordgrid_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subPasswordgrid_Class, "") != 0 )
               {
                  subPasswordgrid_Linesclass = subPasswordgrid_Class+"Odd";
               }
            }
         }
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr ") ;
            context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
            context.WriteHtmlText( " gxrow=\""+sGXsfl_22_idx+"\">") ;
         }
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+"display:none;"+"\">") ;
         }
         /* Single line edit */
         ROClassString = "Attribute";
         PasswordgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlpasswordid_Internalname,((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV45GXV1)).gxTpr_Passwordid.ToString(),((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV45GXV1)).gxTpr_Passwordid.ToString(),""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlpasswordid_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtlpasswordid_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)36,(short)0,(short)0,(short)22,(short)0,(short)0,(short)0,(bool)true,(string)"",(string)"",(bool)false,(string)""});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 24,'" + sPrefix + "',false,'" + sGXsfl_22_idx + "',22)\"";
         ROClassString = "Attribute";
         PasswordgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDescription_Internalname,StringUtil.RTrim( AV9description),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,24);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)edtavDescription_Link,(string)edtavDescription_Linktarget,(string)"",(string)"",(string)edtavDescription_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavDescription_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)80,(short)0,(short)0,(short)22,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 25,'" + sPrefix + "',false,'" + sGXsfl_22_idx + "',22)\"";
         ROClassString = "Attribute";
         PasswordgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtllogin_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV45GXV1)).gxTpr_Login),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,25);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtllogin_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtllogin_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)80,(short)0,(short)0,(short)22,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 26,'" + sPrefix + "',false,'',22)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavCopylogin_gximage, "")==0) ? "" : "GX_Image_"+edtavCopylogin_gximage+"_Class");
         StyleString = "";
         AV6copyLogin_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV6copyLogin))&&String.IsNullOrEmpty(StringUtil.RTrim( AV61Copylogin_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV6copyLogin)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV6copyLogin)) ? AV61Copylogin_GXI : context.PathToRelativeUrl( AV6copyLogin));
         PasswordgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavCopylogin_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(short)-1,(short)1,(string)"",(string)"copy login",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavCopylogin_Jsonclick,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'COPY LOGIN\\'."+sGXsfl_22_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV6copyLogin_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl),(string)"none"});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 27,'" + sPrefix + "',false,'" + sGXsfl_22_idx + "',22)\"";
         ROClassString = "Attribute";
         PasswordgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavPass_Internalname,StringUtil.RTrim( AV16pass),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,27);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavPass_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavPass_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)10,(short)-1,(short)0,(short)22,(short)0,(short)0,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 28,'" + sPrefix + "',false,'',22)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavCopypassword_gximage, "")==0) ? "" : "GX_Image_"+edtavCopypassword_gximage+"_Class");
         StyleString = "";
         AV7copyPassword_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV7copyPassword))&&String.IsNullOrEmpty(StringUtil.RTrim( AV62Copypassword_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV7copyPassword)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV7copyPassword)) ? AV62Copypassword_GXI : context.PathToRelativeUrl( AV7copyPassword));
         PasswordgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavCopypassword_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(short)-1,(short)1,(string)"",(string)"copy password",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavCopypassword_Jsonclick,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'COPY PASSWORD\\'."+sGXsfl_22_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV7copyPassword_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl),(string)"none"});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+((edtavCopyauthenticator_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 29,'" + sPrefix + "',false,'',22)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavCopyauthenticator_gximage, "")==0) ? "" : "GX_Image_"+edtavCopyauthenticator_gximage+"_Class");
         StyleString = "";
         AV5copyAuthenticator_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV5copyAuthenticator))&&String.IsNullOrEmpty(StringUtil.RTrim( AV63Copyauthenticator_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV5copyAuthenticator)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV5copyAuthenticator)) ? AV63Copyauthenticator_GXI : context.PathToRelativeUrl( AV5copyAuthenticator));
         PasswordgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavCopyauthenticator_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(int)edtavCopyauthenticator_Visible,(short)1,(string)"",(string)"copy PIN",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavCopyauthenticator_Jsonclick,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'COPY AUTHENTICATOR\\'."+sGXsfl_22_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV5copyAuthenticator_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl),(string)"none"});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+((edtavViewnote_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 30,'" + sPrefix + "',false,'',22)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavViewnote_gximage, "")==0) ? "" : "GX_Image_"+edtavViewnote_gximage+"_Class");
         StyleString = "";
         AV43viewNote_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV43viewNote))&&String.IsNullOrEmpty(StringUtil.RTrim( AV65Viewnote_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV43viewNote)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV43viewNote)) ? AV65Viewnote_GXI : context.PathToRelativeUrl( AV43viewNote));
         PasswordgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavViewnote_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(int)edtavViewnote_Visible,(short)1,(string)"",(string)"View Note",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavViewnote_Jsonclick,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'VIEW NOTE\\'."+sGXsfl_22_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV43viewNote_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl),(string)"none"});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+((edtavEditimage_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 31,'" + sPrefix + "',false,'',22)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavEditimage_gximage, "")==0) ? "" : "GX_Image_"+edtavEditimage_gximage+"_Class");
         StyleString = "";
         AV10editImage_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV10editImage))&&String.IsNullOrEmpty(StringUtil.RTrim( AV60Editimage_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV10editImage)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV10editImage)) ? AV60Editimage_GXI : context.PathToRelativeUrl( AV10editImage));
         PasswordgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavEditimage_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(int)edtavEditimage_Visible,(short)1,(string)"",(string)"edit",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavEditimage_Jsonclick,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'EDIT PASSWORD\\'."+sGXsfl_22_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV10editImage_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl),(string)"none"});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+((edtavDeleteimage_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 32,'" + sPrefix + "',false,'',22)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavDeleteimage_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteimage_gximage+"_Class");
         StyleString = "";
         AV8deleteImage_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV8deleteImage))&&String.IsNullOrEmpty(StringUtil.RTrim( AV59Deleteimage_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV8deleteImage)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV8deleteImage)) ? AV59Deleteimage_GXI : context.PathToRelativeUrl( AV8deleteImage));
         PasswordgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavDeleteimage_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(int)edtavDeleteimage_Visible,(short)1,(string)"",(string)"delete",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavDeleteimage_Jsonclick,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'DELETE PASSWORD\\'."+sGXsfl_22_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV8deleteImage_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl),(string)"none"});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+((edtavWwtags_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 33,'" + sPrefix + "',false,'',22)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavWwtags_gximage, "")==0) ? "" : "GX_Image_"+edtavWwtags_gximage+"_Class");
         StyleString = "";
         AV23wwTags_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV23wwTags))&&String.IsNullOrEmpty(StringUtil.RTrim( AV64Wwtags_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV23wwTags)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV23wwTags)) ? AV64Wwtags_GXI : context.PathToRelativeUrl( AV23wwTags));
         PasswordgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavWwtags_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(int)edtavWwtags_Visible,(short)1,(string)"",(string)"work with Tags",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavWwtags_Jsonclick,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'WWTAGS\\'."+sGXsfl_22_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV23wwTags_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl),(string)"none"});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+((edtavWwconctacts_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 34,'" + sPrefix + "',false,'',22)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavWwconctacts_gximage, "")==0) ? "" : "GX_Image_"+edtavWwconctacts_gximage+"_Class");
         StyleString = "";
         AV41wwConctacts_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV41wwConctacts))&&String.IsNullOrEmpty(StringUtil.RTrim( AV48Wwconctacts_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV41wwConctacts)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV41wwConctacts)) ? AV48Wwconctacts_GXI : context.PathToRelativeUrl( AV41wwConctacts));
         PasswordgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavWwconctacts_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(int)edtavWwconctacts_Visible,(short)1,(string)"",(string)"work with Contacts",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavWwconctacts_Jsonclick,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'WWCONTACTS\\'."+sGXsfl_22_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV41wwConctacts_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl),(string)"none"});
         send_integrity_lvl_hashes2H2( ) ;
         PasswordgridContainer.AddRow(PasswordgridRow);
         nGXsfl_22_idx = ((subPasswordgrid_Islastpage==1)&&(nGXsfl_22_idx+1>subPasswordgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_22_idx+1);
         sGXsfl_22_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_22_idx), 4, 0), 4, "0");
         SubsflControlProps_222( ) ;
         /* End function sendrow_222 */
      }

      protected void init_web_controls( )
      {
         cmbavTagname.Name = "vTAGNAME";
         cmbavTagname.WebTags = "";
         if ( cmbavTagname.ItemCount > 0 )
         {
         }
         cmbavUsername.Name = "vUSERNAME";
         cmbavUsername.WebTags = "";
         if ( cmbavUsername.ItemCount > 0 )
         {
         }
         /* End function init_web_controls */
      }

      protected void StartGridControl22( )
      {
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"PasswordgridContainer"+"DivS\" data-gxgridid=\"22\">") ;
            sStyleString = "";
            if ( subPasswordgrid_Visible == 0 )
            {
               sStyleString += "display:none;";
            }
            GxWebStd.gx_table_start( context, subPasswordgrid_Internalname, subPasswordgrid_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subPasswordgrid_Backcolorstyle == 0 )
            {
               subPasswordgrid_Titlebackstyle = 0;
               if ( StringUtil.Len( subPasswordgrid_Class) > 0 )
               {
                  subPasswordgrid_Linesclass = subPasswordgrid_Class+"Title";
               }
            }
            else
            {
               subPasswordgrid_Titlebackstyle = 1;
               if ( subPasswordgrid_Backcolorstyle == 1 )
               {
                  subPasswordgrid_Titlebackcolor = subPasswordgrid_Allbackcolor;
                  if ( StringUtil.Len( subPasswordgrid_Class) > 0 )
                  {
                     subPasswordgrid_Linesclass = subPasswordgrid_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subPasswordgrid_Class) > 0 )
                  {
                     subPasswordgrid_Linesclass = subPasswordgrid_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "Password Id") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Description") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "login") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavCopylogin_gximage, "")==0) ? "" : "GX_Image_"+edtavCopylogin_gximage+"_Class")+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Password") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavCopypassword_gximage, "")==0) ? "" : "GX_Image_"+edtavCopypassword_gximage+"_Class")+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavCopyauthenticator_gximage, "")==0) ? "" : "GX_Image_"+edtavCopyauthenticator_gximage+"_Class")+"\" "+" style=\""+((edtavCopyauthenticator_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "PIN") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavViewnote_gximage, "")==0) ? "" : "GX_Image_"+edtavViewnote_gximage+"_Class")+"\" "+" style=\""+((edtavViewnote_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "Note") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavEditimage_gximage, "")==0) ? "" : "GX_Image_"+edtavEditimage_gximage+"_Class")+"\" "+" style=\""+((edtavEditimage_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavDeleteimage_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteimage_gximage+"_Class")+"\" "+" style=\""+((edtavDeleteimage_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavWwtags_gximage, "")==0) ? "" : "GX_Image_"+edtavWwtags_gximage+"_Class")+"\" "+" style=\""+((edtavWwtags_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavWwconctacts_gximage, "")==0) ? "" : "GX_Image_"+edtavWwconctacts_gximage+"_Class")+"\" "+" style=\""+((edtavWwconctacts_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            PasswordgridContainer.AddObjectProperty("GridName", "Passwordgrid");
         }
         else
         {
            PasswordgridContainer.AddObjectProperty("GridName", "Passwordgrid");
            PasswordgridContainer.AddObjectProperty("Header", subPasswordgrid_Header);
            PasswordgridContainer.AddObjectProperty("Class", "Grid");
            PasswordgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            PasswordgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            PasswordgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subPasswordgrid_Backcolorstyle), 1, 0, ".", "")));
            PasswordgridContainer.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(subPasswordgrid_Visible), 5, 0, ".", "")));
            PasswordgridContainer.AddObjectProperty("CmpContext", sPrefix);
            PasswordgridContainer.AddObjectProperty("InMasterPage", "false");
            PasswordgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            PasswordgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlpasswordid_Enabled), 5, 0, ".", "")));
            PasswordgridContainer.AddColumnProperties(PasswordgridColumn);
            PasswordgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            PasswordgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV9description)));
            PasswordgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDescription_Enabled), 5, 0, ".", "")));
            PasswordgridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavDescription_Link));
            PasswordgridColumn.AddObjectProperty("Linktarget", StringUtil.RTrim( edtavDescription_Linktarget));
            PasswordgridContainer.AddColumnProperties(PasswordgridColumn);
            PasswordgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            PasswordgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtllogin_Enabled), 5, 0, ".", "")));
            PasswordgridContainer.AddColumnProperties(PasswordgridColumn);
            PasswordgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            PasswordgridColumn.AddObjectProperty("Value", context.convertURL( AV6copyLogin));
            PasswordgridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavCopylogin_Link));
            PasswordgridContainer.AddColumnProperties(PasswordgridColumn);
            PasswordgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            PasswordgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV16pass)));
            PasswordgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavPass_Enabled), 5, 0, ".", "")));
            PasswordgridContainer.AddColumnProperties(PasswordgridColumn);
            PasswordgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            PasswordgridColumn.AddObjectProperty("Value", context.convertURL( AV7copyPassword));
            PasswordgridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavCopypassword_Link));
            PasswordgridContainer.AddColumnProperties(PasswordgridColumn);
            PasswordgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            PasswordgridColumn.AddObjectProperty("Value", context.convertURL( AV5copyAuthenticator));
            PasswordgridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavCopyauthenticator_Link));
            PasswordgridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCopyauthenticator_Visible), 5, 0, ".", "")));
            PasswordgridContainer.AddColumnProperties(PasswordgridColumn);
            PasswordgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            PasswordgridColumn.AddObjectProperty("Value", context.convertURL( AV43viewNote));
            PasswordgridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavViewnote_Link));
            PasswordgridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavViewnote_Visible), 5, 0, ".", "")));
            PasswordgridContainer.AddColumnProperties(PasswordgridColumn);
            PasswordgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            PasswordgridColumn.AddObjectProperty("Value", context.convertURL( AV10editImage));
            PasswordgridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavEditimage_Link));
            PasswordgridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavEditimage_Visible), 5, 0, ".", "")));
            PasswordgridContainer.AddColumnProperties(PasswordgridColumn);
            PasswordgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            PasswordgridColumn.AddObjectProperty("Value", context.convertURL( AV8deleteImage));
            PasswordgridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDeleteimage_Visible), 5, 0, ".", "")));
            PasswordgridContainer.AddColumnProperties(PasswordgridColumn);
            PasswordgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            PasswordgridColumn.AddObjectProperty("Value", context.convertURL( AV23wwTags));
            PasswordgridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavWwtags_Link));
            PasswordgridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavWwtags_Visible), 5, 0, ".", "")));
            PasswordgridContainer.AddColumnProperties(PasswordgridColumn);
            PasswordgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            PasswordgridColumn.AddObjectProperty("Value", context.convertURL( AV41wwConctacts));
            PasswordgridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavWwconctacts_Link));
            PasswordgridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavWwconctacts_Visible), 5, 0, ".", "")));
            PasswordgridContainer.AddColumnProperties(PasswordgridColumn);
            PasswordgridContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subPasswordgrid_Selectedindex), 4, 0, ".", "")));
            PasswordgridContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subPasswordgrid_Allowselection), 1, 0, ".", "")));
            PasswordgridContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subPasswordgrid_Selectioncolor), 9, 0, ".", "")));
            PasswordgridContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subPasswordgrid_Allowhovering), 1, 0, ".", "")));
            PasswordgridContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subPasswordgrid_Hoveringcolor), 9, 0, ".", "")));
            PasswordgridContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subPasswordgrid_Allowcollapsing), 1, 0, ".", "")));
            PasswordgridContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subPasswordgrid_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         cmbavTagname_Internalname = sPrefix+"vTAGNAME";
         cmbavUsername_Internalname = sPrefix+"vUSERNAME";
         bttAddanewpassword_Internalname = sPrefix+"ADDANEWPASSWORD";
         divTable1_Internalname = sPrefix+"TABLE1";
         Uc_clipboard_v11_Internalname = sPrefix+"UC_CLIPBOARD_V11";
         edtavCtlpasswordid_Internalname = sPrefix+"CTLPASSWORDID";
         edtavDescription_Internalname = sPrefix+"vDESCRIPTION";
         edtavCtllogin_Internalname = sPrefix+"CTLLOGIN";
         edtavCopylogin_Internalname = sPrefix+"vCOPYLOGIN";
         edtavPass_Internalname = sPrefix+"vPASS";
         edtavCopypassword_Internalname = sPrefix+"vCOPYPASSWORD";
         edtavCopyauthenticator_Internalname = sPrefix+"vCOPYAUTHENTICATOR";
         edtavViewnote_Internalname = sPrefix+"vVIEWNOTE";
         edtavEditimage_Internalname = sPrefix+"vEDITIMAGE";
         edtavDeleteimage_Internalname = sPrefix+"vDELETEIMAGE";
         edtavWwtags_Internalname = sPrefix+"vWWTAGS";
         edtavWwconctacts_Internalname = sPrefix+"vWWCONCTACTS";
         divMaintable_Internalname = sPrefix+"MAINTABLE";
         Form.Internalname = sPrefix+"FORM";
         subPasswordgrid_Internalname = sPrefix+"PASSWORDGRID";
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
         subPasswordgrid_Allowcollapsing = 0;
         subPasswordgrid_Allowselection = 0;
         edtavWwconctacts_Link = "";
         edtavWwtags_Link = "";
         edtavEditimage_Link = "";
         edtavViewnote_Link = "";
         edtavCopyauthenticator_Link = "";
         edtavCopypassword_Link = "";
         edtavCopylogin_Link = "";
         subPasswordgrid_Header = "";
         edtavWwconctacts_Jsonclick = "";
         edtavWwtags_Jsonclick = "";
         edtavWwtags_gximage = "";
         edtavDeleteimage_Jsonclick = "";
         edtavDeleteimage_gximage = "";
         edtavEditimage_Jsonclick = "";
         edtavEditimage_gximage = "";
         edtavViewnote_Jsonclick = "";
         edtavViewnote_gximage = "";
         edtavViewnote_Visible = -1;
         edtavCopyauthenticator_Jsonclick = "";
         edtavCopyauthenticator_gximage = "";
         edtavCopyauthenticator_Visible = -1;
         edtavCopypassword_Jsonclick = "";
         edtavCopypassword_gximage = "";
         edtavPass_Jsonclick = "";
         edtavPass_Enabled = 1;
         edtavCopylogin_Jsonclick = "";
         edtavCopylogin_gximage = "";
         edtavCtllogin_Jsonclick = "";
         edtavCtllogin_Enabled = 0;
         edtavDescription_Jsonclick = "";
         edtavDescription_Linktarget = "";
         edtavDescription_Link = "";
         edtavDescription_Enabled = 1;
         edtavCtlpasswordid_Jsonclick = "";
         edtavCtlpasswordid_Enabled = 0;
         subPasswordgrid_Class = "Grid";
         subPasswordgrid_Backcolorstyle = 0;
         edtavEditimage_Visible = -1;
         edtavDeleteimage_Visible = -1;
         edtavWwtags_Visible = -1;
         edtavWwconctacts_Visible = -1;
         edtavWwconctacts_gximage = "";
         WebComp_Editpassword_Visible = 1;
         AssignProp(sPrefix, false, sPrefix+"gxHTMLWrpW0037"+"", "Visible", StringUtil.LTrimStr( (decimal)(WebComp_Editpassword_Visible), 5, 0), true);
         subPasswordgrid_Visible = 1;
         bttAddanewpassword_Visible = 1;
         cmbavUsername_Jsonclick = "";
         cmbavUsername.Enabled = 1;
         cmbavUsername.Visible = 1;
         cmbavTagname_Jsonclick = "";
         cmbavTagname.Enabled = 1;
         cmbavTagname.Visible = 1;
         edtavCtllogin_Enabled = -1;
         edtavCtlpasswordid_Enabled = -1;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"PASSWORDGRID_nEOF","type":"int"},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV31groupId","fld":"vGROUPID","type":"guid"},{"av":"AV29group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"cmbavUsername"},{"av":"AV40userName","fld":"vUSERNAME","type":"guid"},{"av":"AV17Password_tags","fld":"vPASSWORD_TAGS","hsh":true,"type":""}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV29group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV41wwConctacts","fld":"vWWCONCTACTS","type":"bits"},{"ctrl":"ADDANEWPASSWORD","prop":"Visible"},{"av":"cmbavUsername"},{"av":"edtavWwconctacts_Visible","ctrl":"vWWCONCTACTS","prop":"Visible"},{"av":"edtavWwtags_Visible","ctrl":"vWWTAGS","prop":"Visible"},{"av":"edtavDeleteimage_Visible","ctrl":"vDELETEIMAGE","prop":"Visible"},{"av":"edtavEditimage_Visible","ctrl":"vEDITIMAGE","prop":"Visible"},{"ctrl":"EDITPASSWORD","prop":"Visible"},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV40userName","fld":"vUSERNAME","type":"guid"},{"av":"AV19Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"AV17Password_tags","fld":"vPASSWORD_TAGS","hsh":true,"type":""},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"}]}""");
         setEventMetadata("'ADD A NEW PASSWORD'","""{"handler":"E112H2","iparms":[{"av":"AV31groupId","fld":"vGROUPID","type":"guid"},{"av":"AV29group_sdt","fld":"vGROUP_SDT","type":""}]""");
         setEventMetadata("'ADD A NEW PASSWORD'",""","oparms":[{"av":"cmbavTagname"},{"ctrl":"ADDANEWPASSWORD","prop":"Visible"},{"av":"subPasswordgrid_Visible","ctrl":"PASSWORDGRID","prop":"Visible"},{"ctrl":"EDITPASSWORD","prop":"Visible"},{"ctrl":"EDITPASSWORD"}]}""");
         setEventMetadata("'DELETE PASSWORD'","""{"handler":"E172H2","iparms":[{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED","""{"handler":"E122H2","iparms":[{"av":"AV21UserResponse","fld":"vUSERRESPONSE","type":"boolean"},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"},{"av":"AV19Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"PASSWORDGRID_nEOF","type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV31groupId","fld":"vGROUPID","type":"guid"},{"av":"AV29group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"cmbavUsername"},{"av":"AV40userName","fld":"vUSERNAME","type":"guid"},{"av":"AV17Password_tags","fld":"vPASSWORD_TAGS","hsh":true,"type":""}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED",""","oparms":[{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"},{"av":"AV19Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"AV29group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV11error","fld":"vERROR","type":"char"}]}""");
         setEventMetadata("'EDIT PASSWORD'","""{"handler":"E182H2","iparms":[{"av":"AV31groupId","fld":"vGROUPID","type":"guid"},{"av":"AV29group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"}]""");
         setEventMetadata("'EDIT PASSWORD'",""","oparms":[{"av":"cmbavTagname"},{"av":"cmbavUsername"},{"ctrl":"ADDANEWPASSWORD","prop":"Visible"},{"av":"subPasswordgrid_Visible","ctrl":"PASSWORDGRID","prop":"Visible"},{"ctrl":"EDITPASSWORD","prop":"Visible"},{"ctrl":"EDITPASSWORD"}]}""");
         setEventMetadata("GLOBALEVENTS.DONEWITHPASSWORD","""{"handler":"E132H2","iparms":[{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"PASSWORDGRID_nEOF","type":"int"},{"av":"AV31groupId","fld":"vGROUPID","type":"guid"},{"av":"AV17Password_tags","fld":"vPASSWORD_TAGS","hsh":true,"type":""},{"av":"AV29group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"cmbavUsername"},{"av":"AV40userName","fld":"vUSERNAME","type":"guid"},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV44isNewPassword","fld":"vISNEWPASSWORD","type":"boolean"},{"av":"AV19Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""}]""");
         setEventMetadata("GLOBALEVENTS.DONEWITHPASSWORD",""","oparms":[{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"AV19Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"cmbavUsername"},{"av":"cmbavTagname"},{"ctrl":"ADDANEWPASSWORD","prop":"Visible"},{"av":"subPasswordgrid_Visible","ctrl":"PASSWORDGRID","prop":"Visible"},{"ctrl":"EDITPASSWORD","prop":"Visible"},{"av":"AV29group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"AV41wwConctacts","fld":"vWWCONCTACTS","type":"bits"},{"av":"edtavWwconctacts_Visible","ctrl":"vWWCONCTACTS","prop":"Visible"},{"av":"edtavWwtags_Visible","ctrl":"vWWTAGS","prop":"Visible"},{"av":"edtavDeleteimage_Visible","ctrl":"vDELETEIMAGE","prop":"Visible"},{"av":"edtavEditimage_Visible","ctrl":"vEDITIMAGE","prop":"Visible"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV40userName","fld":"vUSERNAME","type":"guid"},{"av":"AV17Password_tags","fld":"vPASSWORD_TAGS","hsh":true,"type":""},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"}]}""");
         setEventMetadata("VTAGNAME.CONTROLVALUECHANGED","""{"handler":"E142H2","iparms":[{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"PASSWORDGRID_nEOF","type":"int"},{"av":"AV31groupId","fld":"vGROUPID","type":"guid"},{"av":"AV17Password_tags","fld":"vPASSWORD_TAGS","hsh":true,"type":""},{"av":"AV29group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"cmbavUsername"},{"av":"AV40userName","fld":"vUSERNAME","type":"guid"},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("VTAGNAME.CONTROLVALUECHANGED",""","oparms":[{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"},{"av":"AV29group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV41wwConctacts","fld":"vWWCONCTACTS","type":"bits"},{"ctrl":"ADDANEWPASSWORD","prop":"Visible"},{"av":"cmbavUsername"},{"av":"edtavWwconctacts_Visible","ctrl":"vWWCONCTACTS","prop":"Visible"},{"av":"edtavWwtags_Visible","ctrl":"vWWTAGS","prop":"Visible"},{"av":"edtavDeleteimage_Visible","ctrl":"vDELETEIMAGE","prop":"Visible"},{"av":"edtavEditimage_Visible","ctrl":"vEDITIMAGE","prop":"Visible"},{"ctrl":"EDITPASSWORD","prop":"Visible"},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV40userName","fld":"vUSERNAME","type":"guid"},{"av":"AV19Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"AV17Password_tags","fld":"vPASSWORD_TAGS","hsh":true,"type":""},{"av":"AV11error","fld":"vERROR","type":"char"}]}""");
         setEventMetadata("VUSERNAME.CONTROLVALUECHANGED","""{"handler":"E152H2","iparms":[{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"PASSWORDGRID_nEOF","type":"int"},{"av":"AV31groupId","fld":"vGROUPID","type":"guid"},{"av":"AV17Password_tags","fld":"vPASSWORD_TAGS","hsh":true,"type":""},{"av":"AV29group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"cmbavUsername"},{"av":"AV40userName","fld":"vUSERNAME","type":"guid"},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("VUSERNAME.CONTROLVALUECHANGED",""","oparms":[{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"},{"av":"AV29group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV41wwConctacts","fld":"vWWCONCTACTS","type":"bits"},{"ctrl":"ADDANEWPASSWORD","prop":"Visible"},{"av":"cmbavUsername"},{"av":"edtavWwconctacts_Visible","ctrl":"vWWCONCTACTS","prop":"Visible"},{"av":"edtavWwtags_Visible","ctrl":"vWWTAGS","prop":"Visible"},{"av":"edtavDeleteimage_Visible","ctrl":"vDELETEIMAGE","prop":"Visible"},{"av":"edtavEditimage_Visible","ctrl":"vEDITIMAGE","prop":"Visible"},{"ctrl":"EDITPASSWORD","prop":"Visible"},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV40userName","fld":"vUSERNAME","type":"guid"},{"av":"AV19Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"AV17Password_tags","fld":"vPASSWORD_TAGS","hsh":true,"type":""},{"av":"AV11error","fld":"vERROR","type":"char"}]}""");
         setEventMetadata("'VIEW NOTE'","""{"handler":"E192H2","iparms":[{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"}]}""");
         setEventMetadata("PASSWORDGRID.LOAD","""{"handler":"E202H2","iparms":[{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"}]""");
         setEventMetadata("PASSWORDGRID.LOAD",""","oparms":[{"av":"AV8deleteImage","fld":"vDELETEIMAGE","type":"bits"},{"av":"AV10editImage","fld":"vEDITIMAGE","type":"bits"},{"av":"AV6copyLogin","fld":"vCOPYLOGIN","type":"bits"},{"av":"AV7copyPassword","fld":"vCOPYPASSWORD","type":"bits"},{"av":"AV5copyAuthenticator","fld":"vCOPYAUTHENTICATOR","type":"bits"},{"av":"AV23wwTags","fld":"vWWTAGS","type":"bits"},{"av":"AV43viewNote","fld":"vVIEWNOTE","type":"bits"},{"av":"edtavViewnote_Visible","ctrl":"vVIEWNOTE","prop":"Visible"},{"av":"edtavCopyauthenticator_Visible","ctrl":"vCOPYAUTHENTICATOR","prop":"Visible"},{"av":"AV16pass","fld":"vPASS","type":"char"},{"av":"AV9description","fld":"vDESCRIPTION","type":"char"},{"av":"edtavDescription_Link","ctrl":"vDESCRIPTION","prop":"Link"},{"av":"edtavDescription_Linktarget","ctrl":"vDESCRIPTION","prop":"Linktarget"}]}""");
         setEventMetadata("'COPY LOGIN'","""{"handler":"E212H2","iparms":[{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"}]}""");
         setEventMetadata("'COPY PASSWORD'","""{"handler":"E222H2","iparms":[{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"}]}""");
         setEventMetadata("'COPY AUTHENTICATOR'","""{"handler":"E232H2","iparms":[{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"}]""");
         setEventMetadata("'COPY AUTHENTICATOR'",""","oparms":[{"av":"AV11error","fld":"vERROR","type":"char"}]}""");
         setEventMetadata("'WWTAGS'","""{"handler":"E242H2","iparms":[{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"PASSWORDGRID_nEOF","type":"int"},{"av":"AV31groupId","fld":"vGROUPID","type":"guid"},{"av":"AV17Password_tags","fld":"vPASSWORD_TAGS","hsh":true,"type":""},{"av":"AV29group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"cmbavUsername"},{"av":"AV40userName","fld":"vUSERNAME","type":"guid"},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'WWTAGS'",""","oparms":[{"av":"AV29group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV41wwConctacts","fld":"vWWCONCTACTS","type":"bits"},{"ctrl":"ADDANEWPASSWORD","prop":"Visible"},{"av":"cmbavUsername"},{"av":"edtavWwconctacts_Visible","ctrl":"vWWCONCTACTS","prop":"Visible"},{"av":"edtavWwtags_Visible","ctrl":"vWWTAGS","prop":"Visible"},{"av":"edtavDeleteimage_Visible","ctrl":"vDELETEIMAGE","prop":"Visible"},{"av":"edtavEditimage_Visible","ctrl":"vEDITIMAGE","prop":"Visible"},{"ctrl":"EDITPASSWORD","prop":"Visible"},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV40userName","fld":"vUSERNAME","type":"guid"},{"av":"AV19Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"AV17Password_tags","fld":"vPASSWORD_TAGS","hsh":true,"type":""},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"}]}""");
         setEventMetadata("'WWCONTACTS'","""{"handler":"E252H2","iparms":[{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"PASSWORDGRID_nEOF","type":"int"},{"av":"AV31groupId","fld":"vGROUPID","type":"guid"},{"av":"AV17Password_tags","fld":"vPASSWORD_TAGS","hsh":true,"type":""},{"av":"AV29group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"cmbavUsername"},{"av":"AV40userName","fld":"vUSERNAME","type":"guid"},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'WWCONTACTS'",""","oparms":[{"av":"AV29group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV41wwConctacts","fld":"vWWCONCTACTS","type":"bits"},{"ctrl":"ADDANEWPASSWORD","prop":"Visible"},{"av":"cmbavUsername"},{"av":"edtavWwconctacts_Visible","ctrl":"vWWCONCTACTS","prop":"Visible"},{"av":"edtavWwtags_Visible","ctrl":"vWWTAGS","prop":"Visible"},{"av":"edtavDeleteimage_Visible","ctrl":"vDELETEIMAGE","prop":"Visible"},{"av":"edtavEditimage_Visible","ctrl":"vEDITIMAGE","prop":"Visible"},{"ctrl":"EDITPASSWORD","prop":"Visible"},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV40userName","fld":"vUSERNAME","type":"guid"},{"av":"AV19Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"AV17Password_tags","fld":"vPASSWORD_TAGS","hsh":true,"type":""},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":22,"type":""},{"av":"nGXsfl_22_idx","ctrl":"GRID","prop":"GridCurrRow","grid":22},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_22","ctrl":"PASSWORDGRID","prop":"GridRC","grid":22,"type":"int"}]}""");
         setEventMetadata("VALIDV_TAGNAME","""{"handler":"Validv_Tagname","iparms":[]}""");
         setEventMetadata("VALIDV_USERNAME","""{"handler":"Validv_Username","iparms":[]}""");
         setEventMetadata("VALIDV_GXV2","""{"handler":"Validv_Gxv2","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Wwconctacts","iparms":[]}""");
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
         wcpOAV31groupId = Guid.Empty;
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         AV17Password_tags = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "distributedcryptography");
         AV29group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV11error = "";
         AV20tagName = Guid.Empty;
         AV18Passwords = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>( context, "Password", "distributedcryptography");
         AV40userName = Guid.Empty;
         AV26showPasswords = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>( context, "Password", "distributedcryptography");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV19Passwords_and_tags = new GeneXus.Programs.wallet.SdtPasswords_and_tags(context);
         GX_FocusControl = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttAddanewpassword_Jsonclick = "";
         ucUc_clipboard_v11 = new GXUserControl();
         PasswordgridContainer = new GXWebGrid( context);
         sStyleString = "";
         WebComp_Editpassword_Component = "";
         OldEditpassword = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV9description = "";
         AV6copyLogin = "";
         AV61Copylogin_GXI = "";
         AV16pass = "";
         AV7copyPassword = "";
         AV62Copypassword_GXI = "";
         AV5copyAuthenticator = "";
         AV63Copyauthenticator_GXI = "";
         AV43viewNote = "";
         AV65Viewnote_GXI = "";
         AV10editImage = "";
         AV60Editimage_GXI = "";
         AV8deleteImage = "";
         AV59Deleteimage_GXI = "";
         AV23wwTags = "";
         AV64Wwtags_GXI = "";
         AV41wwConctacts = "";
         AV48Wwconctacts_GXI = "";
         AV22websession = context.GetSession();
         GXt_SdtGroup_SDT1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV24oneTag = new GeneXus.Programs.wallet.SdtPassword_tag(context);
         AV32oneContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV15onePassword = new GeneXus.Programs.wallet.SdtPassword(context);
         AV12findPassword = new GeneXus.Programs.wallet.SdtPassword(context);
         AV38showPasswordsTags = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>( context, "Password", "distributedcryptography");
         AV37showPasswordsNames = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>( context, "Password", "distributedcryptography");
         AV35passwordContactId = Guid.Empty;
         AV34onePasswordTags = new GeneXus.Programs.wallet.SdtPassword(context);
         AV33onePasswordNames = new GeneXus.Programs.wallet.SdtPassword(context);
         PasswordgridRow = new GXWebRow();
         AV13generatedPIN = "";
         AV27clearText = "";
         AV28externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_SdtExternalUser3 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV30groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV42passwords_and_tags_Json = "";
         GXt_char2 = "";
         GXt_char4 = "";
         AV36returnGroupId = Guid.Empty;
         GXt_char5 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sCtrlAV31groupId = "";
         subPasswordgrid_Linesclass = "";
         ROClassString = "";
         sImgUrl = "";
         PasswordgridColumn = new GXWebColumn();
         WebComp_Editpassword = new GeneXus.Http.GXNullWebComponent();
         /* GeneXus formulas. */
         edtavCtlpasswordid_Enabled = 0;
         edtavDescription_Enabled = 0;
         edtavCtllogin_Enabled = 0;
         edtavPass_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nCmpId ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subPasswordgrid_Backcolorstyle ;
      private short PASSWORDGRID_nEOF ;
      private short nGXWrapped ;
      private short subPasswordgrid_Backstyle ;
      private short subPasswordgrid_Titlebackstyle ;
      private short subPasswordgrid_Allowselection ;
      private short subPasswordgrid_Allowhovering ;
      private short subPasswordgrid_Allowcollapsing ;
      private short subPasswordgrid_Collapsed ;
      private int nRC_GXsfl_22 ;
      private int nGXsfl_22_idx=1 ;
      private int edtavCtlpasswordid_Enabled ;
      private int edtavDescription_Enabled ;
      private int edtavCtllogin_Enabled ;
      private int edtavPass_Enabled ;
      private int bttAddanewpassword_Visible ;
      private int AV45GXV1 ;
      private int subPasswordgrid_Visible ;
      private int WebComp_Editpassword_Visible ;
      private int subPasswordgrid_Islastpage ;
      private int nGXsfl_22_fel_idx=1 ;
      private int edtavWwconctacts_Visible ;
      private int edtavWwtags_Visible ;
      private int edtavDeleteimage_Visible ;
      private int edtavEditimage_Visible ;
      private int AV49GXV4 ;
      private int AV50GXV5 ;
      private int AV51GXV6 ;
      private int nGXsfl_22_bak_idx=1 ;
      private int AV52GXV7 ;
      private int AV53GXV8 ;
      private int AV54GXV9 ;
      private int AV55GXV10 ;
      private int AV56GXV11 ;
      private int AV57GXV12 ;
      private int AV58GXV13 ;
      private int edtavViewnote_Visible ;
      private int edtavCopyauthenticator_Visible ;
      private int AV66GXV14 ;
      private int idxLst ;
      private int subPasswordgrid_Backcolor ;
      private int subPasswordgrid_Allbackcolor ;
      private int subPasswordgrid_Titlebackcolor ;
      private int subPasswordgrid_Selectedindex ;
      private int subPasswordgrid_Selectioncolor ;
      private int subPasswordgrid_Hoveringcolor ;
      private long PASSWORDGRID_nCurrentRecord ;
      private long PASSWORDGRID_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_22_idx="0001" ;
      private string AV11error ;
      private string edtavCtlpasswordid_Internalname ;
      private string edtavDescription_Internalname ;
      private string edtavCtllogin_Internalname ;
      private string edtavPass_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string divTable1_Internalname ;
      private string cmbavTagname_Internalname ;
      private string TempTags ;
      private string cmbavTagname_Jsonclick ;
      private string cmbavUsername_Internalname ;
      private string cmbavUsername_Jsonclick ;
      private string ClassString ;
      private string StyleString ;
      private string bttAddanewpassword_Internalname ;
      private string bttAddanewpassword_Jsonclick ;
      private string Uc_clipboard_v11_Internalname ;
      private string sStyleString ;
      private string subPasswordgrid_Internalname ;
      private string WebComp_Editpassword_Component ;
      private string OldEditpassword ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV9description ;
      private string edtavCopylogin_Internalname ;
      private string AV16pass ;
      private string edtavCopypassword_Internalname ;
      private string edtavCopyauthenticator_Internalname ;
      private string edtavViewnote_Internalname ;
      private string edtavEditimage_Internalname ;
      private string edtavDeleteimage_Internalname ;
      private string edtavWwtags_Internalname ;
      private string edtavWwconctacts_Internalname ;
      private string sGXsfl_22_fel_idx="0001" ;
      private string edtavWwconctacts_gximage ;
      private string edtavDeleteimage_gximage ;
      private string edtavEditimage_gximage ;
      private string edtavCopylogin_gximage ;
      private string edtavCopypassword_gximage ;
      private string edtavCopyauthenticator_gximage ;
      private string edtavWwtags_gximage ;
      private string edtavViewnote_gximage ;
      private string edtavDescription_Link ;
      private string edtavDescription_Linktarget ;
      private string AV13generatedPIN ;
      private string GXt_char2 ;
      private string GXt_char4 ;
      private string GXt_char5 ;
      private string sCtrlAV31groupId ;
      private string subPasswordgrid_Class ;
      private string subPasswordgrid_Linesclass ;
      private string ROClassString ;
      private string edtavCtlpasswordid_Jsonclick ;
      private string edtavDescription_Jsonclick ;
      private string edtavCtllogin_Jsonclick ;
      private string sImgUrl ;
      private string edtavCopylogin_Jsonclick ;
      private string edtavPass_Jsonclick ;
      private string edtavCopypassword_Jsonclick ;
      private string edtavCopyauthenticator_Jsonclick ;
      private string edtavViewnote_Jsonclick ;
      private string edtavEditimage_Jsonclick ;
      private string edtavDeleteimage_Jsonclick ;
      private string edtavWwtags_Jsonclick ;
      private string edtavWwconctacts_Jsonclick ;
      private string subPasswordgrid_Header ;
      private string edtavCopylogin_Link ;
      private string edtavCopypassword_Link ;
      private string edtavCopyauthenticator_Link ;
      private string edtavViewnote_Link ;
      private string edtavEditimage_Link ;
      private string edtavWwtags_Link ;
      private string edtavWwconctacts_Link ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_22_Refreshing=false ;
      private bool AV21UserResponse ;
      private bool AV44isNewPassword ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool gx_refresh_fired ;
      private bool returnInSub ;
      private bool bDynCreated_Editpassword ;
      private bool gx_BV22 ;
      private bool AV25tagFound ;
      private bool AV39userFound ;
      private bool AV6copyLogin_IsBlob ;
      private bool AV7copyPassword_IsBlob ;
      private bool AV5copyAuthenticator_IsBlob ;
      private bool AV43viewNote_IsBlob ;
      private bool AV10editImage_IsBlob ;
      private bool AV8deleteImage_IsBlob ;
      private bool AV23wwTags_IsBlob ;
      private bool AV41wwConctacts_IsBlob ;
      private string AV27clearText ;
      private string AV42passwords_and_tags_Json ;
      private string AV61Copylogin_GXI ;
      private string AV62Copypassword_GXI ;
      private string AV63Copyauthenticator_GXI ;
      private string AV65Viewnote_GXI ;
      private string AV60Editimage_GXI ;
      private string AV59Deleteimage_GXI ;
      private string AV64Wwtags_GXI ;
      private string AV48Wwconctacts_GXI ;
      private string AV6copyLogin ;
      private string AV7copyPassword ;
      private string AV5copyAuthenticator ;
      private string AV43viewNote ;
      private string AV10editImage ;
      private string AV8deleteImage ;
      private string AV23wwTags ;
      private string AV41wwConctacts ;
      private Guid AV31groupId ;
      private Guid wcpOAV31groupId ;
      private Guid AV20tagName ;
      private Guid AV40userName ;
      private Guid AV35passwordContactId ;
      private Guid AV36returnGroupId ;
      private IGxSession AV22websession ;
      private GXWebComponent WebComp_Editpassword ;
      private GXWebGrid PasswordgridContainer ;
      private GXWebRow PasswordgridRow ;
      private GXWebColumn PasswordgridColumn ;
      private GXUserControl ucUc_clipboard_v11 ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private Guid aP0_groupId ;
      private GXCombobox cmbavTagname ;
      private GXCombobox cmbavUsername ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> AV17Password_tags ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV29group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> AV18Passwords ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> AV26showPasswords ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV19Passwords_and_tags ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT GXt_SdtGroup_SDT1 ;
      private GeneXus.Programs.wallet.SdtPassword_tag AV24oneTag ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV32oneContact ;
      private GeneXus.Programs.wallet.SdtPassword AV15onePassword ;
      private GeneXus.Programs.wallet.SdtPassword AV12findPassword ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> AV38showPasswordsTags ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> AV37showPasswordsNames ;
      private GeneXus.Programs.wallet.SdtPassword AV34onePasswordTags ;
      private GeneXus.Programs.wallet.SdtPassword AV33onePasswordNames ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV28externalUser ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser3 ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV30groupContact ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
