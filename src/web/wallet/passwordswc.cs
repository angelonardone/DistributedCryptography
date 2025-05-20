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
         cmbavTagname = new GXCombobox();
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
         gxnrPasswordgrid_newrow( ) ;
         /* End function gxnrPasswordgrid_newrow_invoke */
      }

      protected void gxgrPasswordgrid_refresh_invoke( )
      {
         AV11error = GetPar( "error");
         cmbavTagname.FromJSonString( GetNextPar( ));
         AV20tagName = StringUtil.StrToGuid( GetPar( "tagName"));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV18Passwords);
         ajax_req_read_hidden_sdt(GetNextPar( ), AV26showPasswords);
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrPasswordgrid_refresh( AV11error, AV20tagName, AV18Passwords, AV26showPasswords, sPrefix) ;
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
               AssignProp(sPrefix, false, edtavCtlpasswordid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlpasswordid_Enabled), 5, 0), !bGXsfl_16_Refreshing);
               edtavDescription_Enabled = 0;
               AssignProp(sPrefix, false, edtavDescription_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDescription_Enabled), 5, 0), !bGXsfl_16_Refreshing);
               edtavCtllogin_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtllogin_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtllogin_Enabled), 5, 0), !bGXsfl_16_Refreshing);
               edtavPass_Enabled = 0;
               AssignProp(sPrefix, false, edtavPass_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavPass_Enabled), 5, 0), !bGXsfl_16_Refreshing);
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 123260), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 123260), false, true);
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
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.passwordswc") +"\">") ;
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Showpasswords", AV26showPasswords);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Showpasswords", AV26showPasswords);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_16", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_16), 8, 0, ".", "")));
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", cmbavTagname.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavTagname_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavTagname_Internalname, "Filter by Tag", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavTagname, cmbavTagname_Internalname, AV20tagName.ToString(), 1, cmbavTagname_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "guid", "", cmbavTagname.Visible, cmbavTagname.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,8);\"", "", true, 0, "HLP_Wallet/PasswordsWC.htm");
            cmbavTagname.CurrentValue = AV20tagName.ToString();
            AssignProp(sPrefix, false, cmbavTagname_Internalname, "Values", (string)(cmbavTagname.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 10,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttAddanewpassword_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(16), 2, 0)+","+"null"+");", "Add a new password", bttAddanewpassword_Jsonclick, 5, "Add a new password", "", StyleString, ClassString, bttAddanewpassword_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'ADD A NEW PASSWORD\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/PasswordsWC.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
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
            StartGridControl16( ) ;
         }
         if ( wbEnd == 16 )
         {
            wbEnd = 0;
            nRC_GXsfl_16 = (int)(nGXsfl_16_idx-1);
            if ( PasswordgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV27GXV1 = nGXsfl_16_idx;
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
               GxWebStd.gx_hidden_field( context, sPrefix+"W0029"+"", StringUtil.RTrim( WebComp_Editpassword_Component));
               context.WriteHtmlText( "<div") ;
               GxWebStd.ClassAttribute( context, "gxwebcomponent");
               context.WriteHtmlText( " id=\""+sPrefix+"gxHTMLWrpW0029"+""+"\""+((WebComp_Editpassword_Visible==1) ? "" : " style=\"display:none;\"")) ;
               context.WriteHtmlText( ">") ;
               if ( bGXsfl_16_Refreshing )
               {
                  if ( StringUtil.Len( WebComp_Editpassword_Component) != 0 )
                  {
                     if ( StringUtil.StrCmp(StringUtil.Lower( OldEditpassword), StringUtil.Lower( WebComp_Editpassword_Component)) != 0 )
                     {
                        context.httpAjaxContext.ajax_rspStartCmp(sPrefix+"gxHTMLWrpW0029"+"");
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
         if ( wbEnd == 16 )
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
                  AV27GXV1 = nGXsfl_16_idx;
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
                  Form.Meta.addItem("generator", "GeneXus .NET 18_0_12-186073", 0) ;
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
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 17), "'DELETE PASSWORD'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 15), "'EDIT PASSWORD'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 17), "PASSWORDGRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 12), "'COPY LOGIN'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 15), "'COPY PASSWORD'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 20), "'COPY AUTHENTICATOR'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 8), "'WWTAGS'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 12), "'COPY LOGIN'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 15), "'COPY PASSWORD'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 20), "'COPY AUTHENTICATOR'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 15), "'EDIT PASSWORD'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 17), "'DELETE PASSWORD'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 8), "'WWTAGS'") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP2H0( ) ;
                              }
                              nGXsfl_16_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
                              SubsflControlProps_162( ) ;
                              AV27GXV1 = nGXsfl_16_idx;
                              if ( ( AV26showPasswords.Count >= AV27GXV1 ) && ( AV27GXV1 > 0 ) )
                              {
                                 AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV27GXV1));
                                 AV9description = cgiGet( edtavDescription_Internalname);
                                 AssignAttri(sPrefix, false, edtavDescription_Internalname, AV9description);
                                 AV6copyLogin = cgiGet( edtavCopylogin_Internalname);
                                 AssignProp(sPrefix, false, edtavCopylogin_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV6copyLogin)) ? AV37Copylogin_GXI : context.convertURL( context.PathToRelativeUrl( AV6copyLogin))), !bGXsfl_16_Refreshing);
                                 AssignProp(sPrefix, false, edtavCopylogin_Internalname, "SrcSet", context.GetImageSrcSet( AV6copyLogin), true);
                                 AV16pass = cgiGet( edtavPass_Internalname);
                                 AssignAttri(sPrefix, false, edtavPass_Internalname, AV16pass);
                                 AV7copyPassword = cgiGet( edtavCopypassword_Internalname);
                                 AssignProp(sPrefix, false, edtavCopypassword_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV7copyPassword)) ? AV38Copypassword_GXI : context.convertURL( context.PathToRelativeUrl( AV7copyPassword))), !bGXsfl_16_Refreshing);
                                 AssignProp(sPrefix, false, edtavCopypassword_Internalname, "SrcSet", context.GetImageSrcSet( AV7copyPassword), true);
                                 AV5copyAuthenticator = cgiGet( edtavCopyauthenticator_Internalname);
                                 AssignProp(sPrefix, false, edtavCopyauthenticator_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV5copyAuthenticator)) ? AV39Copyauthenticator_GXI : context.convertURL( context.PathToRelativeUrl( AV5copyAuthenticator))), !bGXsfl_16_Refreshing);
                                 AssignProp(sPrefix, false, edtavCopyauthenticator_Internalname, "SrcSet", context.GetImageSrcSet( AV5copyAuthenticator), true);
                                 AV10editImage = cgiGet( edtavEditimage_Internalname);
                                 AssignProp(sPrefix, false, edtavEditimage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV10editImage)) ? AV36Editimage_GXI : context.convertURL( context.PathToRelativeUrl( AV10editImage))), !bGXsfl_16_Refreshing);
                                 AssignProp(sPrefix, false, edtavEditimage_Internalname, "SrcSet", context.GetImageSrcSet( AV10editImage), true);
                                 AV8deleteImage = cgiGet( edtavDeleteimage_Internalname);
                                 AssignProp(sPrefix, false, edtavDeleteimage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV8deleteImage)) ? AV35Deleteimage_GXI : context.convertURL( context.PathToRelativeUrl( AV8deleteImage))), !bGXsfl_16_Refreshing);
                                 AssignProp(sPrefix, false, edtavDeleteimage_Internalname, "SrcSet", context.GetImageSrcSet( AV8deleteImage), true);
                                 AV23wwTags = cgiGet( edtavWwtags_Internalname);
                                 AssignProp(sPrefix, false, edtavWwtags_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV23wwTags)) ? AV40Wwtags_GXI : context.convertURL( context.PathToRelativeUrl( AV23wwTags))), !bGXsfl_16_Refreshing);
                                 AssignProp(sPrefix, false, edtavWwtags_Internalname, "SrcSet", context.GetImageSrcSet( AV23wwTags), true);
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
                                          E152H2 ();
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
                                          E162H2 ();
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
                                          E172H2 ();
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
                                          E182H2 ();
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
                                          E192H2 ();
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
                                          E202H2 ();
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
                                          E212H2 ();
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
                                          E222H2 ();
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
                        if ( nCmpId == 29 )
                        {
                           OldEditpassword = cgiGet( sPrefix+"W0029");
                           if ( ( StringUtil.Len( OldEditpassword) == 0 ) || ( StringUtil.StrCmp(OldEditpassword, WebComp_Editpassword_Component) != 0 ) )
                           {
                              WebComp_Editpassword = getWebComponent(GetType(), "GeneXus.Programs", OldEditpassword, new Object[] {context} );
                              WebComp_Editpassword.ComponentInit();
                              WebComp_Editpassword.Name = "OldEditpassword";
                              WebComp_Editpassword_Component = OldEditpassword;
                           }
                           if ( StringUtil.Len( WebComp_Editpassword_Component) != 0 )
                           {
                              WebComp_Editpassword.componentprocess(sPrefix+"W0029", "", sEvt);
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
         SubsflControlProps_162( ) ;
         while ( nGXsfl_16_idx <= nRC_GXsfl_16 )
         {
            sendrow_162( ) ;
            nGXsfl_16_idx = ((subPasswordgrid_Islastpage==1)&&(nGXsfl_16_idx+1>subPasswordgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_16_idx+1);
            sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
            SubsflControlProps_162( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( PasswordgridContainer)) ;
         /* End function gxnrPasswordgrid_newrow */
      }

      protected void gxgrPasswordgrid_refresh( string AV11error ,
                                               Guid AV20tagName ,
                                               GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> AV18Passwords ,
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
         wbStart = 16;
         /* Execute user event: Refresh */
         E152H2 ();
         nGXsfl_16_idx = 1;
         sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
         SubsflControlProps_162( ) ;
         bGXsfl_16_Refreshing = true;
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
            SubsflControlProps_162( ) ;
            /* Execute user event: Passwordgrid.Load */
            E182H2 ();
            wbEnd = 16;
            WB2H0( ) ;
         }
         bGXsfl_16_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes2H2( )
      {
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
            nRC_GXsfl_16 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_16"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_16 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_16"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_16_fel_idx = 0;
            while ( nGXsfl_16_fel_idx < nRC_GXsfl_16 )
            {
               nGXsfl_16_fel_idx = ((subPasswordgrid_Islastpage==1)&&(nGXsfl_16_fel_idx+1>subPasswordgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_16_fel_idx+1);
               sGXsfl_16_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_162( ) ;
               AV27GXV1 = nGXsfl_16_fel_idx;
               if ( ( AV26showPasswords.Count >= AV27GXV1 ) && ( AV27GXV1 > 0 ) )
               {
                  AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV27GXV1));
                  AV9description = cgiGet( edtavDescription_Internalname);
                  AV6copyLogin = cgiGet( edtavCopylogin_Internalname);
                  AV16pass = cgiGet( edtavPass_Internalname);
                  AV7copyPassword = cgiGet( edtavCopypassword_Internalname);
                  AV5copyAuthenticator = cgiGet( edtavCopyauthenticator_Internalname);
                  AV10editImage = cgiGet( edtavEditimage_Internalname);
                  AV8deleteImage = cgiGet( edtavDeleteimage_Internalname);
                  AV23wwTags = cgiGet( edtavWwtags_Internalname);
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
            cmbavTagname.Name = cmbavTagname_Internalname;
            cmbavTagname.CurrentValue = cgiGet( cmbavTagname_Internalname);
            AV20tagName = StringUtil.StrToGuid( cgiGet( cmbavTagname_Internalname));
            AssignAttri(sPrefix, false, "AV20tagName", AV20tagName.ToString());
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void E152H2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         WebComp_Editpassword_Visible = 0;
         AssignProp(sPrefix, false, sPrefix+"gxHTMLWrpW0029"+"", "Visible", StringUtil.LTrimStr( (decimal)(WebComp_Editpassword_Visible), 5, 0), true);
         AV19Passwords_and_tags.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "encpasswords.enc", out  AV11error), null);
         AV18Passwords = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>)(AV19Passwords_and_tags.gxTpr_Password.Clone());
         AV17Password_tags = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>)(AV19Passwords_and_tags.gxTpr_Password_tag.Clone());
         cmbavTagname.removeAllItems();
         cmbavTagname.addItem(StringUtil.StrToGuid( "").ToString(), "Select a Tag to add", 0);
         AV30GXV4 = 1;
         while ( AV30GXV4 <= AV17Password_tags.Count )
         {
            AV24oneTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV17Password_tags.Item(AV30GXV4));
            cmbavTagname.addItem(AV24oneTag.gxTpr_Tagid.ToString(), AV24oneTag.gxTpr_Name, 0);
            AV30GXV4 = (int)(AV30GXV4+1);
         }
         /* Execute user subroutine: 'LOAD PASSORD VALUES' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV19Passwords_and_tags", AV19Passwords_and_tags);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV18Passwords", AV18Passwords);
         cmbavTagname.CurrentValue = AV20tagName.ToString();
         AssignProp(sPrefix, false, cmbavTagname_Internalname, "Values", cmbavTagname.ToJavascriptSource(), true);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV26showPasswords", AV26showPasswords);
      }

      protected void E112H2( )
      {
         /* 'Add a new password' Routine */
         returnInSub = false;
         cmbavTagname.Visible = 0;
         AssignProp(sPrefix, false, cmbavTagname_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavTagname.Visible), 5, 0), true);
         bttAddanewpassword_Visible = 0;
         AssignProp(sPrefix, false, bttAddanewpassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttAddanewpassword_Visible), 5, 0), true);
         subPasswordgrid_Visible = 0;
         AssignProp(sPrefix, false, sPrefix+"PasswordgridContainerDiv", "Visible", StringUtil.LTrimStr( (decimal)(subPasswordgrid_Visible), 5, 0), true);
         WebComp_Editpassword_Visible = 1;
         AssignProp(sPrefix, false, sPrefix+"gxHTMLWrpW0029"+"", "Visible", StringUtil.LTrimStr( (decimal)(WebComp_Editpassword_Visible), 5, 0), true);
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
            WebComp_Editpassword.componentprepare(new Object[] {(string)sPrefix+"W0029",(string)""});
            WebComp_Editpassword.componentbind(new Object[] {});
         }
         if ( isFullAjaxMode( ) || isAjaxCallMode( ) && bDynCreated_Editpassword )
         {
            context.httpAjaxContext.ajax_rspStartCmp(sPrefix+"gxHTMLWrpW0029"+"");
            WebComp_Editpassword.componentdraw();
            context.httpAjaxContext.ajax_rspEndCmp();
         }
         /*  Sending Event outputs  */
      }

      protected void E162H2( )
      {
         AV27GXV1 = nGXsfl_16_idx;
         if ( ( AV27GXV1 > 0 ) && ( AV26showPasswords.Count >= AV27GXV1 ) )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV27GXV1));
         }
         /* 'Delete Password' Routine */
         returnInSub = false;
         this.executeExternalObjectMethod(sPrefix, false, "gx.extensions.web.dialogs", "showConfirm", new Object[] {"Are you sure you want to Delete "+StringUtil.Trim( ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).gxTpr_Description)+" Password?"}, false);
      }

      protected void E122H2( )
      {
         AV27GXV1 = nGXsfl_16_idx;
         if ( ( AV27GXV1 > 0 ) && ( AV26showPasswords.Count >= AV27GXV1 ) )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV27GXV1));
         }
         /* Extensions\Web\Dialog_Onconfirmclosed Routine */
         returnInSub = false;
         if ( AV21UserResponse )
         {
            AV31GXV5 = 1;
            while ( AV31GXV5 <= AV26showPasswords.Count )
            {
               AV12findPassword = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV31GXV5));
               if ( AV12findPassword.gxTpr_Passwordid == ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).gxTpr_Passwordid )
               {
                  AV26showPasswords.RemoveItem(AV26showPasswords.IndexOf(AV12findPassword));
                  gx_BV16 = true;
               }
               AV31GXV5 = (int)(AV31GXV5+1);
            }
            AV19Passwords_and_tags.gxTpr_Password = AV26showPasswords;
            GXt_char1 = AV11error;
            new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "encpasswords.enc",  AV19Passwords_and_tags.ToJSonString(false, true), out  GXt_char1) ;
            AV11error = GXt_char1;
            AssignAttri(sPrefix, false, "AV11error", AV11error);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV26showPasswords", AV26showPasswords);
         nGXsfl_16_bak_idx = nGXsfl_16_idx;
         gxgrPasswordgrid_refresh( AV11error, AV20tagName, AV18Passwords, AV26showPasswords, sPrefix) ;
         nGXsfl_16_idx = nGXsfl_16_bak_idx;
         sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
         SubsflControlProps_162( ) ;
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV19Passwords_and_tags", AV19Passwords_and_tags);
      }

      protected void E172H2( )
      {
         AV27GXV1 = nGXsfl_16_idx;
         if ( ( AV27GXV1 > 0 ) && ( AV26showPasswords.Count >= AV27GXV1 ) )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV27GXV1));
         }
         /* 'Edit Password' Routine */
         returnInSub = false;
         cmbavTagname.Visible = 0;
         AssignProp(sPrefix, false, cmbavTagname_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavTagname.Visible), 5, 0), true);
         bttAddanewpassword_Visible = 0;
         AssignProp(sPrefix, false, bttAddanewpassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttAddanewpassword_Visible), 5, 0), true);
         subPasswordgrid_Visible = 0;
         AssignProp(sPrefix, false, sPrefix+"PasswordgridContainerDiv", "Visible", StringUtil.LTrimStr( (decimal)(subPasswordgrid_Visible), 5, 0), true);
         WebComp_Editpassword_Visible = 1;
         AssignProp(sPrefix, false, sPrefix+"gxHTMLWrpW0029"+"", "Visible", StringUtil.LTrimStr( (decimal)(WebComp_Editpassword_Visible), 5, 0), true);
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
            WebComp_Editpassword.componentprepare(new Object[] {(string)sPrefix+"W0029",(string)""});
            WebComp_Editpassword.componentbind(new Object[] {});
         }
         if ( isFullAjaxMode( ) || isAjaxCallMode( ) && bDynCreated_Editpassword )
         {
            context.httpAjaxContext.ajax_rspStartCmp(sPrefix+"gxHTMLWrpW0029"+"");
            WebComp_Editpassword.componentdraw();
            context.httpAjaxContext.ajax_rspEndCmp();
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
            AV32GXV6 = 1;
            while ( AV32GXV6 <= AV18Passwords.Count )
            {
               AV12findPassword = ((GeneXus.Programs.wallet.SdtPassword)AV18Passwords.Item(AV32GXV6));
               if ( AV12findPassword.gxTpr_Passwordid == AV15onePassword.gxTpr_Passwordid )
               {
                  AV18Passwords.RemoveItem(AV18Passwords.IndexOf(AV12findPassword));
               }
               AV32GXV6 = (int)(AV32GXV6+1);
            }
            AV18Passwords.Add(AV15onePassword, 0);
            AV19Passwords_and_tags.gxTpr_Password = AV18Passwords;
            GXt_char1 = AV11error;
            new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "encpasswords.enc",  AV19Passwords_and_tags.ToJSonString(false, true), out  GXt_char1) ;
            AV11error = GXt_char1;
            AssignAttri(sPrefix, false, "AV11error", AV11error);
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11error)) )
         {
            GX_msglist.addItem(AV11error);
         }
         cmbavTagname.Visible = 1;
         AssignProp(sPrefix, false, cmbavTagname_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavTagname.Visible), 5, 0), true);
         bttAddanewpassword_Visible = 1;
         AssignProp(sPrefix, false, bttAddanewpassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttAddanewpassword_Visible), 5, 0), true);
         subPasswordgrid_Visible = 1;
         AssignProp(sPrefix, false, sPrefix+"PasswordgridContainerDiv", "Visible", StringUtil.LTrimStr( (decimal)(subPasswordgrid_Visible), 5, 0), true);
         WebComp_Editpassword_Visible = 0;
         AssignProp(sPrefix, false, sPrefix+"gxHTMLWrpW0029"+"", "Visible", StringUtil.LTrimStr( (decimal)(WebComp_Editpassword_Visible), 5, 0), true);
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV18Passwords", AV18Passwords);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV19Passwords_and_tags", AV19Passwords_and_tags);
         cmbavTagname.CurrentValue = AV20tagName.ToString();
         AssignProp(sPrefix, false, cmbavTagname_Internalname, "Values", cmbavTagname.ToJavascriptSource(), true);
         if ( gx_BV16 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV26showPasswords", AV26showPasswords);
            nGXsfl_16_bak_idx = nGXsfl_16_idx;
            gxgrPasswordgrid_refresh( AV11error, AV20tagName, AV18Passwords, AV26showPasswords, sPrefix) ;
            nGXsfl_16_idx = nGXsfl_16_bak_idx;
            sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
            SubsflControlProps_162( ) ;
         }
      }

      protected void S112( )
      {
         /* 'LOAD PASSORD VALUES' Routine */
         returnInSub = false;
         AV26showPasswords.Clear();
         gx_BV16 = true;
         if ( (Guid.Empty==AV20tagName) )
         {
            AV26showPasswords = (GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>)(AV18Passwords.Clone());
            gx_BV16 = true;
         }
         else
         {
            AV33GXV7 = 1;
            while ( AV33GXV7 <= AV18Passwords.Count )
            {
               AV15onePassword = ((GeneXus.Programs.wallet.SdtPassword)AV18Passwords.Item(AV33GXV7));
               AV25tagFound = false;
               AV34GXV8 = 1;
               while ( AV34GXV8 <= AV15onePassword.gxTpr_Password_tag.Count )
               {
                  AV24oneTag = ((GeneXus.Programs.wallet.SdtPassword_tag)AV15onePassword.gxTpr_Password_tag.Item(AV34GXV8));
                  if ( AV24oneTag.gxTpr_Tagid == AV20tagName )
                  {
                     AV25tagFound = true;
                     if (true) break;
                  }
                  AV34GXV8 = (int)(AV34GXV8+1);
               }
               if ( AV25tagFound )
               {
                  AV26showPasswords.Add(AV15onePassword, 0);
                  gx_BV16 = true;
               }
               AV33GXV7 = (int)(AV33GXV7+1);
            }
         }
      }

      protected void E142H2( )
      {
         AV27GXV1 = nGXsfl_16_idx;
         if ( ( AV27GXV1 > 0 ) && ( AV26showPasswords.Count >= AV27GXV1 ) )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV27GXV1));
         }
         /* Tagname_Controlvaluechanged Routine */
         returnInSub = false;
         /* Execute user subroutine: 'LOAD PASSORD VALUES' */
         S112 ();
         if (returnInSub) return;
         gxgrPasswordgrid_refresh( AV11error, AV20tagName, AV18Passwords, AV26showPasswords, sPrefix) ;
         /*  Sending Event outputs  */
         if ( gx_BV16 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV26showPasswords", AV26showPasswords);
            nGXsfl_16_bak_idx = nGXsfl_16_idx;
            gxgrPasswordgrid_refresh( AV11error, AV20tagName, AV18Passwords, AV26showPasswords, sPrefix) ;
            nGXsfl_16_idx = nGXsfl_16_bak_idx;
            sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
            SubsflControlProps_162( ) ;
         }
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV19Passwords_and_tags", AV19Passwords_and_tags);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV18Passwords", AV18Passwords);
         cmbavTagname.CurrentValue = AV20tagName.ToString();
         AssignProp(sPrefix, false, cmbavTagname_Internalname, "Values", cmbavTagname.ToJavascriptSource(), true);
      }

      private void E182H2( )
      {
         /* Passwordgrid_Load Routine */
         returnInSub = false;
         AV27GXV1 = 1;
         while ( AV27GXV1 <= AV26showPasswords.Count )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV27GXV1));
            edtavDeleteimage_gximage = "GeneXusUnanimo_delete_light";
            AV8deleteImage = context.GetImagePath( "db0f63cd-dde8-4bf7-aca2-01cdf8d3c157", "", context.GetTheme( ));
            AssignAttri(sPrefix, false, edtavDeleteimage_Internalname, AV8deleteImage);
            AV35Deleteimage_GXI = GXDbFile.PathToUrl( context.GetImagePath( "db0f63cd-dde8-4bf7-aca2-01cdf8d3c157", "", context.GetTheme( )), context);
            edtavEditimage_gximage = "GeneXusUnanimo_edit_light";
            AV10editImage = context.GetImagePath( "f20f0a60-bf65-4ff0-a2d0-9392c759340b", "", context.GetTheme( ));
            AssignAttri(sPrefix, false, edtavEditimage_Internalname, AV10editImage);
            AV36Editimage_GXI = GXDbFile.PathToUrl( context.GetImagePath( "f20f0a60-bf65-4ff0-a2d0-9392c759340b", "", context.GetTheme( )), context);
            edtavCopylogin_gximage = "Wallet_copy";
            AV6copyLogin = context.GetImagePath( "d7ac116e-c7e5-4d68-ae44-0790c20de236", "", context.GetTheme( ));
            AssignAttri(sPrefix, false, edtavCopylogin_Internalname, AV6copyLogin);
            AV37Copylogin_GXI = GXDbFile.PathToUrl( context.GetImagePath( "d7ac116e-c7e5-4d68-ae44-0790c20de236", "", context.GetTheme( )), context);
            edtavCopypassword_gximage = "Wallet_copy";
            AV7copyPassword = context.GetImagePath( "d7ac116e-c7e5-4d68-ae44-0790c20de236", "", context.GetTheme( ));
            AssignAttri(sPrefix, false, edtavCopypassword_Internalname, AV7copyPassword);
            AV38Copypassword_GXI = GXDbFile.PathToUrl( context.GetImagePath( "d7ac116e-c7e5-4d68-ae44-0790c20de236", "", context.GetTheme( )), context);
            edtavCopyauthenticator_gximage = "Wallet_copy";
            AV5copyAuthenticator = context.GetImagePath( "d7ac116e-c7e5-4d68-ae44-0790c20de236", "", context.GetTheme( ));
            AssignAttri(sPrefix, false, edtavCopyauthenticator_Internalname, AV5copyAuthenticator);
            AV39Copyauthenticator_GXI = GXDbFile.PathToUrl( context.GetImagePath( "d7ac116e-c7e5-4d68-ae44-0790c20de236", "", context.GetTheme( )), context);
            edtavWwtags_gximage = "Wallet_tag";
            AV23wwTags = context.GetImagePath( "dd232ae9-7426-400c-a260-b9f5237f9ac5", "", context.GetTheme( ));
            AssignAttri(sPrefix, false, edtavWwtags_Internalname, AV23wwTags);
            AV40Wwtags_GXI = GXDbFile.PathToUrl( context.GetImagePath( "dd232ae9-7426-400c-a260-b9f5237f9ac5", "", context.GetTheme( )), context);
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
               wbStart = 16;
            }
            sendrow_162( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_16_Refreshing )
            {
               DoAjaxLoad(16, PasswordgridRow);
            }
            AV27GXV1 = (int)(AV27GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E192H2( )
      {
         AV27GXV1 = nGXsfl_16_idx;
         if ( ( AV27GXV1 > 0 ) && ( AV26showPasswords.Count >= AV27GXV1 ) )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV27GXV1));
         }
         /* 'Copy login' Routine */
         returnInSub = false;
         this.executeUsercontrolMethod(sPrefix, false, "UC_CLIPBOARD_V11Container", "setText", "", new Object[] {StringUtil.Trim( ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).gxTpr_Login)});
         this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Login Copied",(string)""}, true);
      }

      protected void E202H2( )
      {
         AV27GXV1 = nGXsfl_16_idx;
         if ( ( AV27GXV1 > 0 ) && ( AV26showPasswords.Count >= AV27GXV1 ) )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV27GXV1));
         }
         /* 'Copy password' Routine */
         returnInSub = false;
         this.executeUsercontrolMethod(sPrefix, false, "UC_CLIPBOARD_V11Container", "setText", "", new Object[] {StringUtil.Trim( ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).gxTpr_Password)});
         this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Password Copied",(string)""}, true);
      }

      protected void E212H2( )
      {
         AV27GXV1 = nGXsfl_16_idx;
         if ( ( AV27GXV1 > 0 ) && ( AV26showPasswords.Count >= AV27GXV1 ) )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV27GXV1));
         }
         /* 'Copy authenticator' Routine */
         returnInSub = false;
         GXt_char1 = AV11error;
         new GeneXus.Programs.googleauthenticator.getcurrentpin(context ).execute(  StringUtil.Trim( ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).gxTpr_Based32key), out  AV13generatedPIN, out  GXt_char1) ;
         AV11error = GXt_char1;
         AssignAttri(sPrefix, false, "AV11error", AV11error);
         this.executeUsercontrolMethod(sPrefix, false, "UC_CLIPBOARD_V11Container", "setText", "", new Object[] {StringUtil.Trim( AV13generatedPIN)});
         this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"PIN Copied",(string)""}, true);
         /*  Sending Event outputs  */
      }

      protected void E222H2( )
      {
         AV27GXV1 = nGXsfl_16_idx;
         if ( ( AV27GXV1 > 0 ) && ( AV26showPasswords.Count >= AV27GXV1 ) )
         {
            AV26showPasswords.CurrentItem = ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV27GXV1));
         }
         /* 'wwtags' Routine */
         returnInSub = false;
         AV22websession.Set("ONE_PASSWORD_TO_WWTAGS", ((GeneXus.Programs.wallet.SdtPassword)(AV26showPasswords.CurrentItem)).ToJSonString(false, true));
         context.PopUp(formatLink("wallet.wwtags") , new Object[] {});
         context.DoAjaxRefreshCmp(sPrefix);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV19Passwords_and_tags", AV19Passwords_and_tags);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV18Passwords", AV18Passwords);
         cmbavTagname.CurrentValue = AV20tagName.ToString();
         AssignProp(sPrefix, false, cmbavTagname_Internalname, "Values", cmbavTagname.ToJavascriptSource(), true);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV26showPasswords", AV26showPasswords);
         nGXsfl_16_bak_idx = nGXsfl_16_idx;
         gxgrPasswordgrid_refresh( AV11error, AV20tagName, AV18Passwords, AV26showPasswords, sPrefix) ;
         nGXsfl_16_idx = nGXsfl_16_bak_idx;
         sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
         SubsflControlProps_162( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202552012583811", true, true);
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
         context.AddJavascriptSource("wallet/passwordswc.js", "?202552012583811", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         context.AddJavascriptSource("UserControls/Wallet.UC_Clipboard_v1Render.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_162( )
      {
         edtavCtlpasswordid_Internalname = sPrefix+"CTLPASSWORDID_"+sGXsfl_16_idx;
         edtavDescription_Internalname = sPrefix+"vDESCRIPTION_"+sGXsfl_16_idx;
         edtavCtllogin_Internalname = sPrefix+"CTLLOGIN_"+sGXsfl_16_idx;
         edtavCopylogin_Internalname = sPrefix+"vCOPYLOGIN_"+sGXsfl_16_idx;
         edtavPass_Internalname = sPrefix+"vPASS_"+sGXsfl_16_idx;
         edtavCopypassword_Internalname = sPrefix+"vCOPYPASSWORD_"+sGXsfl_16_idx;
         edtavCopyauthenticator_Internalname = sPrefix+"vCOPYAUTHENTICATOR_"+sGXsfl_16_idx;
         edtavEditimage_Internalname = sPrefix+"vEDITIMAGE_"+sGXsfl_16_idx;
         edtavDeleteimage_Internalname = sPrefix+"vDELETEIMAGE_"+sGXsfl_16_idx;
         edtavWwtags_Internalname = sPrefix+"vWWTAGS_"+sGXsfl_16_idx;
      }

      protected void SubsflControlProps_fel_162( )
      {
         edtavCtlpasswordid_Internalname = sPrefix+"CTLPASSWORDID_"+sGXsfl_16_fel_idx;
         edtavDescription_Internalname = sPrefix+"vDESCRIPTION_"+sGXsfl_16_fel_idx;
         edtavCtllogin_Internalname = sPrefix+"CTLLOGIN_"+sGXsfl_16_fel_idx;
         edtavCopylogin_Internalname = sPrefix+"vCOPYLOGIN_"+sGXsfl_16_fel_idx;
         edtavPass_Internalname = sPrefix+"vPASS_"+sGXsfl_16_fel_idx;
         edtavCopypassword_Internalname = sPrefix+"vCOPYPASSWORD_"+sGXsfl_16_fel_idx;
         edtavCopyauthenticator_Internalname = sPrefix+"vCOPYAUTHENTICATOR_"+sGXsfl_16_fel_idx;
         edtavEditimage_Internalname = sPrefix+"vEDITIMAGE_"+sGXsfl_16_fel_idx;
         edtavDeleteimage_Internalname = sPrefix+"vDELETEIMAGE_"+sGXsfl_16_fel_idx;
         edtavWwtags_Internalname = sPrefix+"vWWTAGS_"+sGXsfl_16_fel_idx;
      }

      protected void sendrow_162( )
      {
         sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
         SubsflControlProps_162( ) ;
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
            if ( ((int)((nGXsfl_16_idx) % (2))) == 0 )
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
            context.WriteHtmlText( " gxrow=\""+sGXsfl_16_idx+"\">") ;
         }
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+"display:none;"+"\">") ;
         }
         /* Single line edit */
         ROClassString = "Attribute";
         PasswordgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlpasswordid_Internalname,((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV27GXV1)).gxTpr_Passwordid.ToString(),((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV27GXV1)).gxTpr_Passwordid.ToString(),""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlpasswordid_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtlpasswordid_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)36,(short)0,(short)0,(short)16,(short)0,(short)0,(short)0,(bool)true,(string)"",(string)"",(bool)false,(string)""});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 18,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',16)\"";
         ROClassString = "Attribute";
         PasswordgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDescription_Internalname,StringUtil.RTrim( AV9description),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,18);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)edtavDescription_Link,(string)edtavDescription_Linktarget,(string)"",(string)"",(string)edtavDescription_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavDescription_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)80,(short)0,(short)0,(short)16,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',16)\"";
         ROClassString = "Attribute";
         PasswordgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtllogin_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtPassword)AV26showPasswords.Item(AV27GXV1)).gxTpr_Login),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,19);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtllogin_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtllogin_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)80,(short)0,(short)0,(short)16,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 20,'" + sPrefix + "',false,'',16)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavCopylogin_gximage, "")==0) ? "" : "GX_Image_"+edtavCopylogin_gximage+"_Class");
         StyleString = "";
         AV6copyLogin_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV6copyLogin))&&String.IsNullOrEmpty(StringUtil.RTrim( AV37Copylogin_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV6copyLogin)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV6copyLogin)) ? AV37Copylogin_GXI : context.PathToRelativeUrl( AV6copyLogin));
         PasswordgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavCopylogin_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(short)-1,(short)1,(string)"",(string)"copy login",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavCopylogin_Jsonclick,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'COPY LOGIN\\'."+sGXsfl_16_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV6copyLogin_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl)});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 21,'" + sPrefix + "',false,'" + sGXsfl_16_idx + "',16)\"";
         ROClassString = "Attribute";
         PasswordgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavPass_Internalname,StringUtil.RTrim( AV16pass),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,21);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavPass_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavPass_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)10,(short)-1,(short)0,(short)16,(short)0,(short)0,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 22,'" + sPrefix + "',false,'',16)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavCopypassword_gximage, "")==0) ? "" : "GX_Image_"+edtavCopypassword_gximage+"_Class");
         StyleString = "";
         AV7copyPassword_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV7copyPassword))&&String.IsNullOrEmpty(StringUtil.RTrim( AV38Copypassword_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV7copyPassword)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV7copyPassword)) ? AV38Copypassword_GXI : context.PathToRelativeUrl( AV7copyPassword));
         PasswordgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavCopypassword_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(short)-1,(short)1,(string)"",(string)"copy password",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavCopypassword_Jsonclick,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'COPY PASSWORD\\'."+sGXsfl_16_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV7copyPassword_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl)});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+((edtavCopyauthenticator_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 23,'" + sPrefix + "',false,'',16)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavCopyauthenticator_gximage, "")==0) ? "" : "GX_Image_"+edtavCopyauthenticator_gximage+"_Class");
         StyleString = "";
         AV5copyAuthenticator_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV5copyAuthenticator))&&String.IsNullOrEmpty(StringUtil.RTrim( AV39Copyauthenticator_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV5copyAuthenticator)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV5copyAuthenticator)) ? AV39Copyauthenticator_GXI : context.PathToRelativeUrl( AV5copyAuthenticator));
         PasswordgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavCopyauthenticator_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(int)edtavCopyauthenticator_Visible,(short)1,(string)"",(string)"copy PIN",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavCopyauthenticator_Jsonclick,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'COPY AUTHENTICATOR\\'."+sGXsfl_16_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV5copyAuthenticator_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl)});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 24,'" + sPrefix + "',false,'',16)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavEditimage_gximage, "")==0) ? "" : "GX_Image_"+edtavEditimage_gximage+"_Class");
         StyleString = "";
         AV10editImage_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV10editImage))&&String.IsNullOrEmpty(StringUtil.RTrim( AV36Editimage_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV10editImage)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV10editImage)) ? AV36Editimage_GXI : context.PathToRelativeUrl( AV10editImage));
         PasswordgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavEditimage_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(short)-1,(short)1,(string)"",(string)"edit",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavEditimage_Jsonclick,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'EDIT PASSWORD\\'."+sGXsfl_16_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV10editImage_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl)});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 25,'" + sPrefix + "',false,'',16)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavDeleteimage_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteimage_gximage+"_Class");
         StyleString = "";
         AV8deleteImage_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV8deleteImage))&&String.IsNullOrEmpty(StringUtil.RTrim( AV35Deleteimage_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV8deleteImage)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV8deleteImage)) ? AV35Deleteimage_GXI : context.PathToRelativeUrl( AV8deleteImage));
         PasswordgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavDeleteimage_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(short)-1,(short)1,(string)"",(string)"delete",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavDeleteimage_Jsonclick,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'DELETE PASSWORD\\'."+sGXsfl_16_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV8deleteImage_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl)});
         /* Subfile cell */
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 26,'" + sPrefix + "',false,'',16)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavWwtags_gximage, "")==0) ? "" : "GX_Image_"+edtavWwtags_gximage+"_Class");
         StyleString = "";
         AV23wwTags_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV23wwTags))&&String.IsNullOrEmpty(StringUtil.RTrim( AV40Wwtags_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV23wwTags)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV23wwTags)) ? AV40Wwtags_GXI : context.PathToRelativeUrl( AV23wwTags));
         PasswordgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavWwtags_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(short)-1,(short)1,(string)"",(string)"work with Tags",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavWwtags_Jsonclick,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'WWTAGS\\'."+sGXsfl_16_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV23wwTags_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl)});
         send_integrity_lvl_hashes2H2( ) ;
         PasswordgridContainer.AddRow(PasswordgridRow);
         nGXsfl_16_idx = ((subPasswordgrid_Islastpage==1)&&(nGXsfl_16_idx+1>subPasswordgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_16_idx+1);
         sGXsfl_16_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_16_idx), 4, 0), 4, "0");
         SubsflControlProps_162( ) ;
         /* End function sendrow_162 */
      }

      protected void init_web_controls( )
      {
         cmbavTagname.Name = "vTAGNAME";
         cmbavTagname.WebTags = "";
         if ( cmbavTagname.ItemCount > 0 )
         {
         }
         /* End function init_web_controls */
      }

      protected void StartGridControl16( )
      {
         if ( PasswordgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"PasswordgridContainer"+"DivS\" data-gxgridid=\"16\">") ;
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
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavEditimage_gximage, "")==0) ? "" : "GX_Image_"+edtavEditimage_gximage+"_Class")+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavDeleteimage_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteimage_gximage+"_Class")+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavWwtags_gximage, "")==0) ? "" : "GX_Image_"+edtavWwtags_gximage+"_Class")+"\" "+" style=\""+""+""+"\" "+">") ;
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
            PasswordgridColumn.AddObjectProperty("Value", context.convertURL( AV10editImage));
            PasswordgridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavEditimage_Link));
            PasswordgridContainer.AddColumnProperties(PasswordgridColumn);
            PasswordgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            PasswordgridColumn.AddObjectProperty("Value", context.convertURL( AV8deleteImage));
            PasswordgridContainer.AddColumnProperties(PasswordgridColumn);
            PasswordgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            PasswordgridColumn.AddObjectProperty("Value", context.convertURL( AV23wwTags));
            PasswordgridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavWwtags_Link));
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
         bttAddanewpassword_Internalname = sPrefix+"ADDANEWPASSWORD";
         Uc_clipboard_v11_Internalname = sPrefix+"UC_CLIPBOARD_V11";
         edtavCtlpasswordid_Internalname = sPrefix+"CTLPASSWORDID";
         edtavDescription_Internalname = sPrefix+"vDESCRIPTION";
         edtavCtllogin_Internalname = sPrefix+"CTLLOGIN";
         edtavCopylogin_Internalname = sPrefix+"vCOPYLOGIN";
         edtavPass_Internalname = sPrefix+"vPASS";
         edtavCopypassword_Internalname = sPrefix+"vCOPYPASSWORD";
         edtavCopyauthenticator_Internalname = sPrefix+"vCOPYAUTHENTICATOR";
         edtavEditimage_Internalname = sPrefix+"vEDITIMAGE";
         edtavDeleteimage_Internalname = sPrefix+"vDELETEIMAGE";
         edtavWwtags_Internalname = sPrefix+"vWWTAGS";
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
         edtavWwtags_Link = "";
         edtavEditimage_Link = "";
         edtavCopyauthenticator_Link = "";
         edtavCopypassword_Link = "";
         edtavCopylogin_Link = "";
         subPasswordgrid_Header = "";
         edtavWwtags_Jsonclick = "";
         edtavWwtags_gximage = "";
         edtavDeleteimage_Jsonclick = "";
         edtavDeleteimage_gximage = "";
         edtavEditimage_Jsonclick = "";
         edtavEditimage_gximage = "";
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
         WebComp_Editpassword_Visible = 1;
         AssignProp(sPrefix, false, sPrefix+"gxHTMLWrpW0029"+"", "Visible", StringUtil.LTrimStr( (decimal)(WebComp_Editpassword_Visible), 5, 0), true);
         subPasswordgrid_Visible = 1;
         bttAddanewpassword_Visible = 1;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"PASSWORDGRID_nEOF","type":"int"},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"nRC_GXsfl_16","ctrl":"PASSWORDGRID","prop":"GridRC","grid":16,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""}]""");
         setEventMetadata("REFRESH",""","oparms":[{"ctrl":"EDITPASSWORD","prop":"Visible"},{"av":"AV19Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"PASSWORDGRID","prop":"GridRC","grid":16,"type":"int"}]}""");
         setEventMetadata("'ADD A NEW PASSWORD'","""{"handler":"E112H2","iparms":[]""");
         setEventMetadata("'ADD A NEW PASSWORD'",""","oparms":[{"av":"cmbavTagname"},{"ctrl":"ADDANEWPASSWORD","prop":"Visible"},{"av":"subPasswordgrid_Visible","ctrl":"PASSWORDGRID","prop":"Visible"},{"ctrl":"EDITPASSWORD","prop":"Visible"},{"ctrl":"EDITPASSWORD"}]}""");
         setEventMetadata("'DELETE PASSWORD'","""{"handler":"E162H2","iparms":[{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"PASSWORDGRID","prop":"GridRC","grid":16,"type":"int"}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED","""{"handler":"E122H2","iparms":[{"av":"AV21UserResponse","fld":"vUSERRESPONSE","type":"boolean"},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"PASSWORDGRID","prop":"GridRC","grid":16,"type":"int"},{"av":"AV19Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"PASSWORDGRID_nEOF","type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED",""","oparms":[{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"PASSWORDGRID","prop":"GridRC","grid":16,"type":"int"},{"av":"AV19Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"AV11error","fld":"vERROR","type":"char"}]}""");
         setEventMetadata("'EDIT PASSWORD'","""{"handler":"E172H2","iparms":[{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"PASSWORDGRID","prop":"GridRC","grid":16,"type":"int"}]""");
         setEventMetadata("'EDIT PASSWORD'",""","oparms":[{"av":"cmbavTagname"},{"ctrl":"ADDANEWPASSWORD","prop":"Visible"},{"av":"subPasswordgrid_Visible","ctrl":"PASSWORDGRID","prop":"Visible"},{"ctrl":"EDITPASSWORD","prop":"Visible"},{"ctrl":"EDITPASSWORD"}]}""");
         setEventMetadata("GLOBALEVENTS.DONEWITHPASSWORD","""{"handler":"E132H2","iparms":[{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"PASSWORDGRID_nEOF","type":"int"},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"nRC_GXsfl_16","ctrl":"PASSWORDGRID","prop":"GridRC","grid":16,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV19Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""}]""");
         setEventMetadata("GLOBALEVENTS.DONEWITHPASSWORD",""","oparms":[{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"AV19Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"cmbavTagname"},{"ctrl":"ADDANEWPASSWORD","prop":"Visible"},{"av":"subPasswordgrid_Visible","ctrl":"PASSWORDGRID","prop":"Visible"},{"ctrl":"EDITPASSWORD","prop":"Visible"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"PASSWORDGRID","prop":"GridRC","grid":16,"type":"int"}]}""");
         setEventMetadata("VTAGNAME.CONTROLVALUECHANGED","""{"handler":"E142H2","iparms":[{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"PASSWORDGRID_nEOF","type":"int"},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"nRC_GXsfl_16","ctrl":"PASSWORDGRID","prop":"GridRC","grid":16,"type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("VTAGNAME.CONTROLVALUECHANGED",""","oparms":[{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"PASSWORDGRID","prop":"GridRC","grid":16,"type":"int"},{"ctrl":"EDITPASSWORD","prop":"Visible"},{"av":"AV19Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"}]}""");
         setEventMetadata("PASSWORDGRID.LOAD","""{"handler":"E182H2","iparms":[{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"PASSWORDGRID","prop":"GridRC","grid":16,"type":"int"}]""");
         setEventMetadata("PASSWORDGRID.LOAD",""","oparms":[{"av":"AV8deleteImage","fld":"vDELETEIMAGE","type":"bits"},{"av":"AV10editImage","fld":"vEDITIMAGE","type":"bits"},{"av":"AV6copyLogin","fld":"vCOPYLOGIN","type":"bits"},{"av":"AV7copyPassword","fld":"vCOPYPASSWORD","type":"bits"},{"av":"AV5copyAuthenticator","fld":"vCOPYAUTHENTICATOR","type":"bits"},{"av":"AV23wwTags","fld":"vWWTAGS","type":"bits"},{"av":"edtavCopyauthenticator_Visible","ctrl":"vCOPYAUTHENTICATOR","prop":"Visible"},{"av":"AV16pass","fld":"vPASS","type":"char"},{"av":"AV9description","fld":"vDESCRIPTION","type":"char"},{"av":"edtavDescription_Link","ctrl":"vDESCRIPTION","prop":"Link"},{"av":"edtavDescription_Linktarget","ctrl":"vDESCRIPTION","prop":"Linktarget"}]}""");
         setEventMetadata("'COPY LOGIN'","""{"handler":"E192H2","iparms":[{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"PASSWORDGRID","prop":"GridRC","grid":16,"type":"int"}]}""");
         setEventMetadata("'COPY PASSWORD'","""{"handler":"E202H2","iparms":[{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"PASSWORDGRID","prop":"GridRC","grid":16,"type":"int"}]}""");
         setEventMetadata("'COPY AUTHENTICATOR'","""{"handler":"E212H2","iparms":[{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"PASSWORDGRID","prop":"GridRC","grid":16,"type":"int"}]""");
         setEventMetadata("'COPY AUTHENTICATOR'",""","oparms":[{"av":"AV11error","fld":"vERROR","type":"char"}]}""");
         setEventMetadata("'WWTAGS'","""{"handler":"E222H2","iparms":[{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"PASSWORDGRID_nEOF","type":"int"},{"av":"AV11error","fld":"vERROR","type":"char"},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"nRC_GXsfl_16","ctrl":"PASSWORDGRID","prop":"GridRC","grid":16,"type":"int"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'WWTAGS'",""","oparms":[{"ctrl":"EDITPASSWORD","prop":"Visible"},{"av":"AV19Passwords_and_tags","fld":"vPASSWORDS_AND_TAGS","type":""},{"av":"AV18Passwords","fld":"vPASSWORDS","type":""},{"av":"cmbavTagname"},{"av":"AV20tagName","fld":"vTAGNAME","type":"guid"},{"av":"AV26showPasswords","fld":"vSHOWPASSWORDS","grid":16,"type":""},{"av":"nGXsfl_16_idx","ctrl":"GRID","prop":"GridCurrRow","grid":16},{"av":"PASSWORDGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_16","ctrl":"PASSWORDGRID","prop":"GridRC","grid":16,"type":"int"}]}""");
         setEventMetadata("VALIDV_TAGNAME","""{"handler":"Validv_Tagname","iparms":[]}""");
         setEventMetadata("VALIDV_GXV2","""{"handler":"Validv_Gxv2","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Wwtags","iparms":[]}""");
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
         AV11error = "";
         AV20tagName = Guid.Empty;
         AV18Passwords = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword>( context, "Password", "distributedcryptography");
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
         AV37Copylogin_GXI = "";
         AV16pass = "";
         AV7copyPassword = "";
         AV38Copypassword_GXI = "";
         AV5copyAuthenticator = "";
         AV39Copyauthenticator_GXI = "";
         AV10editImage = "";
         AV36Editimage_GXI = "";
         AV8deleteImage = "";
         AV35Deleteimage_GXI = "";
         AV23wwTags = "";
         AV40Wwtags_GXI = "";
         AV17Password_tags = new GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag>( context, "Password_tag", "distributedcryptography");
         AV24oneTag = new GeneXus.Programs.wallet.SdtPassword_tag(context);
         AV15onePassword = new GeneXus.Programs.wallet.SdtPassword(context);
         AV22websession = context.GetSession();
         AV12findPassword = new GeneXus.Programs.wallet.SdtPassword(context);
         PasswordgridRow = new GXWebRow();
         GXt_char1 = "";
         AV13generatedPIN = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
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
      private int nRC_GXsfl_16 ;
      private int nGXsfl_16_idx=1 ;
      private int edtavCtlpasswordid_Enabled ;
      private int edtavDescription_Enabled ;
      private int edtavCtllogin_Enabled ;
      private int edtavPass_Enabled ;
      private int bttAddanewpassword_Visible ;
      private int AV27GXV1 ;
      private int subPasswordgrid_Visible ;
      private int WebComp_Editpassword_Visible ;
      private int subPasswordgrid_Islastpage ;
      private int nGXsfl_16_fel_idx=1 ;
      private int AV30GXV4 ;
      private int AV31GXV5 ;
      private int nGXsfl_16_bak_idx=1 ;
      private int AV32GXV6 ;
      private int AV33GXV7 ;
      private int AV34GXV8 ;
      private int edtavCopyauthenticator_Visible ;
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
      private string sGXsfl_16_idx="0001" ;
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
      private string cmbavTagname_Internalname ;
      private string TempTags ;
      private string cmbavTagname_Jsonclick ;
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
      private string edtavEditimage_Internalname ;
      private string edtavDeleteimage_Internalname ;
      private string edtavWwtags_Internalname ;
      private string sGXsfl_16_fel_idx="0001" ;
      private string edtavDeleteimage_gximage ;
      private string edtavEditimage_gximage ;
      private string edtavCopylogin_gximage ;
      private string edtavCopypassword_gximage ;
      private string edtavCopyauthenticator_gximage ;
      private string edtavWwtags_gximage ;
      private string edtavDescription_Link ;
      private string edtavDescription_Linktarget ;
      private string GXt_char1 ;
      private string AV13generatedPIN ;
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
      private string edtavEditimage_Jsonclick ;
      private string edtavDeleteimage_Jsonclick ;
      private string edtavWwtags_Jsonclick ;
      private string subPasswordgrid_Header ;
      private string edtavCopylogin_Link ;
      private string edtavCopypassword_Link ;
      private string edtavCopyauthenticator_Link ;
      private string edtavEditimage_Link ;
      private string edtavWwtags_Link ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_16_Refreshing=false ;
      private bool AV21UserResponse ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool gx_refresh_fired ;
      private bool returnInSub ;
      private bool bDynCreated_Editpassword ;
      private bool gx_BV16 ;
      private bool AV25tagFound ;
      private bool AV6copyLogin_IsBlob ;
      private bool AV7copyPassword_IsBlob ;
      private bool AV5copyAuthenticator_IsBlob ;
      private bool AV10editImage_IsBlob ;
      private bool AV8deleteImage_IsBlob ;
      private bool AV23wwTags_IsBlob ;
      private string AV37Copylogin_GXI ;
      private string AV38Copypassword_GXI ;
      private string AV39Copyauthenticator_GXI ;
      private string AV36Editimage_GXI ;
      private string AV35Deleteimage_GXI ;
      private string AV40Wwtags_GXI ;
      private string AV6copyLogin ;
      private string AV7copyPassword ;
      private string AV5copyAuthenticator ;
      private string AV10editImage ;
      private string AV8deleteImage ;
      private string AV23wwTags ;
      private Guid AV20tagName ;
      private GXWebComponent WebComp_Editpassword ;
      private GXWebGrid PasswordgridContainer ;
      private GXWebRow PasswordgridRow ;
      private GXWebColumn PasswordgridColumn ;
      private GXUserControl ucUc_clipboard_v11 ;
      private GXWebForm Form ;
      private IGxSession AV22websession ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavTagname ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> AV18Passwords ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword> AV26showPasswords ;
      private GeneXus.Programs.wallet.SdtPasswords_and_tags AV19Passwords_and_tags ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtPassword_tag> AV17Password_tags ;
      private GeneXus.Programs.wallet.SdtPassword_tag AV24oneTag ;
      private GeneXus.Programs.wallet.SdtPassword AV15onePassword ;
      private GeneXus.Programs.wallet.SdtPassword AV12findPassword ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
