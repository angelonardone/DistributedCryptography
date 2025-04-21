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
namespace GeneXus.Programs.wallet.registered {
   public class smartgroupwalletbackup : GXWebComponent
   {
      public smartgroupwalletbackup( )
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

      public smartgroupwalletbackup( IGxContext context )
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
         nRC_GXsfl_11 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_11"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_11_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_11_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_11_idx = GetPar( "sGXsfl_11_idx");
         sPrefix = GetPar( "sPrefix");
         AV19removeContact = GetPar( "removeContact");
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
         AV19removeContact = GetPar( "removeContact");
         ajax_req_read_hidden_sdt(GetNextPar( ), AV11group_sdt);
         ajax_req_read_hidden_sdt(GetNextPar( ), AV14groupContacts);
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridcontacts_refresh( AV19removeContact, AV11group_sdt, AV14groupContacts, sPrefix) ;
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
            PA1J2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavCtlcontactid1_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcontactid1_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcontactid1_Enabled), 5, 0), !bGXsfl_11_Refreshing);
               edtavCtlcontactprivatename_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcontactprivatename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcontactprivatename_Enabled), 5, 0), !bGXsfl_11_Refreshing);
               edtavCtlcontactusername_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcontactusername_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcontactusername_Enabled), 5, 0), !bGXsfl_11_Refreshing);
               edtavCtlcontactinvitationsent_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcontactinvitationsent_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcontactinvitationsent_Enabled), 5, 0), !bGXsfl_11_Refreshing);
               edtavCtlcontactinvitacionaccepted_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcontactinvitacionaccepted_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcontactinvitacionaccepted_Enabled), 5, 0), !bGXsfl_11_Refreshing);
               edtavRemovecontact_Enabled = 0;
               AssignProp(sPrefix, false, edtavRemovecontact_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavRemovecontact_Enabled), 5, 0), !bGXsfl_11_Refreshing);
               WS1J2( ) ;
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
            context.SendWebValue( "Smart Group Wallet Backup") ;
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
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("calendar-en.js", "?"+context.GetBuildNumber( 123260), false, true);
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
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.smartgroupwalletbackup") +"\">") ;
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Group_sdt", AV11group_sdt);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Group_sdt", AV11group_sdt);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Groupcontacts", AV14groupContacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Groupcontacts", AV14groupContacts);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_11", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_11), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"vTOTALUSERSHARES", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV22totalUserShares), 4, 0, ".", "")));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vHASCONTACTEMPTYSHARES", AV34hasContactEmptyShares);
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vGROUPCONTACTS", AV14groupContacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vGROUPCONTACTS", AV14groupContacts);
         }
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vSAVEANDRETURN", AV35saveAndReturn);
         GxWebStd.gx_hidden_field( context, sPrefix+"vPOPUPNAME", StringUtil.RTrim( AV18PopupName));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vGROUPCONTACTADD", AV32groupContactAdd);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vGROUPCONTACTADD", AV32groupContactAdd);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vERROR", StringUtil.RTrim( AV8error));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vMESSAGE_SIGNATURE", AV27message_signature);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vMESSAGE_SIGNATURE", AV27message_signature);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vSDT_MESSAGE", AV28sdt_message);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vSDT_MESSAGE", AV28sdt_message);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vGROUP_SDT", AV11group_sdt);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vGROUP_SDT", AV11group_sdt);
         }
      }

      protected void RenderHtmlCloseForm1J2( )
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
         return "Wallet.registered.SmartGroupWalletBackup" ;
      }

      public override string GetPgmdesc( )
      {
         return "Smart Group Wallet Backup" ;
      }

      protected void WB1J0( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.registered.smartgroupwalletbackup");
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
            GxWebStd.gx_label_element( context, edtavCtlminimumshares_Internalname, "Minimum amount of shares to recover the secret", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'" + sPrefix + "',false,'" + sGXsfl_11_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCtlminimumshares_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV11group_sdt.gxTpr_Minimumshares), 4, 0, ".", "")), StringUtil.LTrim( context.localUtil.Format( (decimal)(AV11group_sdt.gxTpr_Minimumshares), "ZZZ9")), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,8);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCtlminimumshares_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCtlminimumshares_Enabled, 1, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/registered/SmartGroupWalletBackup.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridcontactsContainer.SetWrapped(nGXWrapped);
            StartGridControl11( ) ;
         }
         if ( wbEnd == 11 )
         {
            wbEnd = 0;
            nRC_GXsfl_11 = (int)(nGXsfl_11_idx-1);
            if ( GridcontactsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV60GXV2 = nGXsfl_11_idx;
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
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 20,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttAddacontact_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(11), 2, 0)+","+"null"+");", "Add a contact", bttAddacontact_Jsonclick, 7, "Add a contact", "", StyleString, ClassString, bttAddacontact_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+"e111j1_client"+"'", TempTags, "", 2, "HLP_Wallet/registered/SmartGroupWalletBackup.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 23,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSave_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(11), 2, 0)+","+"null"+");", "Save", bttSave_Jsonclick, 5, "Save", "", StyleString, ClassString, bttSave_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'SAVE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SmartGroupWalletBackup.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 25,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCancel_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(11), 2, 0)+","+"null"+");", bttCancel_Caption, bttCancel_Jsonclick, 5, "Cancel", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'CANCEL EDIT\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SmartGroupWalletBackup.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 28,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSendinvitatiotogroupmembers_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(11), 2, 0)+","+"null"+");", "Send Invitation to Group members", bttSendinvitatiotogroupmembers_Jsonclick, 5, "Send Invitation to Group members", "", StyleString, ClassString, bttSendinvitatiotogroupmembers_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'SEND INVITATION TO GROUP MEMBERS\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SmartGroupWalletBackup.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 31,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttActivategroup_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(11), 2, 0)+","+"null"+");", "Activate Group", bttActivategroup_Jsonclick, 7, "Activate Group", "", StyleString, ClassString, bttActivategroup_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+"e121j1_client"+"'", TempTags, "", 2, "HLP_Wallet/registered/SmartGroupWalletBackup.htm");
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
               if ( GridcontactsContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV60GXV2 = nGXsfl_11_idx;
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

      protected void START1J2( )
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
            Form.Meta.addItem("description", "Smart Group Wallet Backup", 0) ;
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
               STRUP1J0( ) ;
            }
         }
      }

      protected void WS1J2( )
      {
         START1J2( ) ;
         EVT1J2( ) ;
      }

      protected void EVT1J2( )
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
                                 STRUP1J0( ) ;
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
                           else if ( StringUtil.StrCmp(sEvt, "'SAVE'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1J0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Save' */
                                    E131J2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CANCEL EDIT'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1J0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Cancel edit' */
                                    E141J2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1J0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E151J2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'SEND INVITATION TO GROUP MEMBERS'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1J0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Send Invitation to Group members' */
                                    E161J2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1J0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavCtlcontactid1_Internalname;
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
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 17), "GRIDCONTACTS.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 16), "'REMOVE CONTACT'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 16), "'REMOVE CONTACT'") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1J0( ) ;
                              }
                              nGXsfl_11_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
                              SubsflControlProps_112( ) ;
                              AV60GXV2 = nGXsfl_11_idx;
                              if ( ( AV14groupContacts.Count >= AV60GXV2 ) && ( AV60GXV2 > 0 ) )
                              {
                                 AV14groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV60GXV2));
                                 AV19removeContact = cgiGet( edtavRemovecontact_Internalname);
                                 AssignAttri(sPrefix, false, edtavRemovecontact_Internalname, AV19removeContact);
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlcontactid1_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Start */
                                          E171J2 ();
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
                                          GX_FocusControl = edtavCtlcontactid1_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Gridcontacts.Load */
                                          E181J2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'REMOVE CONTACT'") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlcontactid1_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: 'Remove contact' */
                                          E191J2 ();
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
                                       STRUP1J0( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavCtlcontactid1_Internalname;
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

      protected void WE1J2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm1J2( ) ;
            }
         }
      }

      protected void PA1J2( )
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
         SubsflControlProps_112( ) ;
         while ( nGXsfl_11_idx <= nRC_GXsfl_11 )
         {
            sendrow_112( ) ;
            nGXsfl_11_idx = ((subGridcontacts_Islastpage==1)&&(nGXsfl_11_idx+1>subGridcontacts_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_idx+1);
            sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
            SubsflControlProps_112( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridcontactsContainer)) ;
         /* End function gxnrGridcontacts_newrow */
      }

      protected void gxgrGridcontacts_refresh( string AV19removeContact ,
                                               GeneXus.Programs.wallet.registered.SdtGroup_SDT AV11group_sdt ,
                                               GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem> AV14groupContacts ,
                                               string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDCONTACTS_nCurrentRecord = 0;
         RF1J2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
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
         RF1J2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtlcontactid1_Enabled = 0;
         edtavCtlcontactprivatename_Enabled = 0;
         edtavCtlcontactusername_Enabled = 0;
         edtavCtlcontactinvitationsent_Enabled = 0;
         edtavCtlcontactinvitacionaccepted_Enabled = 0;
         edtavRemovecontact_Enabled = 0;
      }

      protected void RF1J2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridcontactsContainer.ClearRows();
         }
         wbStart = 11;
         nGXsfl_11_idx = 1;
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
         bGXsfl_11_Refreshing = true;
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
            SubsflControlProps_112( ) ;
            /* Execute user event: Gridcontacts.Load */
            E181J2 ();
            wbEnd = 11;
            WB1J0( ) ;
         }
         bGXsfl_11_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes1J2( )
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
         edtavCtlcontactid1_Enabled = 0;
         edtavCtlcontactprivatename_Enabled = 0;
         edtavCtlcontactusername_Enabled = 0;
         edtavCtlcontactinvitationsent_Enabled = 0;
         edtavCtlcontactinvitacionaccepted_Enabled = 0;
         edtavRemovecontact_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP1J0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E171J2 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vGROUP_SDT"), AV11group_sdt);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Group_sdt"), AV11group_sdt);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Groupcontacts"), AV14groupContacts);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vGROUPCONTACTS"), AV14groupContacts);
            /* Read saved values. */
            nRC_GXsfl_11 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_11"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_11 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_11"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_11_fel_idx = 0;
            while ( nGXsfl_11_fel_idx < nRC_GXsfl_11 )
            {
               nGXsfl_11_fel_idx = ((subGridcontacts_Islastpage==1)&&(nGXsfl_11_fel_idx+1>subGridcontacts_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_fel_idx+1);
               sGXsfl_11_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_112( ) ;
               AV60GXV2 = nGXsfl_11_fel_idx;
               if ( ( AV14groupContacts.Count >= AV60GXV2 ) && ( AV60GXV2 > 0 ) )
               {
                  AV14groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV60GXV2));
                  AV19removeContact = cgiGet( edtavRemovecontact_Internalname);
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
            if ( ( ( context.localUtil.CToN( cgiGet( edtavCtlminimumshares_Internalname), ".", ",") < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavCtlminimumshares_Internalname), ".", ",") > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CTLMINIMUMSHARES");
               GX_FocusControl = edtavCtlminimumshares_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV11group_sdt.gxTpr_Minimumshares = 0;
            }
            else
            {
               AV11group_sdt.gxTpr_Minimumshares = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavCtlminimumshares_Internalname), ".", ","), 18, MidpointRounding.ToEven));
            }
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
         E171J2 ();
         if (returnInSub) return;
      }

      protected void E171J2( )
      {
         /* Start Routine */
         returnInSub = false;
         AV6all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV8error), null);
         AV11group_sdt.FromJSonString(AV5websession.Get("Group_EDIT"), null);
         AV14groupContacts.Clear();
         gx_BV11 = true;
         AV19removeContact = "Remove contact";
         AssignAttri(sPrefix, false, edtavRemovecontact_Internalname, AV19removeContact);
         AV67GXV9 = 1;
         while ( AV67GXV9 <= AV11group_sdt.gxTpr_Contact.Count )
         {
            AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV11group_sdt.gxTpr_Contact.Item(AV67GXV9));
            AV14groupContacts.Add(AV13groupContact, 0);
            gx_BV11 = true;
            if ( ! (DateTime.MinValue==AV13groupContact.gxTpr_Contactinvitacionaccepted) )
            {
               AV33totalInvitationsAccepted = (short)(AV33totalInvitationsAccepted+1);
               AssignAttri(sPrefix, false, "AV33totalInvitationsAccepted", StringUtil.LTrimStr( (decimal)(AV33totalInvitationsAccepted), 4, 0));
            }
            AV67GXV9 = (int)(AV67GXV9+1);
         }
         if ( AV11group_sdt.gxTpr_Amigroupowner )
         {
            if ( AV11group_sdt.gxTpr_Isactive )
            {
               bttActivategroup_Visible = 0;
               AssignProp(sPrefix, false, bttActivategroup_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttActivategroup_Visible), 5, 0), true);
               bttSave_Visible = 0;
               AssignProp(sPrefix, false, bttSave_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSave_Visible), 5, 0), true);
               bttAddacontact_Visible = 0;
               AssignProp(sPrefix, false, bttAddacontact_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttAddacontact_Visible), 5, 0), true);
               bttCancel_Caption = "Close";
               AssignProp(sPrefix, false, bttCancel_Internalname, "Caption", bttCancel_Caption, true);
               edtavCtlminimumshares_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlminimumshares_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlminimumshares_Enabled), 5, 0), true);
               AV19removeContact = "";
               AssignAttri(sPrefix, false, edtavRemovecontact_Internalname, AV19removeContact);
               edtavCtlnumshares_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlnumshares_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlnumshares_Enabled), 5, 0), !bGXsfl_11_Refreshing);
               bttSendinvitatiotogroupmembers_Visible = 0;
               AssignProp(sPrefix, false, bttSendinvitatiotogroupmembers_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSendinvitatiotogroupmembers_Visible), 5, 0), true);
            }
            else
            {
               if ( ( AV33totalInvitationsAccepted == AV14groupContacts.Count ) && ( AV33totalInvitationsAccepted > 1 ) )
               {
                  bttActivategroup_Visible = 1;
                  AssignProp(sPrefix, false, bttActivategroup_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttActivategroup_Visible), 5, 0), true);
               }
               else
               {
                  bttActivategroup_Visible = 0;
                  AssignProp(sPrefix, false, bttActivategroup_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttActivategroup_Visible), 5, 0), true);
               }
            }
         }
         else
         {
            bttActivategroup_Visible = 0;
            AssignProp(sPrefix, false, bttActivategroup_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttActivategroup_Visible), 5, 0), true);
            bttSave_Visible = 0;
            AssignProp(sPrefix, false, bttSave_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSave_Visible), 5, 0), true);
            bttAddacontact_Visible = 0;
            AssignProp(sPrefix, false, bttAddacontact_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttAddacontact_Visible), 5, 0), true);
            bttCancel_Caption = "Close";
            AssignProp(sPrefix, false, bttCancel_Internalname, "Caption", bttCancel_Caption, true);
            edtavCtlminimumshares_Enabled = 0;
            AssignProp(sPrefix, false, edtavCtlminimumshares_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlminimumshares_Enabled), 5, 0), true);
            AV19removeContact = "";
            AssignAttri(sPrefix, false, edtavRemovecontact_Internalname, AV19removeContact);
            edtavCtlnumshares_Enabled = 0;
            AssignProp(sPrefix, false, edtavCtlnumshares_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlnumshares_Enabled), 5, 0), !bGXsfl_11_Refreshing);
            bttSendinvitatiotogroupmembers_Visible = 0;
            AssignProp(sPrefix, false, bttSendinvitatiotogroupmembers_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSendinvitatiotogroupmembers_Visible), 5, 0), true);
         }
      }

      protected void E131J2( )
      {
         AV60GXV2 = nGXsfl_11_idx;
         if ( ( AV60GXV2 > 0 ) && ( AV14groupContacts.Count >= AV60GXV2 ) )
         {
            AV14groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV60GXV2));
         }
         /* 'Save' Routine */
         returnInSub = false;
         AV35saveAndReturn = true;
         AssignAttri(sPrefix, false, "AV35saveAndReturn", AV35saveAndReturn);
         /* Execute user subroutine: 'SAVE AND RETURN' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV11group_sdt", AV11group_sdt);
      }

      protected void S112( )
      {
         /* 'SAVE AND RETURN' Routine */
         returnInSub = false;
         if ( AV11group_sdt.gxTpr_Minimumshares <= 1 )
         {
            AV8error = "The \"Minimum amount of shares to recover the secret\" has to be at least 2";
            AssignAttri(sPrefix, false, "AV8error", AV8error);
            GX_msglist.addItem(AV8error);
         }
         else
         {
            /* Execute user subroutine: 'COUNT TOTAL SHARES AND EMPTY' */
            S132 ();
            if (returnInSub) return;
            if ( AV11group_sdt.gxTpr_Minimumshares > AV22totalUserShares )
            {
               AV8error = "The \"Minimum amount of shares to recover the secret\" cannot be bigger that the total amont of shares for each contact";
               AssignAttri(sPrefix, false, "AV8error", AV8error);
               GX_msglist.addItem(AV8error);
            }
            else
            {
               if ( AV34hasContactEmptyShares )
               {
                  AV8error = "Each contact must have at least one (1) share";
                  AssignAttri(sPrefix, false, "AV8error", AV8error);
                  GX_msglist.addItem(AV8error);
               }
               else
               {
                  AV11group_sdt.gxTpr_Contact.Clear();
                  AV11group_sdt.gxTpr_Contact = AV14groupContacts;
                  GXt_char1 = AV8error;
                  new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV11group_sdt,  StringUtil.Trim( AV11group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV15grpupId, out  GXt_char1) ;
                  AV8error = GXt_char1;
                  AssignAttri(sPrefix, false, "AV8error", AV8error);
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                  {
                     AV6all_groups_sdt.Clear();
                     AV6all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV8error), null);
                     AV68GXV10 = 1;
                     while ( AV68GXV10 <= AV6all_groups_sdt.Count )
                     {
                        AV12group_sdt_delete = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV6all_groups_sdt.Item(AV68GXV10));
                        if ( AV12group_sdt_delete.gxTpr_Groupid == AV11group_sdt.gxTpr_Groupid )
                        {
                           AV6all_groups_sdt.RemoveItem(AV6all_groups_sdt.IndexOf(AV12group_sdt_delete));
                        }
                        AV68GXV10 = (int)(AV68GXV10+1);
                     }
                     AV6all_groups_sdt.Add(AV11group_sdt, 0);
                     new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "gropus.enc",  AV6all_groups_sdt.ToJSonString(false), out  AV8error) ;
                     AssignAttri(sPrefix, false, "AV8error", AV8error);
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                     {
                        if ( AV35saveAndReturn )
                        {
                           AV5websession.Set("Group_EDIT", "");
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
                        GX_msglist.addItem(AV8error);
                     }
                  }
                  else
                  {
                     GX_msglist.addItem(AV8error);
                  }
               }
            }
         }
      }

      protected void E141J2( )
      {
         /* 'Cancel edit' Routine */
         returnInSub = false;
         AV5websession.Set("Group_EDIT", "");
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
         /*  Sending Event outputs  */
      }

      private void E181J2( )
      {
         /* Gridcontacts_Load Routine */
         returnInSub = false;
         AV60GXV2 = 1;
         while ( AV60GXV2 <= AV14groupContacts.Count )
         {
            AV14groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV60GXV2));
            if ( AV11group_sdt.gxTpr_Amigroupowner )
            {
               edtavCtlcontactprivatename_Visible = 1;
               edtavCtlcontactusername_Visible = 0;
            }
            else
            {
               edtavCtlcontactprivatename_Visible = 0;
               edtavCtlcontactusername_Visible = 1;
            }
            if ( ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)(AV14groupContacts.CurrentItem)).gxTpr_Contactinvisent )
            {
               edtavRemovecontact_Visible = 0;
            }
            else
            {
               edtavRemovecontact_Visible = 1;
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 11;
            }
            sendrow_112( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_11_Refreshing )
            {
               DoAjaxLoad(11, GridcontactsRow);
            }
            AV60GXV2 = (int)(AV60GXV2+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E151J2( )
      {
         AV60GXV2 = nGXsfl_11_idx;
         if ( ( AV60GXV2 > 0 ) && ( AV14groupContacts.Count >= AV60GXV2 ) )
         {
            AV14groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV60GXV2));
         }
         /* Extensions\Web\Popup_Onpopupclosed Routine */
         returnInSub = false;
         AV9expectedPopupName = "Wallet.registered.PromptContac";
         AV58strFound = (short)(StringUtil.StringSearch( AV18PopupName, StringUtil.Trim( StringUtil.Lower( AV9expectedPopupName)), 1));
         if ( AV58strFound > 0 )
         {
            AV19removeContact = "Remove contact";
            AssignAttri(sPrefix, false, edtavRemovecontact_Internalname, AV19removeContact);
            AV10found = false;
            AV7contact.FromJSonString(AV5websession.Get("Contact_SDT_SELECTED"), null);
            if ( ! (Guid.Empty==AV7contact.gxTpr_Contactrid) )
            {
               AV69GXV11 = 1;
               while ( AV69GXV11 <= AV14groupContacts.Count )
               {
                  AV17oneGroupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV69GXV11));
                  if ( AV17oneGroupContact.gxTpr_Contactid == AV7contact.gxTpr_Contactrid )
                  {
                     AV10found = true;
                  }
                  AV69GXV11 = (int)(AV69GXV11+1);
               }
               if ( ! AV10found )
               {
                  AV32groupContactAdd.gxTpr_Contactid = AV7contact.gxTpr_Contactrid;
                  AV32groupContactAdd.gxTpr_Contactprivatename = AV7contact.gxTpr_Userprivatename;
                  AV32groupContactAdd.gxTpr_Contactusername = AV7contact.gxTpr_Username;
                  AV32groupContactAdd.gxTpr_Contactuserpubkey = StringUtil.Trim( AV7contact.gxTpr_Grouppubkey);
                  AV14groupContacts.Add(AV32groupContactAdd, 0);
                  gx_BV11 = true;
               }
            }
         }
         else
         {
            AV9expectedPopupName = "Wallet.ApproveGroupActivation";
            AV58strFound = (short)(StringUtil.StringSearch( AV18PopupName, StringUtil.Trim( StringUtil.Lower( AV9expectedPopupName)), 1));
            if ( AV58strFound > 0 )
            {
               GXt_SdtExtKeyInfo2 = AV36extKeyInfo;
               new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo2) ;
               AV36extKeyInfo = GXt_SdtExtKeyInfo2;
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV36extKeyInfo.gxTpr_Privatekey)) )
               {
                  /* Execute user subroutine: 'CONFIRMED AND ACTIVATE' */
                  S122 ();
                  if (returnInSub) return;
               }
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV32groupContactAdd", AV32groupContactAdd);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV14groupContacts", AV14groupContacts);
         nGXsfl_11_bak_idx = nGXsfl_11_idx;
         gxgrGridcontacts_refresh( AV19removeContact, AV11group_sdt, AV14groupContacts, sPrefix) ;
         nGXsfl_11_idx = nGXsfl_11_bak_idx;
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV11group_sdt", AV11group_sdt);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV27message_signature", AV27message_signature);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV28sdt_message", AV28sdt_message);
      }

      protected void E191J2( )
      {
         AV60GXV2 = nGXsfl_11_idx;
         if ( ( AV60GXV2 > 0 ) && ( AV14groupContacts.Count >= AV60GXV2 ) )
         {
            AV14groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV60GXV2));
         }
         /* 'Remove contact' Routine */
         returnInSub = false;
         AV14groupContacts.RemoveItem(AV14groupContacts.IndexOf(((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)(AV14groupContacts.CurrentItem))));
         gx_BV11 = true;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV14groupContacts", AV14groupContacts);
         nGXsfl_11_bak_idx = nGXsfl_11_idx;
         gxgrGridcontacts_refresh( AV19removeContact, AV11group_sdt, AV14groupContacts, sPrefix) ;
         nGXsfl_11_idx = nGXsfl_11_bak_idx;
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
      }

      protected void S132( )
      {
         /* 'COUNT TOTAL SHARES AND EMPTY' Routine */
         returnInSub = false;
         AV34hasContactEmptyShares = false;
         AssignAttri(sPrefix, false, "AV34hasContactEmptyShares", AV34hasContactEmptyShares);
         AV22totalUserShares = 0;
         AssignAttri(sPrefix, false, "AV22totalUserShares", StringUtil.LTrimStr( (decimal)(AV22totalUserShares), 4, 0));
         AV70GXV12 = 1;
         while ( AV70GXV12 <= AV14groupContacts.Count )
         {
            AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV70GXV12));
            AV22totalUserShares = (short)(AV22totalUserShares+(AV13groupContact.gxTpr_Numshares));
            AssignAttri(sPrefix, false, "AV22totalUserShares", StringUtil.LTrimStr( (decimal)(AV22totalUserShares), 4, 0));
            if ( AV13groupContact.gxTpr_Numshares == 0 )
            {
               AV34hasContactEmptyShares = true;
               AssignAttri(sPrefix, false, "AV34hasContactEmptyShares", AV34hasContactEmptyShares);
            }
            AV70GXV12 = (int)(AV70GXV12+1);
         }
      }

      protected void E161J2( )
      {
         AV60GXV2 = nGXsfl_11_idx;
         if ( ( AV60GXV2 > 0 ) && ( AV14groupContacts.Count >= AV60GXV2 ) )
         {
            AV14groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV60GXV2));
         }
         /* 'Send Invitation to Group members' Routine */
         returnInSub = false;
         AV35saveAndReturn = false;
         AssignAttri(sPrefix, false, "AV35saveAndReturn", AV35saveAndReturn);
         /* Execute user subroutine: 'SAVE AND RETURN' */
         S112 ();
         if (returnInSub) return;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            GXt_SdtExternalUser3 = AV29externalUser;
            new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser3) ;
            AV29externalUser = GXt_SdtExternalUser3;
            AV30group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
            AV30group_sdt_temp.gxTpr_Groupname = AV11group_sdt.gxTpr_Groupname;
            AV30group_sdt_temp.gxTpr_Grouptype = AV11group_sdt.gxTpr_Grouptype;
            AV30group_sdt_temp.gxTpr_Othergroup.gxTpr_Referenceusernname = StringUtil.Trim( AV29externalUser.gxTpr_Userinfo.gxTpr_Username);
            AV30group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid = AV11group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
            AV30group_sdt_temp.gxTpr_Othergroup.gxTpr_Encpassword = AV11group_sdt.gxTpr_Othergroup.gxTpr_Encpassword;
            AV27message_signature.gxTpr_Username = StringUtil.Trim( AV29externalUser.gxTpr_Userinfo.gxTpr_Username);
            AV27message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV29externalUser.gxTpr_Groupskeyinfo.gxTpr_Publickey);
            GXt_char1 = AV8error;
            GXt_char4 = AV27message_signature.gxTpr_Signature;
            new GeneXus.Programs.nbitcoin.eccsignmsg(context ).execute(  AV29externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey,  StringUtil.Trim( AV27message_signature.gxTpr_Username)+StringUtil.Trim( AV27message_signature.gxTpr_Grouppubkey), out  GXt_char4, out  GXt_char1) ;
            AV27message_signature.gxTpr_Signature = GXt_char4;
            AV8error = GXt_char1;
            AssignAttri(sPrefix, false, "AV8error", AV8error);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
            {
               AV30group_sdt_temp.gxTpr_Othergroup.gxTpr_Signature = StringUtil.Trim( AV27message_signature.gxTpr_Signature);
               AV71GXV13 = 1;
               while ( AV71GXV13 <= AV11group_sdt.gxTpr_Contact.Count )
               {
                  AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV11group_sdt.gxTpr_Contact.Item(AV71GXV13));
                  AV13groupContact.gxTpr_Contactinvitationsent = DateTimeUtil.Now( context);
                  AV13groupContact.gxTpr_Contactinvisent = true;
                  AV28sdt_message.gxTpr_Id = Guid.NewGuid( );
                  GXt_int5 = 0;
                  new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int5) ;
                  AV28sdt_message.gxTpr_Datetimeunix = GXt_int5;
                  AV28sdt_message.gxTpr_Messagetype = 70;
                  AV28sdt_message.gxTpr_Message = AV30group_sdt_temp.ToJSonString(false, true);
                  AV7contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
                  AV7contact.gxTpr_Username = StringUtil.Trim( AV13groupContact.gxTpr_Contactusername);
                  AV7contact.gxTpr_Messagepubkey = StringUtil.Trim( AV13groupContact.gxTpr_Contactuserpubkey);
                  GXt_char4 = AV8error;
                  new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV7contact,  AV28sdt_message, out  GXt_char4) ;
                  AV8error = GXt_char4;
                  AssignAttri(sPrefix, false, "AV8error", AV8error);
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                  {
                     GX_msglist.addItem("There was a problem sending the Invitation to the Group: "+AV8error);
                     if (true) break;
                  }
                  AV71GXV13 = (int)(AV71GXV13+1);
               }
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
               {
                  GXt_char4 = AV8error;
                  new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV11group_sdt,  StringUtil.Trim( AV11group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV15grpupId, out  GXt_char4) ;
                  AV8error = GXt_char4;
                  AssignAttri(sPrefix, false, "AV8error", AV8error);
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                  {
                     AV6all_groups_sdt.Clear();
                     AV6all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV8error), null);
                     AV72GXV14 = 1;
                     while ( AV72GXV14 <= AV6all_groups_sdt.Count )
                     {
                        AV12group_sdt_delete = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV6all_groups_sdt.Item(AV72GXV14));
                        if ( AV12group_sdt_delete.gxTpr_Groupid == AV11group_sdt.gxTpr_Groupid )
                        {
                           AV6all_groups_sdt.RemoveItem(AV6all_groups_sdt.IndexOf(AV12group_sdt_delete));
                        }
                        AV72GXV14 = (int)(AV72GXV14+1);
                     }
                     AV6all_groups_sdt.Add(AV11group_sdt, 0);
                     new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "gropus.enc",  AV6all_groups_sdt.ToJSonString(false), out  AV8error) ;
                     AssignAttri(sPrefix, false, "AV8error", AV8error);
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                     {
                        AV5websession.Set("Group_EDIT", "");
                        this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Group Invitation",(string)"All invitations sent"}, true);
                        context.setWebReturnParms(new Object[] {});
                        context.setWebReturnParmsMetadata(new Object[] {});
                        context.wjLocDisableFrm = 1;
                        context.nUserReturn = 1;
                        returnInSub = true;
                        if (true) return;
                     }
                     else
                     {
                        GX_msglist.addItem(AV8error);
                     }
                  }
                  else
                  {
                     GX_msglist.addItem(AV8error);
                  }
               }
            }
            else
            {
               GX_msglist.addItem("There was a problem Signing the invitation: "+AV8error);
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV27message_signature", AV27message_signature);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV11group_sdt", AV11group_sdt);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV28sdt_message", AV28sdt_message);
      }

      protected void S122( )
      {
         /* 'CONFIRMED AND ACTIVATE' Routine */
         returnInSub = false;
         if ( AV11group_sdt.gxTpr_Minimumshares <= 1 )
         {
            GX_msglist.addItem("The \"Minimum amount of shares to recover the secret\" has to be bigger than 1");
         }
         else
         {
            /* Execute user subroutine: 'COUNT TOTAL SHARES AND EMPTY' */
            S132 ();
            if (returnInSub) return;
            if ( AV11group_sdt.gxTpr_Minimumshares > AV22totalUserShares )
            {
               GX_msglist.addItem("The \"Minimum amount of shares to recover the secret\" cannot be bigger that the total amont of shares for each contact");
            }
            else
            {
               if ( AV34hasContactEmptyShares )
               {
                  GX_msglist.addItem("Each contact must have at least one (1) share");
               }
               else
               {
                  GXt_SdtExtKeyInfo2 = AV36extKeyInfo;
                  new GeneXus.Programs.wallet.getextkey(context ).execute( out  GXt_SdtExtKeyInfo2) ;
                  AV36extKeyInfo = GXt_SdtExtKeyInfo2;
                  AV20secret = AV36extKeyInfo.gxTpr_Extended.gxTpr_Privatekeytaproot;
                  new GeneXus.Programs.wallet.cleanprivatekeys(context ).execute( ) ;
                  GXt_char4 = AV8error;
                  new GeneXus.Programs.shamirss.createshares(context ).execute(  AV20secret,  AV22totalUserShares,  AV11group_sdt.gxTpr_Minimumshares, out  AV21shares, ref  GXt_char4) ;
                  AV8error = GXt_char4;
                  AssignAttri(sPrefix, false, "AV8error", AV8error);
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                  {
                     AV23assignShares = 1;
                     AV73GXV15 = 1;
                     while ( AV73GXV15 <= AV11group_sdt.gxTpr_Contact.Count )
                     {
                        AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV11group_sdt.gxTpr_Contact.Item(AV73GXV15));
                        AV24userShares.Clear();
                        AV16i = 1;
                        while ( AV16i <= AV13groupContact.gxTpr_Numshares )
                        {
                           AV24userShares.Add(((string)AV21shares.Item(AV23assignShares)), 0);
                           AV23assignShares = (short)(AV23assignShares+1);
                           AV16i = (short)(AV16i+1);
                        }
                        GXt_char4 = AV8error;
                        GXt_char1 = AV13groupContact.gxTpr_Contactencryptedkey;
                        GXt_char6 = AV13groupContact.gxTpr_Contactiv;
                        GXt_char7 = AV13groupContact.gxTpr_Contactencryptedtext;
                        new GeneXus.Programs.distributedcrypto.encryptjsonto(context ).execute(  AV24userShares.ToJSonString(false),  StringUtil.Trim( AV13groupContact.gxTpr_Contactuserpubkey), out  GXt_char1, out  GXt_char6, out  GXt_char7, out  GXt_char4) ;
                        AV13groupContact.gxTpr_Contactencryptedkey = GXt_char1;
                        AV13groupContact.gxTpr_Contactiv = GXt_char6;
                        AV13groupContact.gxTpr_Contactencryptedtext = GXt_char7;
                        AV8error = GXt_char4;
                        AssignAttri(sPrefix, false, "AV8error", AV8error);
                        AV73GXV15 = (int)(AV73GXV15+1);
                     }
                     AV11group_sdt.gxTpr_Isactive = true;
                     /* Execute user subroutine: 'SEND ACTIVATION TO GROUP MEMBERS' */
                     S142 ();
                     if (returnInSub) return;
                  }
                  else
                  {
                     GX_msglist.addItem(AV8error);
                  }
               }
            }
         }
      }

      protected void S142( )
      {
         /* 'SEND ACTIVATION TO GROUP MEMBERS' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            GXt_SdtExternalUser3 = AV29externalUser;
            new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser3) ;
            AV29externalUser = GXt_SdtExternalUser3;
            AV30group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
            AV30group_sdt_temp.gxTpr_Groupname = AV11group_sdt.gxTpr_Groupname;
            AV30group_sdt_temp.gxTpr_Grouptype = AV11group_sdt.gxTpr_Grouptype;
            AV30group_sdt_temp.gxTpr_Minimumshares = AV11group_sdt.gxTpr_Minimumshares;
            AV30group_sdt_temp.gxTpr_Othergroup.gxTpr_Referenceusernname = StringUtil.Trim( AV29externalUser.gxTpr_Userinfo.gxTpr_Username);
            AV30group_sdt_temp.gxTpr_Othergroup.gxTpr_Referencegroupid = AV11group_sdt.gxTpr_Othergroup.gxTpr_Referencegroupid;
            AV30group_sdt_temp.gxTpr_Othergroup.gxTpr_Encpassword = AV11group_sdt.gxTpr_Othergroup.gxTpr_Encpassword;
            AV27message_signature.gxTpr_Username = StringUtil.Trim( AV29externalUser.gxTpr_Userinfo.gxTpr_Username);
            AV27message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV29externalUser.gxTpr_Groupskeyinfo.gxTpr_Publickey);
            GXt_char7 = AV8error;
            GXt_char6 = AV27message_signature.gxTpr_Signature;
            new GeneXus.Programs.nbitcoin.eccsignmsg(context ).execute(  AV29externalUser.gxTpr_Groupskeyinfo.gxTpr_Privatekey,  StringUtil.Trim( AV27message_signature.gxTpr_Username)+StringUtil.Trim( AV27message_signature.gxTpr_Grouppubkey), out  GXt_char6, out  GXt_char7) ;
            AV27message_signature.gxTpr_Signature = GXt_char6;
            AV8error = GXt_char7;
            AssignAttri(sPrefix, false, "AV8error", AV8error);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
            {
               AV30group_sdt_temp.gxTpr_Othergroup.gxTpr_Signature = StringUtil.Trim( AV27message_signature.gxTpr_Signature);
               AV74GXV16 = 1;
               while ( AV74GXV16 <= AV11group_sdt.gxTpr_Contact.Count )
               {
                  AV13groupContact = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV11group_sdt.gxTpr_Contact.Item(AV74GXV16));
                  AV13groupContact.gxTpr_Contactinvitationsent = DateTimeUtil.Now( context);
                  AV13groupContact.gxTpr_Contactinvisent = true;
                  AV28sdt_message.gxTpr_Id = Guid.NewGuid( );
                  GXt_int5 = 0;
                  new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int5) ;
                  AV28sdt_message.gxTpr_Datetimeunix = GXt_int5;
                  AV28sdt_message.gxTpr_Messagetype = 90;
                  AV28sdt_message.gxTpr_Message = AV30group_sdt_temp.ToJSonString(false, true);
                  AV7contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
                  AV7contact.gxTpr_Username = StringUtil.Trim( AV13groupContact.gxTpr_Contactusername);
                  AV7contact.gxTpr_Messagepubkey = StringUtil.Trim( AV13groupContact.gxTpr_Contactuserpubkey);
                  GXt_char7 = AV8error;
                  new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV7contact,  AV28sdt_message, out  GXt_char7) ;
                  AV8error = GXt_char7;
                  AssignAttri(sPrefix, false, "AV8error", AV8error);
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                  {
                     GX_msglist.addItem("There was a problem sending the Invitation to the Group: "+AV8error);
                     if (true) break;
                  }
                  AV74GXV16 = (int)(AV74GXV16+1);
               }
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
               {
                  GXt_char7 = AV8error;
                  new GeneXus.Programs.wallet.registered.updategroup(context ).execute(  AV11group_sdt,  StringUtil.Trim( AV11group_sdt.gxTpr_Othergroup.gxTpr_Encpassword), out  AV15grpupId, out  GXt_char7) ;
                  AV8error = GXt_char7;
                  AssignAttri(sPrefix, false, "AV8error", AV8error);
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                  {
                     AV6all_groups_sdt.Clear();
                     AV6all_groups_sdt.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "gropus.enc", out  AV8error), null);
                     AV75GXV17 = 1;
                     while ( AV75GXV17 <= AV6all_groups_sdt.Count )
                     {
                        AV12group_sdt_delete = ((GeneXus.Programs.wallet.registered.SdtGroup_SDT)AV6all_groups_sdt.Item(AV75GXV17));
                        if ( AV12group_sdt_delete.gxTpr_Groupid == AV11group_sdt.gxTpr_Groupid )
                        {
                           AV6all_groups_sdt.RemoveItem(AV6all_groups_sdt.IndexOf(AV12group_sdt_delete));
                        }
                        AV75GXV17 = (int)(AV75GXV17+1);
                     }
                     AV6all_groups_sdt.Add(AV11group_sdt, 0);
                     new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "gropus.enc",  AV6all_groups_sdt.ToJSonString(false), out  AV8error) ;
                     AssignAttri(sPrefix, false, "AV8error", AV8error);
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                     {
                        AV5websession.Set("Group_EDIT", "");
                        this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Group Activation",(string)"All notifications sent"}, true);
                        context.setWebReturnParms(new Object[] {});
                        context.setWebReturnParmsMetadata(new Object[] {});
                        context.wjLocDisableFrm = 1;
                        context.nUserReturn = 1;
                        returnInSub = true;
                        if (true) return;
                     }
                     else
                     {
                        GX_msglist.addItem(AV8error);
                     }
                  }
                  else
                  {
                     GX_msglist.addItem(AV8error);
                  }
               }
            }
            else
            {
               GX_msglist.addItem("There was a problem Signing the invitation: "+AV8error);
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
         PA1J2( ) ;
         WS1J2( ) ;
         WE1J2( ) ;
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
         PA1J2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\registered\\smartgroupwalletbackup", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA1J2( ) ;
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
         PA1J2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS1J2( ) ;
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
         WS1J2( ) ;
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
         WE1J2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202541913335749", true, true);
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
         context.AddJavascriptSource("wallet/registered/smartgroupwalletbackup.js", "?202541913335749", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_112( )
      {
         edtavCtlcontactid1_Internalname = sPrefix+"CTLCONTACTID1_"+sGXsfl_11_idx;
         edtavCtlcontactprivatename_Internalname = sPrefix+"CTLCONTACTPRIVATENAME_"+sGXsfl_11_idx;
         edtavCtlcontactusername_Internalname = sPrefix+"CTLCONTACTUSERNAME_"+sGXsfl_11_idx;
         edtavCtlnumshares_Internalname = sPrefix+"CTLNUMSHARES_"+sGXsfl_11_idx;
         edtavCtlcontactinvitationsent_Internalname = sPrefix+"CTLCONTACTINVITATIONSENT_"+sGXsfl_11_idx;
         edtavCtlcontactinvitacionaccepted_Internalname = sPrefix+"CTLCONTACTINVITACIONACCEPTED_"+sGXsfl_11_idx;
         edtavRemovecontact_Internalname = sPrefix+"vREMOVECONTACT_"+sGXsfl_11_idx;
      }

      protected void SubsflControlProps_fel_112( )
      {
         edtavCtlcontactid1_Internalname = sPrefix+"CTLCONTACTID1_"+sGXsfl_11_fel_idx;
         edtavCtlcontactprivatename_Internalname = sPrefix+"CTLCONTACTPRIVATENAME_"+sGXsfl_11_fel_idx;
         edtavCtlcontactusername_Internalname = sPrefix+"CTLCONTACTUSERNAME_"+sGXsfl_11_fel_idx;
         edtavCtlnumshares_Internalname = sPrefix+"CTLNUMSHARES_"+sGXsfl_11_fel_idx;
         edtavCtlcontactinvitationsent_Internalname = sPrefix+"CTLCONTACTINVITATIONSENT_"+sGXsfl_11_fel_idx;
         edtavCtlcontactinvitacionaccepted_Internalname = sPrefix+"CTLCONTACTINVITACIONACCEPTED_"+sGXsfl_11_fel_idx;
         edtavRemovecontact_Internalname = sPrefix+"vREMOVECONTACT_"+sGXsfl_11_fel_idx;
      }

      protected void sendrow_112( )
      {
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
         WB1J0( ) ;
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
            if ( ((int)((nGXsfl_11_idx) % (2))) == 0 )
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
            context.WriteHtmlText( " gxrow=\""+sGXsfl_11_idx+"\">") ;
         }
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+"display:none;"+"\">") ;
         }
         /* Single line edit */
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactid1_Internalname,((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV60GXV2)).gxTpr_Contactid.ToString(),((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV60GXV2)).gxTpr_Contactid.ToString(),""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactid1_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtlcontactid1_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)36,(short)0,(short)0,(short)11,(short)0,(short)0,(short)0,(bool)true,(string)"",(string)"",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavCtlcontactprivatename_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'" + sPrefix + "',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactprivatename_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV60GXV2)).gxTpr_Contactprivatename),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,13);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactprivatename_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(int)edtavCtlcontactprivatename_Visible,(int)edtavCtlcontactprivatename_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavCtlcontactusername_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 14,'" + sPrefix + "',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactusername_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV60GXV2)).gxTpr_Contactusername),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,14);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactusername_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(int)edtavCtlcontactusername_Visible,(int)edtavCtlcontactusername_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 15,'" + sPrefix + "',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlnumshares_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV60GXV2)).gxTpr_Numshares), 4, 0, ".", "")),StringUtil.LTrim( context.localUtil.Format( (decimal)(((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV60GXV2)).gxTpr_Numshares), "ZZZ9"))," dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onblur(this,15);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlnumshares_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlnumshares_Enabled,(short)1,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 16,'" + sPrefix + "',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactinvitationsent_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV60GXV2)).gxTpr_Contactinvitationsent, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV60GXV2)).gxTpr_Contactinvitationsent, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onblur(this,16);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactinvitationsent_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlcontactinvitationsent_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'" + sPrefix + "',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactinvitacionaccepted_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV60GXV2)).gxTpr_Contactinvitacionaccepted, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem)AV14groupContacts.Item(AV60GXV2)).gxTpr_Contactinvitacionaccepted, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onblur(this,17);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactinvitacionaccepted_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlcontactinvitacionaccepted_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavRemovecontact_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 18,'" + sPrefix + "',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavRemovecontact_Internalname,StringUtil.RTrim( AV19removeContact),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,18);\"","'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'REMOVE CONTACT\\'."+sGXsfl_11_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavRemovecontact_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(int)edtavRemovecontact_Visible,(int)edtavRemovecontact_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         send_integrity_lvl_hashes1J2( ) ;
         GridcontactsContainer.AddRow(GridcontactsRow);
         nGXsfl_11_idx = ((subGridcontacts_Islastpage==1)&&(nGXsfl_11_idx+1>subGridcontacts_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_idx+1);
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
         /* End function sendrow_112 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl11( )
      {
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"GridcontactsContainer"+"DivS\" data-gxgridid=\"11\">") ;
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
            context.SendWebValue( "Number of shares") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Invitation Sent") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Invitacion Accepted") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtavRemovecontact_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "") ;
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
            GridcontactsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridcontactsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV19removeContact)));
            GridcontactsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavRemovecontact_Enabled), 5, 0, ".", "")));
            GridcontactsColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavRemovecontact_Visible), 5, 0, ".", "")));
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
         edtavCtlcontactid1_Internalname = sPrefix+"CTLCONTACTID1";
         edtavCtlcontactprivatename_Internalname = sPrefix+"CTLCONTACTPRIVATENAME";
         edtavCtlcontactusername_Internalname = sPrefix+"CTLCONTACTUSERNAME";
         edtavCtlnumshares_Internalname = sPrefix+"CTLNUMSHARES";
         edtavCtlcontactinvitationsent_Internalname = sPrefix+"CTLCONTACTINVITATIONSENT";
         edtavCtlcontactinvitacionaccepted_Internalname = sPrefix+"CTLCONTACTINVITACIONACCEPTED";
         edtavRemovecontact_Internalname = sPrefix+"vREMOVECONTACT";
         bttAddacontact_Internalname = sPrefix+"ADDACONTACT";
         bttSave_Internalname = sPrefix+"SAVE";
         bttCancel_Internalname = sPrefix+"CANCEL";
         bttSendinvitatiotogroupmembers_Internalname = sPrefix+"SENDINVITATIOTOGROUPMEMBERS";
         bttActivategroup_Internalname = sPrefix+"ACTIVATEGROUP";
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
         edtavRemovecontact_Jsonclick = "";
         edtavRemovecontact_Enabled = 1;
         edtavRemovecontact_Visible = -1;
         edtavCtlcontactinvitacionaccepted_Jsonclick = "";
         edtavCtlcontactinvitacionaccepted_Enabled = 0;
         edtavCtlcontactinvitationsent_Jsonclick = "";
         edtavCtlcontactinvitationsent_Enabled = 0;
         edtavCtlnumshares_Jsonclick = "";
         edtavCtlnumshares_Enabled = 1;
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
         edtavCtlnumshares_Enabled = 1;
         edtavCtlminimumshares_Enabled = 1;
         bttActivategroup_Visible = 1;
         bttSendinvitatiotogroupmembers_Visible = 1;
         bttCancel_Caption = "Cancel";
         bttSave_Visible = 1;
         bttAddacontact_Visible = 1;
         edtavCtlminimumshares_Jsonclick = "";
         edtavCtlminimumshares_Enabled = 1;
         edtavCtlcontactinvitacionaccepted_Enabled = -1;
         edtavCtlcontactinvitationsent_Enabled = -1;
         edtavCtlcontactusername_Enabled = -1;
         edtavCtlcontactprivatename_Enabled = -1;
         edtavCtlcontactid1_Enabled = -1;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"GRIDCONTACTS_nEOF","type":"int"},{"av":"AV19removeContact","fld":"vREMOVECONTACT","type":"char"},{"av":"AV11group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV14groupContacts","fld":"vGROUPCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"nRC_GXsfl_11","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":11,"type":"int"},{"av":"sPrefix","type":"char"}]}""");
         setEventMetadata("'SAVE'","""{"handler":"E131J2","iparms":[{"av":"AV11group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV22totalUserShares","fld":"vTOTALUSERSHARES","pic":"ZZZ9","type":"int"},{"av":"AV34hasContactEmptyShares","fld":"vHASCONTACTEMPTYSHARES","type":"boolean"},{"av":"AV14groupContacts","fld":"vGROUPCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":11,"type":"int"},{"av":"AV35saveAndReturn","fld":"vSAVEANDRETURN","type":"boolean"}]""");
         setEventMetadata("'SAVE'",""","oparms":[{"av":"AV35saveAndReturn","fld":"vSAVEANDRETURN","type":"boolean"},{"av":"AV11group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV8error","fld":"vERROR","type":"char"},{"av":"AV34hasContactEmptyShares","fld":"vHASCONTACTEMPTYSHARES","type":"boolean"},{"av":"AV22totalUserShares","fld":"vTOTALUSERSHARES","pic":"ZZZ9","type":"int"}]}""");
         setEventMetadata("'CANCEL EDIT'","""{"handler":"E141J2","iparms":[]}""");
         setEventMetadata("'ADD A CONTACT'","""{"handler":"E111J1","iparms":[]}""");
         setEventMetadata("GRIDCONTACTS.LOAD","""{"handler":"E181J2","iparms":[{"av":"AV11group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV14groupContacts","fld":"vGROUPCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":11,"type":"int"}]""");
         setEventMetadata("GRIDCONTACTS.LOAD",""","oparms":[{"ctrl":"CTLCONTACTPRIVATENAME","prop":"Visible"},{"ctrl":"CTLCONTACTUSERNAME","prop":"Visible"},{"av":"edtavRemovecontact_Visible","ctrl":"vREMOVECONTACT","prop":"Visible"}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED","""{"handler":"E151J2","iparms":[{"av":"AV18PopupName","fld":"vPOPUPNAME","type":"char"},{"av":"AV14groupContacts","fld":"vGROUPCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":11,"type":"int"},{"av":"AV32groupContactAdd","fld":"vGROUPCONTACTADD","type":""},{"av":"AV11group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV22totalUserShares","fld":"vTOTALUSERSHARES","pic":"ZZZ9","type":"int"},{"av":"AV34hasContactEmptyShares","fld":"vHASCONTACTEMPTYSHARES","type":"boolean"},{"av":"AV8error","fld":"vERROR","type":"char"},{"av":"AV27message_signature","fld":"vMESSAGE_SIGNATURE","type":""},{"av":"AV28sdt_message","fld":"vSDT_MESSAGE","type":""},{"av":"GRIDCONTACTS_nEOF","type":"int"},{"av":"AV19removeContact","fld":"vREMOVECONTACT","type":"char"},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",""","oparms":[{"av":"AV19removeContact","fld":"vREMOVECONTACT","type":"char"},{"av":"AV32groupContactAdd","fld":"vGROUPCONTACTADD","type":""},{"av":"AV14groupContacts","fld":"vGROUPCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":11,"type":"int"},{"av":"AV8error","fld":"vERROR","type":"char"},{"av":"AV11group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV34hasContactEmptyShares","fld":"vHASCONTACTEMPTYSHARES","type":"boolean"},{"av":"AV22totalUserShares","fld":"vTOTALUSERSHARES","pic":"ZZZ9","type":"int"},{"av":"AV27message_signature","fld":"vMESSAGE_SIGNATURE","type":""},{"av":"AV28sdt_message","fld":"vSDT_MESSAGE","type":""}]}""");
         setEventMetadata("'REMOVE CONTACT'","""{"handler":"E191J2","iparms":[{"av":"AV14groupContacts","fld":"vGROUPCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":11,"type":"int"},{"av":"GRIDCONTACTS_nEOF","type":"int"},{"av":"AV19removeContact","fld":"vREMOVECONTACT","type":"char"},{"av":"AV11group_sdt","fld":"vGROUP_SDT","type":""},{"av":"sPrefix","type":"char"}]""");
         setEventMetadata("'REMOVE CONTACT'",""","oparms":[{"av":"AV14groupContacts","fld":"vGROUPCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":11,"type":"int"}]}""");
         setEventMetadata("'SEND INVITATION TO GROUP MEMBERS'","""{"handler":"E161J2","iparms":[{"av":"AV8error","fld":"vERROR","type":"char"},{"av":"AV11group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV27message_signature","fld":"vMESSAGE_SIGNATURE","type":""},{"av":"AV28sdt_message","fld":"vSDT_MESSAGE","type":""},{"av":"AV22totalUserShares","fld":"vTOTALUSERSHARES","pic":"ZZZ9","type":"int"},{"av":"AV34hasContactEmptyShares","fld":"vHASCONTACTEMPTYSHARES","type":"boolean"},{"av":"AV14groupContacts","fld":"vGROUPCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":11,"type":"int"},{"av":"AV35saveAndReturn","fld":"vSAVEANDRETURN","type":"boolean"}]""");
         setEventMetadata("'SEND INVITATION TO GROUP MEMBERS'",""","oparms":[{"av":"AV35saveAndReturn","fld":"vSAVEANDRETURN","type":"boolean"},{"av":"AV27message_signature","fld":"vMESSAGE_SIGNATURE","type":""},{"av":"AV8error","fld":"vERROR","type":"char"},{"av":"AV11group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV28sdt_message","fld":"vSDT_MESSAGE","type":""},{"av":"AV34hasContactEmptyShares","fld":"vHASCONTACTEMPTYSHARES","type":"boolean"},{"av":"AV22totalUserShares","fld":"vTOTALUSERSHARES","pic":"ZZZ9","type":"int"}]}""");
         setEventMetadata("'ACTIVATE GROUP'","""{"handler":"E121J1","iparms":[]}""");
         setEventMetadata("VALIDV_GXV3","""{"handler":"Validv_Gxv3","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Removecontact","iparms":[]}""");
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
         AV19removeContact = "";
         AV11group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV14groupContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem>( context, "Group_SDT.ContactItem", "distributedcryptography");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV18PopupName = "";
         AV32groupContactAdd = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV8error = "";
         AV27message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         AV28sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         GX_FocusControl = "";
         TempTags = "";
         GridcontactsContainer = new GXWebGrid( context);
         sStyleString = "";
         ClassString = "";
         StyleString = "";
         bttAddacontact_Jsonclick = "";
         bttSave_Jsonclick = "";
         bttCancel_Jsonclick = "";
         bttSendinvitatiotogroupmembers_Jsonclick = "";
         bttActivategroup_Jsonclick = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV6all_groups_sdt = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT>( context, "Group_SDT", "distributedcryptography");
         AV5websession = context.GetSession();
         AV13groupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV15grpupId = Guid.Empty;
         AV12group_sdt_delete = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GridcontactsRow = new GXWebRow();
         AV9expectedPopupName = "";
         AV7contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV17oneGroupContact = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem(context);
         AV36extKeyInfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV29externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV30group_sdt_temp = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         GXt_SdtExtKeyInfo2 = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
         AV20secret = "";
         AV21shares = new GxSimpleCollection<string>();
         AV24userShares = new GxSimpleCollection<string>();
         GXt_char4 = "";
         GXt_char1 = "";
         GXt_SdtExternalUser3 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         GXt_char6 = "";
         GXt_char7 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGridcontacts_Linesclass = "";
         ROClassString = "";
         GridcontactsColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtlcontactid1_Enabled = 0;
         edtavCtlcontactprivatename_Enabled = 0;
         edtavCtlcontactusername_Enabled = 0;
         edtavCtlcontactinvitationsent_Enabled = 0;
         edtavCtlcontactinvitacionaccepted_Enabled = 0;
         edtavRemovecontact_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short AV22totalUserShares ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGridcontacts_Backcolorstyle ;
      private short AV33totalInvitationsAccepted ;
      private short AV58strFound ;
      private short GRIDCONTACTS_nEOF ;
      private short AV23assignShares ;
      private short AV16i ;
      private short nGXWrapped ;
      private short subGridcontacts_Backstyle ;
      private short subGridcontacts_Titlebackstyle ;
      private short subGridcontacts_Allowselection ;
      private short subGridcontacts_Allowhovering ;
      private short subGridcontacts_Allowcollapsing ;
      private short subGridcontacts_Collapsed ;
      private int nRC_GXsfl_11 ;
      private int nGXsfl_11_idx=1 ;
      private int edtavCtlcontactid1_Enabled ;
      private int edtavCtlcontactprivatename_Enabled ;
      private int edtavCtlcontactusername_Enabled ;
      private int edtavCtlcontactinvitationsent_Enabled ;
      private int edtavCtlcontactinvitacionaccepted_Enabled ;
      private int edtavRemovecontact_Enabled ;
      private int edtavCtlminimumshares_Enabled ;
      private int AV60GXV2 ;
      private int bttAddacontact_Visible ;
      private int bttSave_Visible ;
      private int bttSendinvitatiotogroupmembers_Visible ;
      private int bttActivategroup_Visible ;
      private int subGridcontacts_Islastpage ;
      private int nGXsfl_11_fel_idx=1 ;
      private int AV67GXV9 ;
      private int edtavCtlnumshares_Enabled ;
      private int AV68GXV10 ;
      private int edtavCtlcontactprivatename_Visible ;
      private int edtavCtlcontactusername_Visible ;
      private int edtavRemovecontact_Visible ;
      private int AV69GXV11 ;
      private int nGXsfl_11_bak_idx=1 ;
      private int AV70GXV12 ;
      private int AV71GXV13 ;
      private int AV72GXV14 ;
      private int AV73GXV15 ;
      private int AV74GXV16 ;
      private int AV75GXV17 ;
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
      private string sGXsfl_11_idx="0001" ;
      private string AV19removeContact ;
      private string edtavCtlcontactid1_Internalname ;
      private string edtavCtlcontactprivatename_Internalname ;
      private string edtavCtlcontactusername_Internalname ;
      private string edtavCtlcontactinvitationsent_Internalname ;
      private string edtavCtlcontactinvitacionaccepted_Internalname ;
      private string edtavRemovecontact_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV18PopupName ;
      private string AV8error ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string edtavCtlminimumshares_Internalname ;
      private string TempTags ;
      private string edtavCtlminimumshares_Jsonclick ;
      private string sStyleString ;
      private string subGridcontacts_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string bttAddacontact_Internalname ;
      private string bttAddacontact_Jsonclick ;
      private string bttSave_Internalname ;
      private string bttSave_Jsonclick ;
      private string bttCancel_Internalname ;
      private string bttCancel_Caption ;
      private string bttCancel_Jsonclick ;
      private string bttSendinvitatiotogroupmembers_Internalname ;
      private string bttSendinvitatiotogroupmembers_Jsonclick ;
      private string bttActivategroup_Internalname ;
      private string bttActivategroup_Jsonclick ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string sGXsfl_11_fel_idx="0001" ;
      private string edtavCtlnumshares_Internalname ;
      private string AV9expectedPopupName ;
      private string GXt_char4 ;
      private string GXt_char1 ;
      private string GXt_char6 ;
      private string GXt_char7 ;
      private string subGridcontacts_Class ;
      private string subGridcontacts_Linesclass ;
      private string ROClassString ;
      private string edtavCtlcontactid1_Jsonclick ;
      private string edtavCtlcontactprivatename_Jsonclick ;
      private string edtavCtlcontactusername_Jsonclick ;
      private string edtavCtlnumshares_Jsonclick ;
      private string edtavCtlcontactinvitationsent_Jsonclick ;
      private string edtavCtlcontactinvitacionaccepted_Jsonclick ;
      private string edtavRemovecontact_Jsonclick ;
      private string subGridcontacts_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_11_Refreshing=false ;
      private bool AV34hasContactEmptyShares ;
      private bool AV35saveAndReturn ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV11 ;
      private bool AV10found ;
      private string AV20secret ;
      private Guid AV15grpupId ;
      private IGxSession AV5websession ;
      private GXWebGrid GridcontactsContainer ;
      private GXWebRow GridcontactsRow ;
      private GXWebColumn GridcontactsColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV11group_sdt ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem> AV14groupContacts ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV32groupContactAdd ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV27message_signature ;
      private GeneXus.Programs.nostr.SdtSDT_message AV28sdt_message ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroup_SDT> AV6all_groups_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV13groupContact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV12group_sdt_delete ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV7contact ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_ContactItem AV17oneGroupContact ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo AV36extKeyInfo ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV29externalUser ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV30group_sdt_temp ;
      private GeneXus.Programs.nbitcoin.SdtExtKeyInfo GXt_SdtExtKeyInfo2 ;
      private GxSimpleCollection<string> AV21shares ;
      private GxSimpleCollection<string> AV24userShares ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser3 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
