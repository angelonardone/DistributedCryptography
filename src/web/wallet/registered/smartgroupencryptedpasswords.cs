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
   public class smartgroupencryptedpasswords : GXWebComponent
   {
      public smartgroupencryptedpasswords( )
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

      public smartgroupencryptedpasswords( IGxContext context )
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
         nRC_GXsfl_6 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_6"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_6_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_6_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_6_idx = GetPar( "sGXsfl_6_idx");
         sPrefix = GetPar( "sPrefix");
         AV26removeContact = GetPar( "removeContact");
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
         AV26removeContact = GetPar( "removeContact");
         ajax_req_read_hidden_sdt(GetNextPar( ), AV36view);
         ajax_req_read_hidden_sdt(GetNextPar( ), AV19groupContacts);
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridcontacts_refresh( AV26removeContact, AV36view, AV19groupContacts, sPrefix) ;
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
            PA302( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavCtlcontactid1_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcontactid1_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcontactid1_Enabled), 5, 0), !bGXsfl_6_Refreshing);
               edtavCtlcontactprivatename_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcontactprivatename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcontactprivatename_Enabled), 5, 0), !bGXsfl_6_Refreshing);
               edtavCtlcontactusername_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcontactusername_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcontactusername_Enabled), 5, 0), !bGXsfl_6_Refreshing);
               edtavCtlcontactinvitationsent_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcontactinvitationsent_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcontactinvitationsent_Enabled), 5, 0), !bGXsfl_6_Refreshing);
               edtavCtlcontactinvitacionaccepted_Enabled = 0;
               AssignProp(sPrefix, false, edtavCtlcontactinvitacionaccepted_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCtlcontactinvitacionaccepted_Enabled), 5, 0), !bGXsfl_6_Refreshing);
               edtavRemovecontact_Enabled = 0;
               AssignProp(sPrefix, false, edtavRemovecontact_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavRemovecontact_Enabled), 5, 0), !bGXsfl_6_Refreshing);
               WS302( ) ;
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
            context.SendWebValue( "Smart Group Encrypted Passwords") ;
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
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.smartgroupencryptedpasswords") +"\">") ;
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vVIEW", AV36view);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vVIEW", AV36view);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vVIEW", GetSecureSignedToken( sPrefix, AV36view, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Groupcontacts", AV19groupContacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Groupcontacts", AV19groupContacts);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_6", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_6), 8, 0, ".", "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vVIEW", AV36view);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vVIEW", AV36view);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vVIEW", GetSecureSignedToken( sPrefix, AV36view, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vGROUPCONTACTS", AV19groupContacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vGROUPCONTACTS", AV19groupContacts);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"vPOPUPNAME", StringUtil.RTrim( AV25PopupName));
      }

      protected void RenderHtmlCloseForm302( )
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
         return "Wallet.registered.SmartGroupEncryptedPasswords" ;
      }

      public override string GetPgmdesc( )
      {
         return "Smart Group Encrypted Passwords" ;
      }

      protected void WB300( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wallet.registered.smartgroupencryptedpasswords");
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
            /*  Grid Control  */
            GridcontactsContainer.SetWrapped(nGXWrapped);
            StartGridControl6( ) ;
         }
         if ( wbEnd == 6 )
         {
            wbEnd = 0;
            nRC_GXsfl_6 = (int)(nGXsfl_6_idx-1);
            if ( GridcontactsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV37GXV1 = nGXsfl_6_idx;
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 14,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttAddacontact_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(6), 1, 0)+","+"null"+");", "Add a contact", bttAddacontact_Jsonclick, 7, "Add a contact", "", StyleString, ClassString, bttAddacontact_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+"e11301_client"+"'", TempTags, "", 2, "HLP_Wallet/registered/SmartGroupEncryptedPasswords.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSave_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(6), 1, 0)+","+"null"+");", "Save", bttSave_Jsonclick, 5, "Save", "", StyleString, ClassString, bttSave_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'SAVE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SmartGroupEncryptedPasswords.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCancel_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(6), 1, 0)+","+"null"+");", bttCancel_Caption, bttCancel_Jsonclick, 5, "Cancel", "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'CANCEL EDIT\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SmartGroupEncryptedPasswords.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 22,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttSendinvitatiotogroupmembers_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(6), 1, 0)+","+"null"+");", "Send Invitation to Group members", bttSendinvitatiotogroupmembers_Jsonclick, 5, "Send Invitation to Group members", "", StyleString, ClassString, bttSendinvitatiotogroupmembers_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'SEND INVITATION TO GROUP MEMBERS\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SmartGroupEncryptedPasswords.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 6 )
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
                  AV37GXV1 = nGXsfl_6_idx;
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

      protected void START302( )
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
            Form.Meta.addItem("description", "Smart Group Encrypted Passwords", 0) ;
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
               STRUP300( ) ;
            }
         }
      }

      protected void WS302( )
      {
         START302( ) ;
         EVT302( ) ;
      }

      protected void EVT302( )
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
                                 STRUP300( ) ;
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
                                 STRUP300( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Save' */
                                    E12302 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CANCEL EDIT'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP300( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Cancel edit' */
                                    E13302 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP300( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E14302 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'SEND INVITATION TO GROUP MEMBERS'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP300( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Send Invitation to Group members' */
                                    E15302 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP300( ) ;
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
                                 STRUP300( ) ;
                              }
                              nGXsfl_6_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
                              SubsflControlProps_62( ) ;
                              AV37GXV1 = nGXsfl_6_idx;
                              if ( ( AV19groupContacts.Count >= AV37GXV1 ) && ( AV37GXV1 > 0 ) )
                              {
                                 AV19groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV19groupContacts.Item(AV37GXV1));
                                 AV26removeContact = cgiGet( edtavRemovecontact_Internalname);
                                 AssignAttri(sPrefix, false, edtavRemovecontact_Internalname, AV26removeContact);
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
                                          E16302 ();
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
                                          E17302 ();
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
                                          E18302 ();
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
                                       STRUP300( ) ;
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

      protected void WE302( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm302( ) ;
            }
         }
      }

      protected void PA302( )
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
         SubsflControlProps_62( ) ;
         while ( nGXsfl_6_idx <= nRC_GXsfl_6 )
         {
            sendrow_62( ) ;
            nGXsfl_6_idx = ((subGridcontacts_Islastpage==1)&&(nGXsfl_6_idx+1>subGridcontacts_fnc_Recordsperpage( )) ? 1 : nGXsfl_6_idx+1);
            sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
            SubsflControlProps_62( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridcontactsContainer)) ;
         /* End function gxnrGridcontacts_newrow */
      }

      protected void gxgrGridcontacts_refresh( string AV26removeContact ,
                                               GeneXus.Programs.wallet.registered.SdtWalletBackupView AV36view ,
                                               GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> AV19groupContacts ,
                                               string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDCONTACTS_nCurrentRecord = 0;
         RF302( ) ;
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
         RF302( ) ;
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

      protected void RF302( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridcontactsContainer.ClearRows();
         }
         wbStart = 6;
         nGXsfl_6_idx = 1;
         sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
         SubsflControlProps_62( ) ;
         bGXsfl_6_Refreshing = true;
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
            SubsflControlProps_62( ) ;
            /* Execute user event: Gridcontacts.Load */
            E17302 ();
            wbEnd = 6;
            WB300( ) ;
         }
         bGXsfl_6_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes302( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vVIEW", AV36view);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vVIEW", AV36view);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vVIEW", GetSecureSignedToken( sPrefix, AV36view, context));
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

      protected void STRUP300( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E16302 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Groupcontacts"), AV19groupContacts);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vGROUPCONTACTS"), AV19groupContacts);
            /* Read saved values. */
            nRC_GXsfl_6 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_6"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_6 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_6"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_6_fel_idx = 0;
            while ( nGXsfl_6_fel_idx < nRC_GXsfl_6 )
            {
               nGXsfl_6_fel_idx = ((subGridcontacts_Islastpage==1)&&(nGXsfl_6_fel_idx+1>subGridcontacts_fnc_Recordsperpage( )) ? 1 : nGXsfl_6_fel_idx+1);
               sGXsfl_6_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_62( ) ;
               AV37GXV1 = nGXsfl_6_fel_idx;
               if ( ( AV19groupContacts.Count >= AV37GXV1 ) && ( AV37GXV1 > 0 ) )
               {
                  AV19groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV19groupContacts.Item(AV37GXV1));
                  AV26removeContact = cgiGet( edtavRemovecontact_Internalname);
               }
            }
            if ( nGXsfl_6_fel_idx == 0 )
            {
               nGXsfl_6_idx = 1;
               sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
               SubsflControlProps_62( ) ;
            }
            nGXsfl_6_fel_idx = 1;
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
         E16302 ();
         if (returnInSub) return;
      }

      protected void E16302( )
      {
         /* Start Routine */
         returnInSub = false;
         GXt_SdtGroupListItem1 = AV35groupView;
         new GeneXus.Programs.wallet.registered.getgroupeditview(context ).execute( out  GXt_SdtGroupListItem1) ;
         AV35groupView = GXt_SdtGroupListItem1;
         GXt_char2 = AV10error;
         new GeneXus.Programs.wallet.registered.getwalletbackupview(context ).execute(  AV35groupView.gxTpr_Groupid, out  AV36view, out  GXt_char2) ;
         AV10error = GXt_char2;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            GX_msglist.addItem(AV10error);
         }
         AV19groupContacts.Clear();
         gx_BV6 = true;
         AV26removeContact = "Remove contact";
         AssignAttri(sPrefix, false, edtavRemovecontact_Internalname, AV26removeContact);
         AV43GXV7 = 1;
         while ( AV43GXV7 <= AV36view.gxTpr_Contact.Count )
         {
            AV17groupContact = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV36view.gxTpr_Contact.Item(AV43GXV7));
            AV19groupContacts.Add(AV17groupContact, 0);
            gx_BV6 = true;
            AV43GXV7 = (int)(AV43GXV7+1);
         }
         AV36view.gxTpr_Contact.Clear();
         if ( ! AV36view.gxTpr_Amigroupowner )
         {
            bttSave_Visible = 0;
            AssignProp(sPrefix, false, bttSave_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSave_Visible), 5, 0), true);
            bttAddacontact_Visible = 0;
            AssignProp(sPrefix, false, bttAddacontact_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttAddacontact_Visible), 5, 0), true);
            bttCancel_Caption = "Close";
            AssignProp(sPrefix, false, bttCancel_Internalname, "Caption", bttCancel_Caption, true);
            AV26removeContact = "";
            AssignAttri(sPrefix, false, edtavRemovecontact_Internalname, AV26removeContact);
            bttSendinvitatiotogroupmembers_Visible = 0;
            AssignProp(sPrefix, false, bttSendinvitatiotogroupmembers_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttSendinvitatiotogroupmembers_Visible), 5, 0), true);
         }
      }

      protected void E12302( )
      {
         AV37GXV1 = nGXsfl_6_idx;
         if ( ( AV37GXV1 > 0 ) && ( AV19groupContacts.Count >= AV37GXV1 ) )
         {
            AV19groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV19groupContacts.Item(AV37GXV1));
         }
         /* 'Save' Routine */
         returnInSub = false;
         GXt_char2 = AV10error;
         new GeneXus.Programs.wallet.registered.savevaultgroup(context ).execute(  AV36view.gxTpr_Groupid,  AV19groupContacts, out  GXt_char2) ;
         AV10error = GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            AV6websession.Set("Group_EDIT", "");
            context.setWebReturnParms(new Object[] {});
            context.setWebReturnParmsMetadata(new Object[] {});
            context.wjLocDisableFrm = 1;
            context.nUserReturn = 1;
            returnInSub = true;
            if (true) return;
         }
         GX_msglist.addItem(AV10error);
         /*  Sending Event outputs  */
      }

      protected void E13302( )
      {
         /* 'Cancel edit' Routine */
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

      private void E17302( )
      {
         /* Gridcontacts_Load Routine */
         returnInSub = false;
         AV37GXV1 = 1;
         while ( AV37GXV1 <= AV19groupContacts.Count )
         {
            AV19groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV19groupContacts.Item(AV37GXV1));
            if ( AV36view.gxTpr_Amigroupowner )
            {
               edtavCtlcontactprivatename_Visible = 1;
               edtavCtlcontactusername_Visible = 0;
            }
            else
            {
               edtavCtlcontactprivatename_Visible = 0;
               edtavCtlcontactusername_Visible = 1;
            }
            if ( ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)(AV19groupContacts.CurrentItem)).gxTpr_Contactinvisent )
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
               wbStart = 6;
            }
            sendrow_62( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_6_Refreshing )
            {
               DoAjaxLoad(6, GridcontactsRow);
            }
            AV37GXV1 = (int)(AV37GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E14302( )
      {
         AV37GXV1 = nGXsfl_6_idx;
         if ( ( AV37GXV1 > 0 ) && ( AV19groupContacts.Count >= AV37GXV1 ) )
         {
            AV19groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV19groupContacts.Item(AV37GXV1));
         }
         /* Extensions\Web\Popup_Onpopupclosed Routine */
         returnInSub = false;
         AV11expectedPopupName = "Wallet.registered.PromptContac";
         AV34strFound = (short)(StringUtil.StringSearch( AV25PopupName, StringUtil.Trim( StringUtil.Lower( AV11expectedPopupName)), 1));
         if ( AV34strFound > 0 )
         {
            AV26removeContact = "Remove contact";
            AssignAttri(sPrefix, false, edtavRemovecontact_Internalname, AV26removeContact);
            AV13found = false;
            AV9contact.FromJSonString(AV6websession.Get("Contact_SDT_SELECTED"), null);
            if ( ! (Guid.Empty==AV9contact.gxTpr_Contactrid) )
            {
               AV44GXV8 = 1;
               while ( AV44GXV8 <= AV19groupContacts.Count )
               {
                  AV24oneGroupContact = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV19groupContacts.Item(AV44GXV8));
                  if ( AV24oneGroupContact.gxTpr_Contactid == AV9contact.gxTpr_Contactrid )
                  {
                     AV13found = true;
                  }
                  AV44GXV8 = (int)(AV44GXV8+1);
               }
               if ( ! AV13found )
               {
                  AV18groupContactAdd = new GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem(context);
                  AV18groupContactAdd.gxTpr_Contactid = AV9contact.gxTpr_Contactrid;
                  AV18groupContactAdd.gxTpr_Contactprivatename = AV9contact.gxTpr_Userprivatename;
                  AV18groupContactAdd.gxTpr_Contactusername = AV9contact.gxTpr_Username;
                  AV18groupContactAdd.gxTpr_Contactuserpubkey = StringUtil.Trim( AV9contact.gxTpr_Grouppubkey);
                  AV19groupContacts.Add(AV18groupContactAdd, 0);
                  gx_BV6 = true;
               }
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV19groupContacts", AV19groupContacts);
         nGXsfl_6_bak_idx = nGXsfl_6_idx;
         gxgrGridcontacts_refresh( AV26removeContact, AV36view, AV19groupContacts, sPrefix) ;
         nGXsfl_6_idx = nGXsfl_6_bak_idx;
         sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
         SubsflControlProps_62( ) ;
      }

      protected void E18302( )
      {
         AV37GXV1 = nGXsfl_6_idx;
         if ( ( AV37GXV1 > 0 ) && ( AV19groupContacts.Count >= AV37GXV1 ) )
         {
            AV19groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV19groupContacts.Item(AV37GXV1));
         }
         /* 'Remove contact' Routine */
         returnInSub = false;
         AV19groupContacts.RemoveItem(AV19groupContacts.IndexOf(((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)(AV19groupContacts.CurrentItem))));
         gx_BV6 = true;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV19groupContacts", AV19groupContacts);
         nGXsfl_6_bak_idx = nGXsfl_6_idx;
         gxgrGridcontacts_refresh( AV26removeContact, AV36view, AV19groupContacts, sPrefix) ;
         nGXsfl_6_idx = nGXsfl_6_bak_idx;
         sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
         SubsflControlProps_62( ) ;
      }

      protected void E15302( )
      {
         AV37GXV1 = nGXsfl_6_idx;
         if ( ( AV37GXV1 > 0 ) && ( AV19groupContacts.Count >= AV37GXV1 ) )
         {
            AV19groupContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV19groupContacts.Item(AV37GXV1));
         }
         /* 'Send Invitation to Group members' Routine */
         returnInSub = false;
         GXt_char2 = AV10error;
         new GeneXus.Programs.wallet.registered.savevaultgroup(context ).execute(  AV36view.gxTpr_Groupid,  AV19groupContacts, out  GXt_char2) ;
         AV10error = GXt_char2;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            GXt_char2 = AV10error;
            new GeneXus.Programs.wallet.registered.sendvaultinvitations(context ).execute(  AV36view.gxTpr_Groupid, out  GXt_char2) ;
            AV10error = GXt_char2;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10error)) )
         {
            AV6websession.Set("Group_EDIT", "");
            this.executeExternalObjectMethod(sPrefix, false, "GlobalEvents", "ShowMsg", new Object[] {(string)"success",(string)"Group Invitation",(string)"All invitations sent"}, true);
            context.setWebReturnParms(new Object[] {});
            context.setWebReturnParmsMetadata(new Object[] {});
            context.wjLocDisableFrm = 1;
            context.nUserReturn = 1;
            returnInSub = true;
            if (true) return;
         }
         GX_msglist.addItem(AV10error);
         /*  Sending Event outputs  */
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
         PA302( ) ;
         WS302( ) ;
         WE302( ) ;
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
         PA302( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wallet\\registered\\smartgroupencryptedpasswords", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA302( ) ;
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
         PA302( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS302( ) ;
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
         WS302( ) ;
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
         WE302( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202610714151762", true, true, false);
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
         context.AddJavascriptSource("wallet/registered/smartgroupencryptedpasswords.js", "?202610714151763", false, true, false);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true, false);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_62( )
      {
         edtavCtlcontactid1_Internalname = sPrefix+"CTLCONTACTID1_"+sGXsfl_6_idx;
         edtavCtlcontactprivatename_Internalname = sPrefix+"CTLCONTACTPRIVATENAME_"+sGXsfl_6_idx;
         edtavCtlcontactusername_Internalname = sPrefix+"CTLCONTACTUSERNAME_"+sGXsfl_6_idx;
         edtavCtlcontactinvitationsent_Internalname = sPrefix+"CTLCONTACTINVITATIONSENT_"+sGXsfl_6_idx;
         edtavCtlcontactinvitacionaccepted_Internalname = sPrefix+"CTLCONTACTINVITACIONACCEPTED_"+sGXsfl_6_idx;
         edtavRemovecontact_Internalname = sPrefix+"vREMOVECONTACT_"+sGXsfl_6_idx;
      }

      protected void SubsflControlProps_fel_62( )
      {
         edtavCtlcontactid1_Internalname = sPrefix+"CTLCONTACTID1_"+sGXsfl_6_fel_idx;
         edtavCtlcontactprivatename_Internalname = sPrefix+"CTLCONTACTPRIVATENAME_"+sGXsfl_6_fel_idx;
         edtavCtlcontactusername_Internalname = sPrefix+"CTLCONTACTUSERNAME_"+sGXsfl_6_fel_idx;
         edtavCtlcontactinvitationsent_Internalname = sPrefix+"CTLCONTACTINVITATIONSENT_"+sGXsfl_6_fel_idx;
         edtavCtlcontactinvitacionaccepted_Internalname = sPrefix+"CTLCONTACTINVITACIONACCEPTED_"+sGXsfl_6_fel_idx;
         edtavRemovecontact_Internalname = sPrefix+"vREMOVECONTACT_"+sGXsfl_6_fel_idx;
      }

      protected void sendrow_62( )
      {
         sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
         SubsflControlProps_62( ) ;
         WB300( ) ;
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
            if ( ((int)((nGXsfl_6_idx) % (2))) == 0 )
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
            context.WriteHtmlText( " gxrow=\""+sGXsfl_6_idx+"\">") ;
         }
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+"display:none;"+"\">") ;
         }
         /* Single line edit */
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactid1_Internalname,((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV19groupContacts.Item(AV37GXV1)).gxTpr_Contactid.ToString(),((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV19groupContacts.Item(AV37GXV1)).gxTpr_Contactid.ToString(),""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactid1_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtlcontactid1_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)36,(short)0,(short)0,(short)6,(short)0,(short)0,(short)0,(bool)true,(string)"",(string)"",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavCtlcontactprivatename_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'" + sPrefix + "',false,'" + sGXsfl_6_idx + "',6)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactprivatename_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV19groupContacts.Item(AV37GXV1)).gxTpr_Contactprivatename),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,8);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactprivatename_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(int)edtavCtlcontactprivatename_Visible,(int)edtavCtlcontactprivatename_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)6,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavCtlcontactusername_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 9,'" + sPrefix + "',false,'" + sGXsfl_6_idx + "',6)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactusername_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV19groupContacts.Item(AV37GXV1)).gxTpr_Contactusername),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,9);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactusername_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(int)edtavCtlcontactusername_Visible,(int)edtavCtlcontactusername_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)6,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 10,'" + sPrefix + "',false,'" + sGXsfl_6_idx + "',6)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactinvitationsent_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV19groupContacts.Item(AV37GXV1)).gxTpr_Contactinvitationsent, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV19groupContacts.Item(AV37GXV1)).gxTpr_Contactinvitationsent, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onblur(this,10);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactinvitationsent_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlcontactinvitationsent_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)6,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 11,'" + sPrefix + "',false,'" + sGXsfl_6_idx + "',6)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlcontactinvitacionaccepted_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV19groupContacts.Item(AV37GXV1)).gxTpr_Contactinvitacionaccepted, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem)AV19groupContacts.Item(AV37GXV1)).gxTpr_Contactinvitacionaccepted, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onblur(this,11);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlcontactinvitacionaccepted_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlcontactinvitacionaccepted_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)6,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavRemovecontact_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 12,'" + sPrefix + "',false,'" + sGXsfl_6_idx + "',6)\"";
         ROClassString = "Attribute";
         GridcontactsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavRemovecontact_Internalname,StringUtil.RTrim( AV26removeContact),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,12);\"","'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'REMOVE CONTACT\\'."+sGXsfl_6_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavRemovecontact_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(int)edtavRemovecontact_Visible,(int)edtavRemovecontact_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)6,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         send_integrity_lvl_hashes302( ) ;
         GridcontactsContainer.AddRow(GridcontactsRow);
         nGXsfl_6_idx = ((subGridcontacts_Islastpage==1)&&(nGXsfl_6_idx+1>subGridcontacts_fnc_Recordsperpage( )) ? 1 : nGXsfl_6_idx+1);
         sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
         SubsflControlProps_62( ) ;
         /* End function sendrow_62 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl6( )
      {
         if ( GridcontactsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"GridcontactsContainer"+"DivS\" data-gxgridid=\"6\">") ;
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
            GridcontactsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlcontactinvitationsent_Enabled), 5, 0, ".", "")));
            GridcontactsContainer.AddColumnProperties(GridcontactsColumn);
            GridcontactsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridcontactsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlcontactinvitacionaccepted_Enabled), 5, 0, ".", "")));
            GridcontactsContainer.AddColumnProperties(GridcontactsColumn);
            GridcontactsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridcontactsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV26removeContact)));
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
         edtavCtlcontactid1_Internalname = sPrefix+"CTLCONTACTID1";
         edtavCtlcontactprivatename_Internalname = sPrefix+"CTLCONTACTPRIVATENAME";
         edtavCtlcontactusername_Internalname = sPrefix+"CTLCONTACTUSERNAME";
         edtavCtlcontactinvitationsent_Internalname = sPrefix+"CTLCONTACTINVITATIONSENT";
         edtavCtlcontactinvitacionaccepted_Internalname = sPrefix+"CTLCONTACTINVITACIONACCEPTED";
         edtavRemovecontact_Internalname = sPrefix+"vREMOVECONTACT";
         bttAddacontact_Internalname = sPrefix+"ADDACONTACT";
         bttSave_Internalname = sPrefix+"SAVE";
         bttCancel_Internalname = sPrefix+"CANCEL";
         bttSendinvitatiotogroupmembers_Internalname = sPrefix+"SENDINVITATIOTOGROUPMEMBERS";
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
         bttSendinvitatiotogroupmembers_Visible = 1;
         bttCancel_Caption = "Cancel";
         bttSave_Visible = 1;
         bttAddacontact_Visible = 1;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"GRIDCONTACTS_nEOF","type":"int"},{"av":"AV26removeContact","fld":"vREMOVECONTACT","type":"char"},{"av":"AV19groupContacts","fld":"vGROUPCONTACTS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"nRC_GXsfl_6","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":6,"type":"int"},{"av":"sPrefix","type":"char"},{"av":"AV36view","fld":"vVIEW","hsh":true,"type":""}]}""");
         setEventMetadata("'SAVE'","""{"handler":"E12302","iparms":[{"av":"AV36view","fld":"vVIEW","hsh":true,"type":""},{"av":"AV19groupContacts","fld":"vGROUPCONTACTS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_6","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":6,"type":"int"}]}""");
         setEventMetadata("'CANCEL EDIT'","""{"handler":"E13302","iparms":[]}""");
         setEventMetadata("'ADD A CONTACT'","""{"handler":"E11301","iparms":[]}""");
         setEventMetadata("GRIDCONTACTS.LOAD","""{"handler":"E17302","iparms":[{"av":"AV36view","fld":"vVIEW","hsh":true,"type":""},{"av":"AV19groupContacts","fld":"vGROUPCONTACTS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_6","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":6,"type":"int"}]""");
         setEventMetadata("GRIDCONTACTS.LOAD",""","oparms":[{"ctrl":"CTLCONTACTPRIVATENAME","prop":"Visible"},{"ctrl":"CTLCONTACTUSERNAME","prop":"Visible"},{"av":"edtavRemovecontact_Visible","ctrl":"vREMOVECONTACT","prop":"Visible"}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED","""{"handler":"E14302","iparms":[{"av":"AV25PopupName","fld":"vPOPUPNAME","type":"char"},{"av":"AV19groupContacts","fld":"vGROUPCONTACTS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_6","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":6,"type":"int"},{"av":"GRIDCONTACTS_nEOF","type":"int"},{"av":"AV26removeContact","fld":"vREMOVECONTACT","type":"char"},{"av":"sPrefix","type":"char"},{"av":"AV36view","fld":"vVIEW","hsh":true,"type":""}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",""","oparms":[{"av":"AV26removeContact","fld":"vREMOVECONTACT","type":"char"},{"av":"AV19groupContacts","fld":"vGROUPCONTACTS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_6","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":6,"type":"int"}]}""");
         setEventMetadata("'REMOVE CONTACT'","""{"handler":"E18302","iparms":[{"av":"AV19groupContacts","fld":"vGROUPCONTACTS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_6","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":6,"type":"int"},{"av":"GRIDCONTACTS_nEOF","type":"int"},{"av":"AV26removeContact","fld":"vREMOVECONTACT","type":"char"},{"av":"sPrefix","type":"char"},{"av":"AV36view","fld":"vVIEW","hsh":true,"type":""}]""");
         setEventMetadata("'REMOVE CONTACT'",""","oparms":[{"av":"AV19groupContacts","fld":"vGROUPCONTACTS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_6","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":6,"type":"int"}]}""");
         setEventMetadata("'SEND INVITATION TO GROUP MEMBERS'","""{"handler":"E15302","iparms":[{"av":"AV36view","fld":"vVIEW","hsh":true,"type":""},{"av":"AV19groupContacts","fld":"vGROUPCONTACTS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"GRIDCONTACTS_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_6","ctrl":"GRIDCONTACTS","prop":"GridRC","grid":6,"type":"int"}]}""");
         setEventMetadata("VALIDV_GXV2","""{"handler":"Validv_Gxv2","iparms":[]}""");
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
         AV26removeContact = "";
         AV36view = new GeneXus.Programs.wallet.registered.SdtWalletBackupView(context);
         AV19groupContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem>( context, "WalletBackupView.ContactItem", "distributedcryptography");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV25PopupName = "";
         GX_FocusControl = "";
         GridcontactsContainer = new GXWebGrid( context);
         sStyleString = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttAddacontact_Jsonclick = "";
         bttSave_Jsonclick = "";
         bttCancel_Jsonclick = "";
         bttSendinvitatiotogroupmembers_Jsonclick = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV35groupView = new GeneXus.Programs.wallet.registered.SdtGroupListItem(context);
         GXt_SdtGroupListItem1 = new GeneXus.Programs.wallet.registered.SdtGroupListItem(context);
         AV10error = "";
         AV17groupContact = new GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem(context);
         AV6websession = context.GetSession();
         GridcontactsRow = new GXWebRow();
         AV11expectedPopupName = "";
         AV9contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV24oneGroupContact = new GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem(context);
         AV18groupContactAdd = new GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem(context);
         GXt_char2 = "";
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
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGridcontacts_Backcolorstyle ;
      private short AV34strFound ;
      private short GRIDCONTACTS_nEOF ;
      private short nGXWrapped ;
      private short subGridcontacts_Backstyle ;
      private short subGridcontacts_Titlebackstyle ;
      private short subGridcontacts_Allowselection ;
      private short subGridcontacts_Allowhovering ;
      private short subGridcontacts_Allowcollapsing ;
      private short subGridcontacts_Collapsed ;
      private int nRC_GXsfl_6 ;
      private int nGXsfl_6_idx=1 ;
      private int edtavCtlcontactid1_Enabled ;
      private int edtavCtlcontactprivatename_Enabled ;
      private int edtavCtlcontactusername_Enabled ;
      private int edtavCtlcontactinvitationsent_Enabled ;
      private int edtavCtlcontactinvitacionaccepted_Enabled ;
      private int edtavRemovecontact_Enabled ;
      private int AV37GXV1 ;
      private int bttAddacontact_Visible ;
      private int bttSave_Visible ;
      private int bttSendinvitatiotogroupmembers_Visible ;
      private int subGridcontacts_Islastpage ;
      private int nGXsfl_6_fel_idx=1 ;
      private int AV43GXV7 ;
      private int edtavCtlcontactprivatename_Visible ;
      private int edtavCtlcontactusername_Visible ;
      private int edtavRemovecontact_Visible ;
      private int AV44GXV8 ;
      private int nGXsfl_6_bak_idx=1 ;
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
      private string sGXsfl_6_idx="0001" ;
      private string AV26removeContact ;
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
      private string AV25PopupName ;
      private string GX_FocusControl ;
      private string divMaintable_Internalname ;
      private string sStyleString ;
      private string subGridcontacts_Internalname ;
      private string TempTags ;
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
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string sGXsfl_6_fel_idx="0001" ;
      private string AV10error ;
      private string AV11expectedPopupName ;
      private string GXt_char2 ;
      private string subGridcontacts_Class ;
      private string subGridcontacts_Linesclass ;
      private string ROClassString ;
      private string edtavCtlcontactid1_Jsonclick ;
      private string edtavCtlcontactprivatename_Jsonclick ;
      private string edtavCtlcontactusername_Jsonclick ;
      private string edtavCtlcontactinvitationsent_Jsonclick ;
      private string edtavCtlcontactinvitacionaccepted_Jsonclick ;
      private string edtavRemovecontact_Jsonclick ;
      private string subGridcontacts_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_6_Refreshing=false ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV6 ;
      private bool AV13found ;
      private GXWebGrid GridcontactsContainer ;
      private GXWebRow GridcontactsRow ;
      private GXWebColumn GridcontactsColumn ;
      private GXWebForm Form ;
      private IGxSession AV6websession ;
      private IGxDataStore dsDefault ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView AV36view ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem> AV19groupContacts ;
      private GeneXus.Programs.wallet.registered.SdtGroupListItem AV35groupView ;
      private GeneXus.Programs.wallet.registered.SdtGroupListItem GXt_SdtGroupListItem1 ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem AV17groupContact ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV9contact ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem AV24oneGroupContact ;
      private GeneXus.Programs.wallet.registered.SdtWalletBackupView_ContactItem AV18groupContactAdd ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
