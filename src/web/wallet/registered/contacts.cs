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
   public class contacts : GXDataArea
   {
      public contacts( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public contacts( IGxContext context )
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

      protected override void createObjects( )
      {
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
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
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Contactgrid") == 0 )
            {
               gxnrContactgrid_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Contactgrid") == 0 )
            {
               gxgrContactgrid_refresh_invoke( ) ;
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
         if ( ! context.IsLocalStorageSupported( ) )
         {
            context.PushCurrentUrl();
         }
      }

      protected void gxnrContactgrid_newrow_invoke( )
      {
         nRC_GXsfl_11 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_11"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_11_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_11_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_11_idx = GetPar( "sGXsfl_11_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrContactgrid_newrow( ) ;
         /* End function gxnrContactgrid_newrow_invoke */
      }

      protected void gxgrContactgrid_refresh_invoke( )
      {
         ajax_req_read_hidden_sdt(GetNextPar( ), AV17allContacts);
         ajax_req_read_hidden_sdt(GetNextPar( ), AV27externalUser);
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrContactgrid_refresh( AV17allContacts, AV27externalUser) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrContactgrid_refresh_invoke */
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            MasterPageObj = (GXMasterPage) ClassLoader.GetInstance("general.ui.masterunanimosidebar", "GeneXus.Programs.general.ui.masterunanimosidebar", new Object[] {context});
            MasterPageObj.setDataArea(this,false);
            ValidateSpaRequest();
            MasterPageObj.webExecute();
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

      public override short ExecuteStartEvent( )
      {
         PA162( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START162( ) ;
         }
         return gxajaxcallmode ;
      }

      public override void RenderHtmlHeaders( )
      {
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, true);
      }

      public override void RenderHtmlOpenForm( )
      {
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.WriteHtmlText( "<title>") ;
         context.SendWebValue( Form.Caption) ;
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
         if ( nGXWrapped != 1 )
         {
            MasterPageObj.master_styles();
         }
         CloseStyles();
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
         context.WriteHtmlText( Form.Headerrawhtml) ;
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
         bodyStyle = "" + "background-color:" + context.BuildHTMLColor( Form.Backcolor) + ";color:" + context.BuildHTMLColor( Form.Textcolor) + ";";
         if ( nGXWrapped == 0 )
         {
            bodyStyle += "-moz-opacity:0;opacity:0;";
         }
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( Form.Background)) ) )
         {
            bodyStyle += " background-image:url(" + context.convertURL( Form.Background) + ")";
         }
         context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
         context.WriteHtmlText( FormProcess+">") ;
         context.skipLines(1);
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.contacts") +"\">") ;
         GxWebStd.gx_hidden_field( context, "_EventName", "");
         GxWebStd.gx_hidden_field( context, "_EventGridId", "");
         GxWebStd.gx_hidden_field( context, "_EventRowId", "");
         context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
         AssignProp("", false, "FORM", "Class", "form-horizontal Form", true);
         toggleJsOutput = isJsOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vEXTERNALUSER", AV27externalUser);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vEXTERNALUSER", AV27externalUser);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vEXTERNALUSER", GetSecureSignedToken( "", AV27externalUser, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Allcontacts", AV17allContacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Allcontacts", AV17allContacts);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_11", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_11), 8, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "vPOPUPNAME", StringUtil.RTrim( AV10PopupName));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vSDT_MESSAGE", AV26sdt_message);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vSDT_MESSAGE", AV26sdt_message);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vEXTERNALUSER", AV27externalUser);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vEXTERNALUSER", AV27externalUser);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vEXTERNALUSER", GetSecureSignedToken( "", AV27externalUser, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vMESSAGE_SIGNATURE", AV28message_signature);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vMESSAGE_SIGNATURE", AV28message_signature);
         }
         GxWebStd.gx_hidden_field( context, "vERROR", StringUtil.RTrim( AV8error));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vALLCONTACTS", AV17allContacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vALLCONTACTS", AV17allContacts);
         }
         GxWebStd.gx_boolean_hidden_field( context, "vUSERRESPONSE", AV20UserResponse);
         GxWebStd.gx_hidden_field( context, "vSELECTEDCLIENTID", AV21selectedClientId.ToString());
      }

      public override void RenderHtmlCloseForm( )
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
         context.WriteHtmlTextNl( "</form>") ;
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         include_jscripts( ) ;
      }

      public override void RenderHtmlContent( )
      {
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gx-ct-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            WE162( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT162( ) ;
      }

      public override bool HasEnterEvent( )
      {
         return false ;
      }

      public override GXWebForm GetForm( )
      {
         return Form ;
      }

      public override string GetSelfLink( )
      {
         return formatLink("wallet.registered.contacts")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.registered.Contacts" ;
      }

      public override string GetPgmdesc( )
      {
         return "Contacts" ;
      }

      protected void WB160( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            if ( nGXWrapped == 1 )
            {
               RenderHtmlHeaders( ) ;
               RenderHtmlOpenForm( ) ;
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", "", "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 6,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttRecovercontactsfromserver_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(11), 2, 0)+","+"null"+");", "Recover contacts from server", bttRecovercontactsfromserver_Jsonclick, 5, "Recover contacts from server", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'RECOVER CONTACTS FROM SERVER\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/Contacts.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCreatenewcontact_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(11), 2, 0)+","+"null"+");", "Create new contact", bttCreatenewcontact_Jsonclick, 7, "Create new contact", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"e11161_client"+"'", TempTags, "", 2, "HLP_Wallet/registered/Contacts.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            ContactgridContainer.SetWrapped(nGXWrapped);
            StartGridControl11( ) ;
         }
         if ( wbEnd == 11 )
         {
            wbEnd = 0;
            nRC_GXsfl_11 = (int)(nGXsfl_11_idx-1);
            if ( ContactgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV39GXV1 = nGXsfl_11_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"ContactgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Contactgrid", ContactgridContainer, subContactgrid_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "ContactgridContainerData", ContactgridContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "ContactgridContainerData"+"V", ContactgridContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"ContactgridContainerData"+"V"+"\" value='"+ContactgridContainer.GridValuesHidden()+"'/>") ;
               }
            }
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
               if ( ContactgridContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV39GXV1 = nGXsfl_11_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"ContactgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Contactgrid", ContactgridContainer, subContactgrid_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "ContactgridContainerData", ContactgridContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "ContactgridContainerData"+"V", ContactgridContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"ContactgridContainerData"+"V"+"\" value='"+ContactgridContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START162( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_13-186702", 0) ;
            }
         }
         Form.Meta.addItem("description", "Contacts", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP160( ) ;
      }

      protected void WS162( )
      {
         START162( ) ;
         EVT162( ) ;
      }

      protected void EVT162( )
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
                  if ( StringUtil.StrCmp(sEvtType, "M") != 0 )
                  {
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
                           else if ( StringUtil.StrCmp(sEvt, "'RECOVER CONTACTS FROM SERVER'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Recover contacts from server' */
                              E12162 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E13162 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E14162 ();
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
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 16), "CONTACTGRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 19), "'ACCEPT INVITATION'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 20), "'DECLINE INVITATION'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 16), "'DELETE CONTACT'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "'CHAT'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "'EDIT CONTACT'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "'EDIT CONTACT'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 16), "'DELETE CONTACT'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 19), "'ACCEPT INVITATION'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 20), "'DECLINE INVITATION'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "'CHAT'") == 0 ) )
                           {
                              nGXsfl_11_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
                              SubsflControlProps_112( ) ;
                              AV39GXV1 = nGXsfl_11_idx;
                              if ( ( AV17allContacts.Count >= AV39GXV1 ) && ( AV39GXV1 > 0 ) )
                              {
                                 AV17allContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV39GXV1));
                                 AV37editImage = cgiGet( edtavEditimage_Internalname);
                                 AssignProp("", false, edtavEditimage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV37editImage)) ? AV47Editimage_GXI : context.convertURL( context.PathToRelativeUrl( AV37editImage))), !bGXsfl_11_Refreshing);
                                 AssignProp("", false, edtavEditimage_Internalname, "SrcSet", context.GetImageSrcSet( AV37editImage), true);
                                 AV36deleteImage = cgiGet( edtavDeleteimage_Internalname);
                                 AssignProp("", false, edtavDeleteimage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV36deleteImage)) ? AV46Deleteimage_GXI : context.convertURL( context.PathToRelativeUrl( AV36deleteImage))), !bGXsfl_11_Refreshing);
                                 AssignProp("", false, edtavDeleteimage_Internalname, "SrcSet", context.GetImageSrcSet( AV36deleteImage), true);
                                 AV29acceptInvitation = cgiGet( edtavAcceptinvitation_Internalname);
                                 AssignAttri("", false, edtavAcceptinvitation_Internalname, AV29acceptInvitation);
                                 AV32declineInvitation = cgiGet( edtavDeclineinvitation_Internalname);
                                 AssignAttri("", false, edtavDeclineinvitation_Internalname, AV32declineInvitation);
                                 AV38chatImage = cgiGet( edtavChatimage_Internalname);
                                 AssignProp("", false, edtavChatimage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV38chatImage)) ? AV48Chatimage_GXI : context.convertURL( context.PathToRelativeUrl( AV38chatImage))), !bGXsfl_11_Refreshing);
                                 AssignProp("", false, edtavChatimage_Internalname, "SrcSet", context.GetImageSrcSet( AV38chatImage), true);
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E15162 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "CONTACTGRID.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Contactgrid.Load */
                                    E16162 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'ACCEPT INVITATION'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Accept Invitation' */
                                    E17162 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'DECLINE INVITATION'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Decline Invitation' */
                                    E18162 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'DELETE CONTACT'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Delete Contact' */
                                    E19162 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'CHAT'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Chat' */
                                    E20162 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'EDIT CONTACT'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Edit Contact' */
                                    E21162 ();
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

      protected void WE162( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               if ( nGXWrapped == 1 )
               {
                  RenderHtmlCloseForm( ) ;
               }
            }
         }
      }

      protected void PA162( )
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

      protected void gxnrContactgrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_112( ) ;
         while ( nGXsfl_11_idx <= nRC_GXsfl_11 )
         {
            sendrow_112( ) ;
            nGXsfl_11_idx = ((subContactgrid_Islastpage==1)&&(nGXsfl_11_idx+1>subContactgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_idx+1);
            sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
            SubsflControlProps_112( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( ContactgridContainer)) ;
         /* End function gxnrContactgrid_newrow */
      }

      protected void gxgrContactgrid_refresh( GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV17allContacts ,
                                              GeneXus.Programs.distcrypt.SdtExternalUser AV27externalUser )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         CONTACTGRID_nCurrentRecord = 0;
         RF162( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrContactgrid_refresh */
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
         RF162( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtluserprivatename1_Enabled = 0;
         edtavCtlusername1_Enabled = 0;
         edtavCtlinvitationsent_Enabled = 0;
         edtavCtlinvitacionaccepted_Enabled = 0;
         edtavAcceptinvitation_Enabled = 0;
         edtavDeclineinvitation_Enabled = 0;
      }

      protected void RF162( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            ContactgridContainer.ClearRows();
         }
         wbStart = 11;
         nGXsfl_11_idx = 1;
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
         bGXsfl_11_Refreshing = true;
         ContactgridContainer.AddObjectProperty("GridName", "Contactgrid");
         ContactgridContainer.AddObjectProperty("CmpContext", "");
         ContactgridContainer.AddObjectProperty("InMasterPage", "false");
         ContactgridContainer.AddObjectProperty("Class", "Grid");
         ContactgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         ContactgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         ContactgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subContactgrid_Backcolorstyle), 1, 0, ".", "")));
         ContactgridContainer.PageSize = subContactgrid_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_112( ) ;
            /* Execute user event: Contactgrid.Load */
            E16162 ();
            wbEnd = 11;
            WB160( ) ;
         }
         bGXsfl_11_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes162( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vEXTERNALUSER", AV27externalUser);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vEXTERNALUSER", AV27externalUser);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vEXTERNALUSER", GetSecureSignedToken( "", AV27externalUser, context));
      }

      protected int subContactgrid_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subContactgrid_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subContactgrid_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subContactgrid_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtluserprivatename1_Enabled = 0;
         edtavCtlusername1_Enabled = 0;
         edtavCtlinvitationsent_Enabled = 0;
         edtavCtlinvitacionaccepted_Enabled = 0;
         edtavAcceptinvitation_Enabled = 0;
         edtavDeclineinvitation_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP160( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E15162 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "Allcontacts"), AV17allContacts);
            ajax_req_read_hidden_sdt(cgiGet( "vALLCONTACTS"), AV17allContacts);
            /* Read saved values. */
            nRC_GXsfl_11 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_11"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_11 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_11"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_11_fel_idx = 0;
            while ( nGXsfl_11_fel_idx < nRC_GXsfl_11 )
            {
               nGXsfl_11_fel_idx = ((subContactgrid_Islastpage==1)&&(nGXsfl_11_fel_idx+1>subContactgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_fel_idx+1);
               sGXsfl_11_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_112( ) ;
               AV39GXV1 = nGXsfl_11_fel_idx;
               if ( ( AV17allContacts.Count >= AV39GXV1 ) && ( AV39GXV1 > 0 ) )
               {
                  AV17allContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV39GXV1));
                  AV37editImage = cgiGet( edtavEditimage_Internalname);
                  AV36deleteImage = cgiGet( edtavDeleteimage_Internalname);
                  AV29acceptInvitation = cgiGet( edtavAcceptinvitation_Internalname);
                  AV32declineInvitation = cgiGet( edtavDeclineinvitation_Internalname);
                  AV38chatImage = cgiGet( edtavChatimage_Internalname);
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
         E15162 ();
         if (returnInSub) return;
      }

      protected void E15162( )
      {
         /* Start Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.delcontact(context ).execute( ) ;
         /* Execute user subroutine: 'INIT CONACTS FROM FILE' */
         S112 ();
         if (returnInSub) return;
         GXt_SdtExternalUser1 = AV27externalUser;
         new GeneXus.Programs.distcrypt.getexternaluser(context ).execute( out  GXt_SdtExternalUser1) ;
         AV27externalUser = GXt_SdtExternalUser1;
         AV13websession.Set("Contact_SDT_ACCEPT", "");
         AV13websession.Set("Contact_SDT", "");
      }

      protected void E12162( )
      {
         AV39GXV1 = nGXsfl_11_idx;
         if ( ( AV39GXV1 > 0 ) && ( AV17allContacts.Count >= AV39GXV1 ) )
         {
            AV17allContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV39GXV1));
         }
         /* 'Recover contacts from server' Routine */
         returnInSub = false;
         GXt_char2 = AV8error;
         new GeneXus.Programs.wallet.registered.getcontacts(context ).execute( out  AV17allContacts, out  GXt_char2) ;
         gx_BV11 = true;
         AV8error = GXt_char2;
         AssignAttri("", false, "AV8error", AV8error);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
         {
            GXt_char2 = AV8error;
            new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "contacts.enc",  AV17allContacts.ToJSonString(false), out  GXt_char2) ;
            AV8error = GXt_char2;
            AssignAttri("", false, "AV8error", AV8error);
         }
         else
         {
            GX_msglist.addItem(AV8error);
         }
         /*  Sending Event outputs  */
         if ( gx_BV11 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV17allContacts", AV17allContacts);
            nGXsfl_11_bak_idx = nGXsfl_11_idx;
            gxgrContactgrid_refresh( AV17allContacts, AV27externalUser) ;
            nGXsfl_11_idx = nGXsfl_11_bak_idx;
            sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
            SubsflControlProps_112( ) ;
         }
      }

      protected void E13162( )
      {
         AV39GXV1 = nGXsfl_11_idx;
         if ( ( AV39GXV1 > 0 ) && ( AV17allContacts.Count >= AV39GXV1 ) )
         {
            AV17allContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV39GXV1));
         }
         /* Extensions\Web\Popup_Onpopupclosed Routine */
         returnInSub = false;
         AV11expectedPopupName = "Wallet.registered.Contact";
         AV12strFound = (short)(StringUtil.StringSearch( AV10PopupName, StringUtil.Trim( StringUtil.Lower( AV11expectedPopupName)), 1));
         if ( AV12strFound > 0 )
         {
            AV24contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
            AV24contact.FromJSonString(AV13websession.Get("Contact_SDT"), null);
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV24contact.gxTpr_Userpubkey)) )
            {
               if ( AV24contact.gxTpr_Sent )
               {
                  AV24contact.gxTpr_Invitationsent = DateTimeUtil.Now( context);
                  GXt_char2 = AV8error;
                  new GeneXus.Programs.wallet.registered.createcontact(context ).execute(  AV24contact, out  AV22ContactId, out  GXt_char2) ;
                  AV8error = GXt_char2;
                  AssignAttri("", false, "AV8error", AV8error);
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                  {
                     AV26sdt_message.gxTpr_Id = Guid.NewGuid( );
                     GXt_int3 = 0;
                     new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int3) ;
                     AV26sdt_message.gxTpr_Datetimeunix = GXt_int3;
                     AV26sdt_message.gxTpr_Messagetype = 30;
                     AV28message_signature.gxTpr_Username = StringUtil.Trim( AV27externalUser.gxTpr_Userinfo.gxTpr_Username);
                     AV28message_signature.gxTpr_Pubkey = StringUtil.Trim( AV27externalUser.gxTpr_Keyinfo.gxTpr_Publickey);
                     AV28message_signature.gxTpr_Newpubkey = StringUtil.Trim( AV27externalUser.gxTpr_Chatkeyinfo.gxTpr_Publickey);
                     AV28message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV27externalUser.gxTpr_Groupskeyinfo.gxTpr_Publickey);
                     GXt_char2 = AV8error;
                     GXt_char4 = AV28message_signature.gxTpr_Signature;
                     new GeneXus.Programs.nbitcoin.eccsignmsg(context ).execute(  AV27externalUser.gxTpr_Keyinfo.gxTpr_Privatekey,  StringUtil.Trim( AV28message_signature.gxTpr_Username)+StringUtil.Trim( AV28message_signature.gxTpr_Pubkey), out  GXt_char4, out  GXt_char2) ;
                     AV28message_signature.gxTpr_Signature = GXt_char4;
                     AV8error = GXt_char2;
                     AssignAttri("", false, "AV8error", AV8error);
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                     {
                        AV26sdt_message.gxTpr_Message = AV28message_signature.ToJSonString(false, true);
                        GXt_char4 = AV8error;
                        new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV24contact,  AV26sdt_message, out  GXt_char4) ;
                        AV8error = GXt_char4;
                        AssignAttri("", false, "AV8error", AV8error);
                        if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                        {
                           AV24contact.gxTpr_Invitationsent = DateTimeUtil.Now( context);
                           AV24contact.gxTpr_Contactrid = AV22ContactId;
                           AV17allContacts.Clear();
                           gx_BV11 = true;
                           AV17allContacts.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV8error), null);
                           gx_BV11 = true;
                           AV17allContacts.Add(AV24contact, 0);
                           gx_BV11 = true;
                           GXt_char4 = AV8error;
                           new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "contacts.enc",  AV17allContacts.ToJSonString(false), out  GXt_char4) ;
                           AV8error = GXt_char4;
                           AssignAttri("", false, "AV8error", AV8error);
                           /* Execute user subroutine: 'INIT CONACTS FROM FILE' */
                           S112 ();
                           if (returnInSub) return;
                        }
                        else
                        {
                           GX_msglist.addItem("There was a problem sending the Invitation to the Contact: "+AV8error);
                        }
                     }
                     else
                     {
                        GX_msglist.addItem("There was a problem Signing the invitation: "+AV8error);
                     }
                  }
                  else
                  {
                     GX_msglist.addItem("There was a problem saving the encrypted Contac information at the server: "+AV8error);
                  }
               }
               else
               {
                  if ( AV24contact.gxTpr_Received )
                  {
                     GXt_char4 = AV8error;
                     new GeneXus.Programs.wallet.registered.createcontact(context ).execute(  AV24contact, out  AV22ContactId, out  GXt_char4) ;
                     AV8error = GXt_char4;
                     AssignAttri("", false, "AV8error", AV8error);
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                     {
                        AV26sdt_message.gxTpr_Id = Guid.NewGuid( );
                        GXt_int3 = 0;
                        new GeneXus.Programs.distributedcrypto.getunixtimemilisecondsutc(context ).execute( out  GXt_int3) ;
                        AV26sdt_message.gxTpr_Datetimeunix = GXt_int3;
                        AV26sdt_message.gxTpr_Messagetype = 40;
                        AV28message_signature.gxTpr_Username = StringUtil.Trim( AV27externalUser.gxTpr_Userinfo.gxTpr_Username);
                        AV28message_signature.gxTpr_Pubkey = StringUtil.Trim( AV27externalUser.gxTpr_Keyinfo.gxTpr_Publickey);
                        AV28message_signature.gxTpr_Newpubkey = StringUtil.Trim( AV27externalUser.gxTpr_Chatkeyinfo.gxTpr_Publickey);
                        AV28message_signature.gxTpr_Grouppubkey = StringUtil.Trim( AV27externalUser.gxTpr_Groupskeyinfo.gxTpr_Publickey);
                        GXt_char4 = AV8error;
                        GXt_char2 = AV28message_signature.gxTpr_Signature;
                        new GeneXus.Programs.nbitcoin.eccsignmsg(context ).execute(  AV27externalUser.gxTpr_Keyinfo.gxTpr_Privatekey,  StringUtil.Trim( AV28message_signature.gxTpr_Username)+StringUtil.Trim( AV28message_signature.gxTpr_Pubkey), out  GXt_char2, out  GXt_char4) ;
                        AV28message_signature.gxTpr_Signature = GXt_char2;
                        AV8error = GXt_char4;
                        AssignAttri("", false, "AV8error", AV8error);
                        if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                        {
                           AV26sdt_message.gxTpr_Message = AV28message_signature.ToJSonString(false, true);
                           GXt_char4 = AV8error;
                           new GeneXus.Programs.wallet.registered.sendmessage(context ).execute(  AV24contact,  AV26sdt_message, out  GXt_char4) ;
                           AV8error = GXt_char4;
                           AssignAttri("", false, "AV8error", AV8error);
                           if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                           {
                              AV24contact.gxTpr_Invitacionaccepted = DateTimeUtil.Now( context);
                              AV24contact.gxTpr_Contactrid = AV22ContactId;
                              GXt_char4 = AV8error;
                              new GeneXus.Programs.wallet.registered.updatecontact(context ).execute(  AV24contact,  AV24contact.gxTpr_Contactrid, out  GXt_char4) ;
                              AV8error = GXt_char4;
                              AssignAttri("", false, "AV8error", AV8error);
                              if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                              {
                                 AV17allContacts.Clear();
                                 gx_BV11 = true;
                                 AV17allContacts.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV8error), null);
                                 gx_BV11 = true;
                                 AV44GXV6 = 1;
                                 while ( AV44GXV6 <= AV17allContacts.Count )
                                 {
                                    AV30contact_temp = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV44GXV6));
                                    if ( StringUtil.StrCmp(StringUtil.Trim( AV30contact_temp.gxTpr_Username), StringUtil.Trim( AV24contact.gxTpr_Username)) == 0 )
                                    {
                                       AV17allContacts.RemoveItem(AV17allContacts.IndexOf(AV30contact_temp));
                                       gx_BV11 = true;
                                       if (true) break;
                                    }
                                    AV44GXV6 = (int)(AV44GXV6+1);
                                 }
                                 AV17allContacts.Add(AV24contact, 0);
                                 gx_BV11 = true;
                                 GXt_char4 = AV8error;
                                 new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "contacts.enc",  AV17allContacts.ToJSonString(false), out  GXt_char4) ;
                                 AV8error = GXt_char4;
                                 AssignAttri("", false, "AV8error", AV8error);
                                 /* Execute user subroutine: 'INIT CONACTS FROM FILE' */
                                 S112 ();
                                 if (returnInSub) return;
                              }
                              else
                              {
                                 GX_msglist.addItem("There was a problem updating the confirmation on the server: "+AV8error);
                              }
                           }
                           else
                           {
                              GX_msglist.addItem("There was a problem sending the confirmation to the Contact: "+AV8error);
                           }
                        }
                        else
                        {
                           GX_msglist.addItem("There was a problem Signing the response: "+AV8error);
                        }
                     }
                     else
                     {
                        GX_msglist.addItem("There was a problem saving the encrypted Contac information at the server: "+AV8error);
                     }
                  }
                  else
                  {
                     if ( AV24contact.gxTpr_Edited )
                     {
                        GXt_char4 = AV8error;
                        new GeneXus.Programs.wallet.registered.updatecontact(context ).execute(  AV24contact,  AV24contact.gxTpr_Contactrid, out  GXt_char4) ;
                        AV8error = GXt_char4;
                        AssignAttri("", false, "AV8error", AV8error);
                        if ( String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
                        {
                           AV17allContacts.Clear();
                           gx_BV11 = true;
                           AV17allContacts.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV8error), null);
                           gx_BV11 = true;
                           AV45GXV7 = 1;
                           while ( AV45GXV7 <= AV17allContacts.Count )
                           {
                              AV30contact_temp = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV45GXV7));
                              if ( StringUtil.StrCmp(StringUtil.Trim( AV30contact_temp.gxTpr_Username), StringUtil.Trim( AV24contact.gxTpr_Username)) == 0 )
                              {
                                 AV17allContacts.RemoveItem(AV17allContacts.IndexOf(AV30contact_temp));
                                 gx_BV11 = true;
                                 if (true) break;
                              }
                              AV45GXV7 = (int)(AV45GXV7+1);
                           }
                           AV17allContacts.Add(AV24contact, 0);
                           gx_BV11 = true;
                           GXt_char4 = AV8error;
                           new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "contacts.enc",  AV17allContacts.ToJSonString(false), out  GXt_char4) ;
                           AV8error = GXt_char4;
                           AssignAttri("", false, "AV8error", AV8error);
                           /* Execute user subroutine: 'INIT CONACTS FROM FILE' */
                           S112 ();
                           if (returnInSub) return;
                        }
                        else
                        {
                           GX_msglist.addItem("There was a problem updating the Contact on the server: "+AV8error);
                        }
                     }
                     else
                     {
                        AV8error = "We could not stablish a flow for the Contact";
                        AssignAttri("", false, "AV8error", AV8error);
                        GX_msglist.addItem(AV8error);
                     }
                  }
               }
            }
            AV13websession.Set("Contact_SDT", "");
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV26sdt_message", AV26sdt_message);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV28message_signature", AV28message_signature);
         if ( gx_BV11 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV17allContacts", AV17allContacts);
            nGXsfl_11_bak_idx = nGXsfl_11_idx;
            gxgrContactgrid_refresh( AV17allContacts, AV27externalUser) ;
            nGXsfl_11_idx = nGXsfl_11_bak_idx;
            sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
            SubsflControlProps_112( ) ;
         }
      }

      private void E16162( )
      {
         /* Contactgrid_Load Routine */
         returnInSub = false;
         AV39GXV1 = 1;
         while ( AV39GXV1 <= AV17allContacts.Count )
         {
            AV17allContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV39GXV1));
            edtavDeleteimage_gximage = "GeneXusUnanimo_delete_light";
            AV36deleteImage = context.GetImagePath( "db0f63cd-dde8-4bf7-aca2-01cdf8d3c157", "", context.GetTheme( ));
            AssignAttri("", false, edtavDeleteimage_Internalname, AV36deleteImage);
            AV46Deleteimage_GXI = GXDbFile.PathToUrl( context.GetImagePath( "db0f63cd-dde8-4bf7-aca2-01cdf8d3c157", "", context.GetTheme( )), context);
            edtavEditimage_gximage = "GeneXusUnanimo_edit_light";
            AV37editImage = context.GetImagePath( "f20f0a60-bf65-4ff0-a2d0-9392c759340b", "", context.GetTheme( ));
            AssignAttri("", false, edtavEditimage_Internalname, AV37editImage);
            AV47Editimage_GXI = GXDbFile.PathToUrl( context.GetImagePath( "f20f0a60-bf65-4ff0-a2d0-9392c759340b", "", context.GetTheme( )), context);
            edtavChatimage_gximage = "Wallet_chat";
            AV38chatImage = context.GetImagePath( "557b71c6-098f-4b1d-8af5-8e521c12d6cc", "", context.GetTheme( ));
            AssignAttri("", false, edtavChatimage_Internalname, AV38chatImage);
            AV48Chatimage_GXI = GXDbFile.PathToUrl( context.GetImagePath( "557b71c6-098f-4b1d-8af5-8e521c12d6cc", "", context.GetTheme( )), context);
            AV29acceptInvitation = "Accept Invitation";
            AssignAttri("", false, edtavAcceptinvitation_Internalname, AV29acceptInvitation);
            AV32declineInvitation = "Decline Invitation";
            AssignAttri("", false, edtavDeclineinvitation_Internalname, AV32declineInvitation);
            if ( (Guid.Empty==((GeneXus.Programs.wallet.registered.SdtContact_SDT)(AV17allContacts.CurrentItem)).gxTpr_Contactrid) )
            {
               edtavAcceptinvitation_Visible = 1;
               edtavDeclineinvitation_Visible = 1;
               edtavChatimage_Visible = 0;
               edtavDeleteimage_Visible = 0;
               edtavEditimage_Visible = 0;
            }
            else
            {
               edtavAcceptinvitation_Visible = 0;
               edtavDeclineinvitation_Visible = 0;
               edtavChatimage_Visible = 1;
               edtavDeleteimage_Visible = 1;
               edtavEditimage_Visible = 1;
            }
            if ( (DateTime.MinValue==((GeneXus.Programs.wallet.registered.SdtContact_SDT)(AV17allContacts.CurrentItem)).gxTpr_Invitacionaccepted) )
            {
               edtavChatimage_Visible = 0;
            }
            else
            {
               edtavChatimage_Visible = 1;
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 11;
            }
            sendrow_112( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_11_Refreshing )
            {
               DoAjaxLoad(11, ContactgridRow);
            }
            AV39GXV1 = (int)(AV39GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E17162( )
      {
         AV39GXV1 = nGXsfl_11_idx;
         if ( ( AV39GXV1 > 0 ) && ( AV17allContacts.Count >= AV39GXV1 ) )
         {
            AV17allContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV39GXV1));
         }
         /* 'Accept Invitation' Routine */
         returnInSub = false;
         AV13websession.Set("Contact_SDT_ACCEPT", ((GeneXus.Programs.wallet.registered.SdtContact_SDT)(AV17allContacts.CurrentItem)).ToJSonString(false, true));
         context.PopUp(formatLink("wallet.registered.contact") , new Object[] {});
         /*  Sending Event outputs  */
      }

      protected void E18162( )
      {
         AV39GXV1 = nGXsfl_11_idx;
         if ( ( AV39GXV1 > 0 ) && ( AV17allContacts.Count >= AV39GXV1 ) )
         {
            AV17allContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV39GXV1));
         }
         /* 'Decline Invitation' Routine */
         returnInSub = false;
         AV34UserName = StringUtil.Trim( ((GeneXus.Programs.wallet.registered.SdtContact_SDT)(AV17allContacts.CurrentItem)).gxTpr_Username);
         AV17allContacts.Clear();
         gx_BV11 = true;
         AV17allContacts.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV8error), null);
         gx_BV11 = true;
         AV49GXV8 = 1;
         while ( AV49GXV8 <= AV17allContacts.Count )
         {
            AV24contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV49GXV8));
            AV30contact_temp = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
            if ( StringUtil.StrCmp(AV24contact.gxTpr_Username, AV34UserName) == 0 )
            {
               AV30contact_temp = AV24contact;
               AV30contact_temp.gxTpr_Invitationdeclined = true;
               AV17allContacts.RemoveItem(AV17allContacts.IndexOf(AV24contact));
               gx_BV11 = true;
               AV30contact_temp = AV24contact;
               AV30contact_temp.gxTpr_Invitationdeclined = true;
               AV17allContacts.Add(AV30contact_temp, 0);
               gx_BV11 = true;
            }
            AV49GXV8 = (int)(AV49GXV8+1);
         }
         GXt_char4 = AV8error;
         new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "contacts.enc",  AV17allContacts.ToJSonString(false), out  GXt_char4) ;
         AV8error = GXt_char4;
         AssignAttri("", false, "AV8error", AV8error);
         /* Execute user subroutine: 'INIT CONACTS FROM FILE' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV17allContacts", AV17allContacts);
         nGXsfl_11_bak_idx = nGXsfl_11_idx;
         gxgrContactgrid_refresh( AV17allContacts, AV27externalUser) ;
         nGXsfl_11_idx = nGXsfl_11_bak_idx;
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
      }

      protected void E19162( )
      {
         AV39GXV1 = nGXsfl_11_idx;
         if ( ( AV39GXV1 > 0 ) && ( AV17allContacts.Count >= AV39GXV1 ) )
         {
            AV17allContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV39GXV1));
         }
         /* 'Delete Contact' Routine */
         returnInSub = false;
         this.executeExternalObjectMethod("", false, "gx.extensions.web.dialogs", "showConfirm", new Object[] {"Are you sure you want to Delete Contact "+((GeneXus.Programs.wallet.registered.SdtContact_SDT)(AV17allContacts.CurrentItem)).gxTpr_Userprivatename+"?"}, false);
         AV21selectedClientId = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)(AV17allContacts.CurrentItem)).gxTpr_Contactrid;
         AssignAttri("", false, "AV21selectedClientId", AV21selectedClientId.ToString());
         /*  Sending Event outputs  */
      }

      protected void E20162( )
      {
         AV39GXV1 = nGXsfl_11_idx;
         if ( ( AV39GXV1 > 0 ) && ( AV17allContacts.Count >= AV39GXV1 ) )
         {
            AV17allContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV39GXV1));
         }
         /* 'Chat' Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.setcontact(context ).execute(  ((GeneXus.Programs.wallet.registered.SdtContact_SDT)(AV17allContacts.CurrentItem))) ;
         CallWebObject(formatLink("wallet.registered.chat") );
         context.wjLocDisableFrm = 1;
      }

      protected void E21162( )
      {
         AV39GXV1 = nGXsfl_11_idx;
         if ( ( AV39GXV1 > 0 ) && ( AV17allContacts.Count >= AV39GXV1 ) )
         {
            AV17allContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV39GXV1));
         }
         /* 'Edit Contact' Routine */
         returnInSub = false;
         AV13websession.Set("Contact_SDT_EDIT", ((GeneXus.Programs.wallet.registered.SdtContact_SDT)(AV17allContacts.CurrentItem)).ToJSonString(false, true));
         context.PopUp(formatLink("wallet.registered.contact") , new Object[] {});
         /*  Sending Event outputs  */
      }

      protected void E14162( )
      {
         AV39GXV1 = nGXsfl_11_idx;
         if ( ( AV39GXV1 > 0 ) && ( AV17allContacts.Count >= AV39GXV1 ) )
         {
            AV17allContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV39GXV1));
         }
         /* Extensions\Web\Dialog_Onconfirmclosed Routine */
         returnInSub = false;
         if ( AV20UserResponse )
         {
            GXt_char4 = AV8error;
            new GeneXus.Programs.wallet.registered.deletecontact(context ).execute(  AV21selectedClientId, out  GXt_char4) ;
            AV8error = GXt_char4;
            AssignAttri("", false, "AV8error", AV8error);
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8error)) )
            {
               GX_msglist.addItem(AV8error);
            }
            else
            {
               AV17allContacts.Clear();
               gx_BV11 = true;
               AV17allContacts.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV8error), null);
               gx_BV11 = true;
               AV50GXV9 = 1;
               while ( AV50GXV9 <= AV17allContacts.Count )
               {
                  AV24contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV50GXV9));
                  if ( AV24contact.gxTpr_Contactrid == AV21selectedClientId )
                  {
                     AV17allContacts.RemoveItem(AV17allContacts.IndexOf(AV24contact));
                     gx_BV11 = true;
                  }
                  AV50GXV9 = (int)(AV50GXV9+1);
               }
               GXt_char4 = AV8error;
               new GeneXus.Programs.wallet.savejsonencfile(context ).execute(  "contacts.enc",  AV17allContacts.ToJSonString(false), out  GXt_char4) ;
               AV8error = GXt_char4;
               AssignAttri("", false, "AV8error", AV8error);
               /* Execute user subroutine: 'INIT CONACTS FROM FILE' */
               S112 ();
               if (returnInSub) return;
            }
         }
         /*  Sending Event outputs  */
         if ( gx_BV11 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV17allContacts", AV17allContacts);
            nGXsfl_11_bak_idx = nGXsfl_11_idx;
            gxgrContactgrid_refresh( AV17allContacts, AV27externalUser) ;
            nGXsfl_11_idx = nGXsfl_11_bak_idx;
            sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
            SubsflControlProps_112( ) ;
         }
      }

      protected void S112( )
      {
         /* 'INIT CONACTS FROM FILE' Routine */
         returnInSub = false;
         AV17allContacts.Clear();
         gx_BV11 = true;
         AV33allContacts_temp.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV8error), null);
         AV51GXV10 = 1;
         while ( AV51GXV10 <= AV33allContacts_temp.Count )
         {
            AV24contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV33allContacts_temp.Item(AV51GXV10));
            if ( ! AV24contact.gxTpr_Invitationdeclined )
            {
               AV17allContacts.Add(AV24contact, 0);
               gx_BV11 = true;
            }
            AV51GXV10 = (int)(AV51GXV10+1);
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
         PA162( ) ;
         WS162( ) ;
         WE162( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2025101144485", true, true);
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
         context.AddJavascriptSource("messages.eng.js", "?"+GetCacheInvalidationToken( ), false, true);
         context.AddJavascriptSource("gxdec.js", "?"+context.GetBuildNumber( 1759500), false, true);
         context.AddJavascriptSource("wallet/registered/contacts.js", "?2025101144485", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_112( )
      {
         edtavCtluserprivatename1_Internalname = "CTLUSERPRIVATENAME1_"+sGXsfl_11_idx;
         edtavCtlusername1_Internalname = "CTLUSERNAME1_"+sGXsfl_11_idx;
         edtavCtlinvitationsent_Internalname = "CTLINVITATIONSENT_"+sGXsfl_11_idx;
         edtavCtlinvitacionaccepted_Internalname = "CTLINVITACIONACCEPTED_"+sGXsfl_11_idx;
         edtavEditimage_Internalname = "vEDITIMAGE_"+sGXsfl_11_idx;
         edtavDeleteimage_Internalname = "vDELETEIMAGE_"+sGXsfl_11_idx;
         edtavAcceptinvitation_Internalname = "vACCEPTINVITATION_"+sGXsfl_11_idx;
         edtavDeclineinvitation_Internalname = "vDECLINEINVITATION_"+sGXsfl_11_idx;
         edtavChatimage_Internalname = "vCHATIMAGE_"+sGXsfl_11_idx;
      }

      protected void SubsflControlProps_fel_112( )
      {
         edtavCtluserprivatename1_Internalname = "CTLUSERPRIVATENAME1_"+sGXsfl_11_fel_idx;
         edtavCtlusername1_Internalname = "CTLUSERNAME1_"+sGXsfl_11_fel_idx;
         edtavCtlinvitationsent_Internalname = "CTLINVITATIONSENT_"+sGXsfl_11_fel_idx;
         edtavCtlinvitacionaccepted_Internalname = "CTLINVITACIONACCEPTED_"+sGXsfl_11_fel_idx;
         edtavEditimage_Internalname = "vEDITIMAGE_"+sGXsfl_11_fel_idx;
         edtavDeleteimage_Internalname = "vDELETEIMAGE_"+sGXsfl_11_fel_idx;
         edtavAcceptinvitation_Internalname = "vACCEPTINVITATION_"+sGXsfl_11_fel_idx;
         edtavDeclineinvitation_Internalname = "vDECLINEINVITATION_"+sGXsfl_11_fel_idx;
         edtavChatimage_Internalname = "vCHATIMAGE_"+sGXsfl_11_fel_idx;
      }

      protected void sendrow_112( )
      {
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
         WB160( ) ;
         ContactgridRow = GXWebRow.GetNew(context,ContactgridContainer);
         if ( subContactgrid_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subContactgrid_Backstyle = 0;
            if ( StringUtil.StrCmp(subContactgrid_Class, "") != 0 )
            {
               subContactgrid_Linesclass = subContactgrid_Class+"Odd";
            }
         }
         else if ( subContactgrid_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subContactgrid_Backstyle = 0;
            subContactgrid_Backcolor = subContactgrid_Allbackcolor;
            if ( StringUtil.StrCmp(subContactgrid_Class, "") != 0 )
            {
               subContactgrid_Linesclass = subContactgrid_Class+"Uniform";
            }
         }
         else if ( subContactgrid_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subContactgrid_Backstyle = 1;
            if ( StringUtil.StrCmp(subContactgrid_Class, "") != 0 )
            {
               subContactgrid_Linesclass = subContactgrid_Class+"Odd";
            }
            subContactgrid_Backcolor = (int)(0x0);
         }
         else if ( subContactgrid_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subContactgrid_Backstyle = 1;
            if ( ((int)((nGXsfl_11_idx) % (2))) == 0 )
            {
               subContactgrid_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subContactgrid_Class, "") != 0 )
               {
                  subContactgrid_Linesclass = subContactgrid_Class+"Even";
               }
            }
            else
            {
               subContactgrid_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subContactgrid_Class, "") != 0 )
               {
                  subContactgrid_Linesclass = subContactgrid_Class+"Odd";
               }
            }
         }
         if ( ContactgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr ") ;
            context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
            context.WriteHtmlText( " gxrow=\""+sGXsfl_11_idx+"\">") ;
         }
         /* Subfile cell */
         if ( ContactgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 12,'',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         ContactgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtluserprivatename1_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV39GXV1)).gxTpr_Userprivatename),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,12);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtluserprivatename1_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtluserprivatename1_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( ContactgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         ContactgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlusername1_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV39GXV1)).gxTpr_Username),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,13);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlusername1_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlusername1_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( ContactgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 14,'',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         ContactgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlinvitationsent_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV39GXV1)).gxTpr_Invitationsent, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV39GXV1)).gxTpr_Invitationsent, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onblur(this,14);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlinvitationsent_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlinvitationsent_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( ContactgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 15,'',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         ContactgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlinvitacionaccepted_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV39GXV1)).gxTpr_Invitacionaccepted, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV17allContacts.Item(AV39GXV1)).gxTpr_Invitacionaccepted, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,0);"+";gx.evt.onblur(this,15);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlinvitacionaccepted_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlinvitacionaccepted_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
         /* Subfile cell */
         if ( ContactgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+((edtavEditimage_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 16,'',false,'',11)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavEditimage_gximage, "")==0) ? "" : "GX_Image_"+edtavEditimage_gximage+"_Class");
         StyleString = "";
         AV37editImage_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV37editImage))&&String.IsNullOrEmpty(StringUtil.RTrim( AV47Editimage_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV37editImage)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV37editImage)) ? AV47Editimage_GXI : context.PathToRelativeUrl( AV37editImage));
         ContactgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavEditimage_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(int)edtavEditimage_Visible,(short)1,(string)"",(string)"edit",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavEditimage_Jsonclick,"'"+""+"'"+",false,"+"'"+"E\\'EDIT CONTACT\\'."+sGXsfl_11_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV37editImage_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl),(string)"none"});
         /* Subfile cell */
         if ( ContactgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+((edtavDeleteimage_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'',false,'',11)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavDeleteimage_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteimage_gximage+"_Class");
         StyleString = "";
         AV36deleteImage_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV36deleteImage))&&String.IsNullOrEmpty(StringUtil.RTrim( AV46Deleteimage_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV36deleteImage)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV36deleteImage)) ? AV46Deleteimage_GXI : context.PathToRelativeUrl( AV36deleteImage));
         ContactgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavDeleteimage_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(int)edtavDeleteimage_Visible,(short)1,(string)"",(string)"delete",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavDeleteimage_Jsonclick,"'"+""+"'"+",false,"+"'"+"E\\'DELETE CONTACT\\'."+sGXsfl_11_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV36deleteImage_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl),(string)"none"});
         /* Subfile cell */
         if ( ContactgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavAcceptinvitation_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 18,'',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         ContactgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavAcceptinvitation_Internalname,StringUtil.RTrim( AV29acceptInvitation),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,18);\"","'"+""+"'"+",false,"+"'"+"E\\'ACCEPT INVITATION\\'."+sGXsfl_11_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavAcceptinvitation_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(int)edtavAcceptinvitation_Visible,(int)edtavAcceptinvitation_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( ContactgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavDeclineinvitation_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         ContactgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDeclineinvitation_Internalname,StringUtil.RTrim( AV32declineInvitation),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,19);\"","'"+""+"'"+",false,"+"'"+"E\\'DECLINE INVITATION\\'."+sGXsfl_11_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDeclineinvitation_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(int)edtavDeclineinvitation_Visible,(int)edtavDeclineinvitation_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( ContactgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+((edtavChatimage_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 20,'',false,'',11)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavChatimage_gximage, "")==0) ? "" : "GX_Image_"+edtavChatimage_gximage+"_Class");
         StyleString = "";
         AV38chatImage_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV38chatImage))&&String.IsNullOrEmpty(StringUtil.RTrim( AV48Chatimage_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV38chatImage)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV38chatImage)) ? AV48Chatimage_GXI : context.PathToRelativeUrl( AV38chatImage));
         ContactgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavChatimage_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(int)edtavChatimage_Visible,(short)1,(string)"",(string)"chat",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavChatimage_Jsonclick,"'"+""+"'"+",false,"+"'"+"E\\'CHAT\\'."+sGXsfl_11_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV38chatImage_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl),(string)"none"});
         send_integrity_lvl_hashes162( ) ;
         ContactgridContainer.AddRow(ContactgridRow);
         nGXsfl_11_idx = ((subContactgrid_Islastpage==1)&&(nGXsfl_11_idx+1>subContactgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_idx+1);
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
         if ( ContactgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"ContactgridContainer"+"DivS\" data-gxgridid=\"11\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subContactgrid_Internalname, subContactgrid_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subContactgrid_Backcolorstyle == 0 )
            {
               subContactgrid_Titlebackstyle = 0;
               if ( StringUtil.Len( subContactgrid_Class) > 0 )
               {
                  subContactgrid_Linesclass = subContactgrid_Class+"Title";
               }
            }
            else
            {
               subContactgrid_Titlebackstyle = 1;
               if ( subContactgrid_Backcolorstyle == 1 )
               {
                  subContactgrid_Titlebackcolor = subContactgrid_Allbackcolor;
                  if ( StringUtil.Len( subContactgrid_Class) > 0 )
                  {
                     subContactgrid_Linesclass = subContactgrid_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subContactgrid_Class) > 0 )
                  {
                     subContactgrid_Linesclass = subContactgrid_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Contact  Name") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "User Name") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "invitation Sent") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "invitacion Accepted") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavEditimage_gximage, "")==0) ? "" : "GX_Image_"+edtavEditimage_gximage+"_Class")+"\" "+" style=\""+((edtavEditimage_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavDeleteimage_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteimage_gximage+"_Class")+"\" "+" style=\""+((edtavDeleteimage_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtavAcceptinvitation_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtavDeclineinvitation_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavChatimage_gximage, "")==0) ? "" : "GX_Image_"+edtavChatimage_gximage+"_Class")+"\" "+" style=\""+((edtavChatimage_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            ContactgridContainer.AddObjectProperty("GridName", "Contactgrid");
         }
         else
         {
            ContactgridContainer.AddObjectProperty("GridName", "Contactgrid");
            ContactgridContainer.AddObjectProperty("Header", subContactgrid_Header);
            ContactgridContainer.AddObjectProperty("Class", "Grid");
            ContactgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            ContactgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            ContactgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subContactgrid_Backcolorstyle), 1, 0, ".", "")));
            ContactgridContainer.AddObjectProperty("CmpContext", "");
            ContactgridContainer.AddObjectProperty("InMasterPage", "false");
            ContactgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            ContactgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtluserprivatename1_Enabled), 5, 0, ".", "")));
            ContactgridContainer.AddColumnProperties(ContactgridColumn);
            ContactgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            ContactgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlusername1_Enabled), 5, 0, ".", "")));
            ContactgridContainer.AddColumnProperties(ContactgridColumn);
            ContactgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            ContactgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlinvitationsent_Enabled), 5, 0, ".", "")));
            ContactgridContainer.AddColumnProperties(ContactgridColumn);
            ContactgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            ContactgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlinvitacionaccepted_Enabled), 5, 0, ".", "")));
            ContactgridContainer.AddColumnProperties(ContactgridColumn);
            ContactgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            ContactgridColumn.AddObjectProperty("Value", context.convertURL( AV37editImage));
            ContactgridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavEditimage_Link));
            ContactgridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavEditimage_Visible), 5, 0, ".", "")));
            ContactgridContainer.AddColumnProperties(ContactgridColumn);
            ContactgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            ContactgridColumn.AddObjectProperty("Value", context.convertURL( AV36deleteImage));
            ContactgridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavDeleteimage_Link));
            ContactgridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDeleteimage_Visible), 5, 0, ".", "")));
            ContactgridContainer.AddColumnProperties(ContactgridColumn);
            ContactgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            ContactgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV29acceptInvitation)));
            ContactgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavAcceptinvitation_Enabled), 5, 0, ".", "")));
            ContactgridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavAcceptinvitation_Visible), 5, 0, ".", "")));
            ContactgridContainer.AddColumnProperties(ContactgridColumn);
            ContactgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            ContactgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV32declineInvitation)));
            ContactgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDeclineinvitation_Enabled), 5, 0, ".", "")));
            ContactgridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDeclineinvitation_Visible), 5, 0, ".", "")));
            ContactgridContainer.AddColumnProperties(ContactgridColumn);
            ContactgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            ContactgridColumn.AddObjectProperty("Value", context.convertURL( AV38chatImage));
            ContactgridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavChatimage_Link));
            ContactgridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavChatimage_Visible), 5, 0, ".", "")));
            ContactgridContainer.AddColumnProperties(ContactgridColumn);
            ContactgridContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subContactgrid_Selectedindex), 4, 0, ".", "")));
            ContactgridContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subContactgrid_Allowselection), 1, 0, ".", "")));
            ContactgridContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subContactgrid_Selectioncolor), 9, 0, ".", "")));
            ContactgridContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subContactgrid_Allowhovering), 1, 0, ".", "")));
            ContactgridContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subContactgrid_Hoveringcolor), 9, 0, ".", "")));
            ContactgridContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subContactgrid_Allowcollapsing), 1, 0, ".", "")));
            ContactgridContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subContactgrid_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         bttRecovercontactsfromserver_Internalname = "RECOVERCONTACTSFROMSERVER";
         bttCreatenewcontact_Internalname = "CREATENEWCONTACT";
         edtavCtluserprivatename1_Internalname = "CTLUSERPRIVATENAME1";
         edtavCtlusername1_Internalname = "CTLUSERNAME1";
         edtavCtlinvitationsent_Internalname = "CTLINVITATIONSENT";
         edtavCtlinvitacionaccepted_Internalname = "CTLINVITACIONACCEPTED";
         edtavEditimage_Internalname = "vEDITIMAGE";
         edtavDeleteimage_Internalname = "vDELETEIMAGE";
         edtavAcceptinvitation_Internalname = "vACCEPTINVITATION";
         edtavDeclineinvitation_Internalname = "vDECLINEINVITATION";
         edtavChatimage_Internalname = "vCHATIMAGE";
         divMaintable_Internalname = "MAINTABLE";
         Form.Internalname = "FORM";
         subContactgrid_Internalname = "CONTACTGRID";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subContactgrid_Allowcollapsing = 0;
         subContactgrid_Allowselection = 0;
         edtavChatimage_Link = "";
         edtavDeleteimage_Link = "";
         edtavEditimage_Link = "";
         subContactgrid_Header = "";
         edtavChatimage_Jsonclick = "";
         edtavChatimage_gximage = "";
         edtavChatimage_Visible = -1;
         edtavDeclineinvitation_Jsonclick = "";
         edtavDeclineinvitation_Enabled = 1;
         edtavDeclineinvitation_Visible = -1;
         edtavAcceptinvitation_Jsonclick = "";
         edtavAcceptinvitation_Enabled = 1;
         edtavAcceptinvitation_Visible = -1;
         edtavDeleteimage_Jsonclick = "";
         edtavDeleteimage_gximage = "";
         edtavDeleteimage_Visible = -1;
         edtavEditimage_Jsonclick = "";
         edtavEditimage_gximage = "";
         edtavEditimage_Visible = -1;
         edtavCtlinvitacionaccepted_Jsonclick = "";
         edtavCtlinvitacionaccepted_Enabled = 0;
         edtavCtlinvitationsent_Jsonclick = "";
         edtavCtlinvitationsent_Enabled = 0;
         edtavCtlusername1_Jsonclick = "";
         edtavCtlusername1_Enabled = 0;
         edtavCtluserprivatename1_Jsonclick = "";
         edtavCtluserprivatename1_Enabled = 0;
         subContactgrid_Class = "Grid";
         subContactgrid_Backcolorstyle = 0;
         edtavCtlinvitacionaccepted_Enabled = -1;
         edtavCtlinvitationsent_Enabled = -1;
         edtavCtlusername1_Enabled = -1;
         edtavCtluserprivatename1_Enabled = -1;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Contacts";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"CONTACTGRID_nFirstRecordOnPage","type":"int"},{"av":"CONTACTGRID_nEOF","type":"int"},{"av":"AV17allContacts","fld":"vALLCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"nRC_GXsfl_11","ctrl":"CONTACTGRID","prop":"GridRC","grid":11,"type":"int"},{"av":"AV27externalUser","fld":"vEXTERNALUSER","hsh":true,"type":""}]}""");
         setEventMetadata("'RECOVER CONTACTS FROM SERVER'","""{"handler":"E12162","iparms":[{"av":"AV17allContacts","fld":"vALLCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"CONTACTGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"CONTACTGRID","prop":"GridRC","grid":11,"type":"int"},{"av":"CONTACTGRID_nEOF","type":"int"},{"av":"AV27externalUser","fld":"vEXTERNALUSER","hsh":true,"type":""}]""");
         setEventMetadata("'RECOVER CONTACTS FROM SERVER'",""","oparms":[{"av":"AV8error","fld":"vERROR","type":"char"},{"av":"AV17allContacts","fld":"vALLCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"CONTACTGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"CONTACTGRID","prop":"GridRC","grid":11,"type":"int"}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED","""{"handler":"E13162","iparms":[{"av":"AV10PopupName","fld":"vPOPUPNAME","type":"char"},{"av":"AV26sdt_message","fld":"vSDT_MESSAGE","type":""},{"av":"AV27externalUser","fld":"vEXTERNALUSER","hsh":true,"type":""},{"av":"AV28message_signature","fld":"vMESSAGE_SIGNATURE","type":""},{"av":"AV8error","fld":"vERROR","type":"char"},{"av":"AV17allContacts","fld":"vALLCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"CONTACTGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"CONTACTGRID","prop":"GridRC","grid":11,"type":"int"},{"av":"CONTACTGRID_nEOF","type":"int"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.POPUP.ONPOPUPCLOSED",""","oparms":[{"av":"AV26sdt_message","fld":"vSDT_MESSAGE","type":""},{"av":"AV28message_signature","fld":"vMESSAGE_SIGNATURE","type":""},{"av":"AV17allContacts","fld":"vALLCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"CONTACTGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"CONTACTGRID","prop":"GridRC","grid":11,"type":"int"},{"av":"AV8error","fld":"vERROR","type":"char"}]}""");
         setEventMetadata("CONTACTGRID.LOAD","""{"handler":"E16162","iparms":[{"av":"AV17allContacts","fld":"vALLCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"CONTACTGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"CONTACTGRID","prop":"GridRC","grid":11,"type":"int"}]""");
         setEventMetadata("CONTACTGRID.LOAD",""","oparms":[{"av":"AV36deleteImage","fld":"vDELETEIMAGE","type":"bits"},{"av":"AV37editImage","fld":"vEDITIMAGE","type":"bits"},{"av":"AV38chatImage","fld":"vCHATIMAGE","type":"bits"},{"av":"AV29acceptInvitation","fld":"vACCEPTINVITATION","type":"char"},{"av":"AV32declineInvitation","fld":"vDECLINEINVITATION","type":"char"},{"av":"edtavAcceptinvitation_Visible","ctrl":"vACCEPTINVITATION","prop":"Visible"},{"av":"edtavDeclineinvitation_Visible","ctrl":"vDECLINEINVITATION","prop":"Visible"},{"av":"edtavChatimage_Visible","ctrl":"vCHATIMAGE","prop":"Visible"},{"av":"edtavDeleteimage_Visible","ctrl":"vDELETEIMAGE","prop":"Visible"},{"av":"edtavEditimage_Visible","ctrl":"vEDITIMAGE","prop":"Visible"}]}""");
         setEventMetadata("'CREATE NEW CONTACT'","""{"handler":"E11161","iparms":[]}""");
         setEventMetadata("'ACCEPT INVITATION'","""{"handler":"E17162","iparms":[{"av":"AV17allContacts","fld":"vALLCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"CONTACTGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"CONTACTGRID","prop":"GridRC","grid":11,"type":"int"}]}""");
         setEventMetadata("'DECLINE INVITATION'","""{"handler":"E18162","iparms":[{"av":"AV17allContacts","fld":"vALLCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"CONTACTGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"CONTACTGRID","prop":"GridRC","grid":11,"type":"int"},{"av":"AV8error","fld":"vERROR","type":"char"},{"av":"CONTACTGRID_nEOF","type":"int"},{"av":"AV27externalUser","fld":"vEXTERNALUSER","hsh":true,"type":""}]""");
         setEventMetadata("'DECLINE INVITATION'",""","oparms":[{"av":"AV17allContacts","fld":"vALLCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"CONTACTGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"CONTACTGRID","prop":"GridRC","grid":11,"type":"int"},{"av":"AV8error","fld":"vERROR","type":"char"}]}""");
         setEventMetadata("'DELETE CONTACT'","""{"handler":"E19162","iparms":[{"av":"AV17allContacts","fld":"vALLCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"CONTACTGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"CONTACTGRID","prop":"GridRC","grid":11,"type":"int"}]""");
         setEventMetadata("'DELETE CONTACT'",""","oparms":[{"av":"AV21selectedClientId","fld":"vSELECTEDCLIENTID","type":"guid"}]}""");
         setEventMetadata("'CHAT'","""{"handler":"E20162","iparms":[{"av":"AV17allContacts","fld":"vALLCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"CONTACTGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"CONTACTGRID","prop":"GridRC","grid":11,"type":"int"}]}""");
         setEventMetadata("'EDIT CONTACT'","""{"handler":"E21162","iparms":[{"av":"AV17allContacts","fld":"vALLCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"CONTACTGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"CONTACTGRID","prop":"GridRC","grid":11,"type":"int"}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED","""{"handler":"E14162","iparms":[{"av":"AV20UserResponse","fld":"vUSERRESPONSE","type":"boolean"},{"av":"AV21selectedClientId","fld":"vSELECTEDCLIENTID","type":"guid"},{"av":"AV8error","fld":"vERROR","type":"char"},{"av":"AV17allContacts","fld":"vALLCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"CONTACTGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"CONTACTGRID","prop":"GridRC","grid":11,"type":"int"},{"av":"CONTACTGRID_nEOF","type":"int"},{"av":"AV27externalUser","fld":"vEXTERNALUSER","hsh":true,"type":""}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED",""","oparms":[{"av":"AV8error","fld":"vERROR","type":"char"},{"av":"AV17allContacts","fld":"vALLCONTACTS","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"CONTACTGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"CONTACTGRID","prop":"GridRC","grid":11,"type":"int"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Chatimage","iparms":[]}""");
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
         AV17allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         AV27externalUser = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV10PopupName = "";
         AV26sdt_message = new GeneXus.Programs.nostr.SdtSDT_message(context);
         AV28message_signature = new GeneXus.Programs.wallet.registered.SdtMessage_signature(context);
         AV8error = "";
         AV21selectedClientId = Guid.Empty;
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttRecovercontactsfromserver_Jsonclick = "";
         bttCreatenewcontact_Jsonclick = "";
         ContactgridContainer = new GXWebGrid( context);
         sStyleString = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV37editImage = "";
         AV47Editimage_GXI = "";
         AV36deleteImage = "";
         AV46Deleteimage_GXI = "";
         AV29acceptInvitation = "";
         AV32declineInvitation = "";
         AV38chatImage = "";
         AV48Chatimage_GXI = "";
         GXt_SdtExternalUser1 = new GeneXus.Programs.distcrypt.SdtExternalUser(context);
         AV13websession = context.GetSession();
         AV11expectedPopupName = "";
         AV24contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         AV22ContactId = Guid.Empty;
         GXt_char2 = "";
         AV30contact_temp = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         ContactgridRow = new GXWebRow();
         AV34UserName = "";
         GXt_char4 = "";
         AV33allContacts_temp = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subContactgrid_Linesclass = "";
         ROClassString = "";
         sImgUrl = "";
         ContactgridColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtluserprivatename1_Enabled = 0;
         edtavCtlusername1_Enabled = 0;
         edtavCtlinvitationsent_Enabled = 0;
         edtavCtlinvitacionaccepted_Enabled = 0;
         edtavAcceptinvitation_Enabled = 0;
         edtavDeclineinvitation_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subContactgrid_Backcolorstyle ;
      private short CONTACTGRID_nEOF ;
      private short AV12strFound ;
      private short nGXWrapped ;
      private short subContactgrid_Backstyle ;
      private short subContactgrid_Titlebackstyle ;
      private short subContactgrid_Allowselection ;
      private short subContactgrid_Allowhovering ;
      private short subContactgrid_Allowcollapsing ;
      private short subContactgrid_Collapsed ;
      private int nRC_GXsfl_11 ;
      private int nGXsfl_11_idx=1 ;
      private int AV39GXV1 ;
      private int subContactgrid_Islastpage ;
      private int edtavCtluserprivatename1_Enabled ;
      private int edtavCtlusername1_Enabled ;
      private int edtavCtlinvitationsent_Enabled ;
      private int edtavCtlinvitacionaccepted_Enabled ;
      private int edtavAcceptinvitation_Enabled ;
      private int edtavDeclineinvitation_Enabled ;
      private int nGXsfl_11_fel_idx=1 ;
      private int nGXsfl_11_bak_idx=1 ;
      private int AV44GXV6 ;
      private int AV45GXV7 ;
      private int edtavAcceptinvitation_Visible ;
      private int edtavDeclineinvitation_Visible ;
      private int edtavChatimage_Visible ;
      private int edtavDeleteimage_Visible ;
      private int edtavEditimage_Visible ;
      private int AV49GXV8 ;
      private int AV50GXV9 ;
      private int AV51GXV10 ;
      private int idxLst ;
      private int subContactgrid_Backcolor ;
      private int subContactgrid_Allbackcolor ;
      private int subContactgrid_Titlebackcolor ;
      private int subContactgrid_Selectedindex ;
      private int subContactgrid_Selectioncolor ;
      private int subContactgrid_Hoveringcolor ;
      private long CONTACTGRID_nCurrentRecord ;
      private long CONTACTGRID_nFirstRecordOnPage ;
      private long GXt_int3 ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_11_idx="0001" ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV10PopupName ;
      private string AV8error ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttRecovercontactsfromserver_Internalname ;
      private string bttRecovercontactsfromserver_Jsonclick ;
      private string bttCreatenewcontact_Internalname ;
      private string bttCreatenewcontact_Jsonclick ;
      private string sStyleString ;
      private string subContactgrid_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string edtavEditimage_Internalname ;
      private string edtavDeleteimage_Internalname ;
      private string AV29acceptInvitation ;
      private string edtavAcceptinvitation_Internalname ;
      private string AV32declineInvitation ;
      private string edtavDeclineinvitation_Internalname ;
      private string edtavChatimage_Internalname ;
      private string sGXsfl_11_fel_idx="0001" ;
      private string AV11expectedPopupName ;
      private string GXt_char2 ;
      private string edtavDeleteimage_gximage ;
      private string edtavEditimage_gximage ;
      private string edtavChatimage_gximage ;
      private string AV34UserName ;
      private string GXt_char4 ;
      private string edtavCtluserprivatename1_Internalname ;
      private string edtavCtlusername1_Internalname ;
      private string edtavCtlinvitationsent_Internalname ;
      private string edtavCtlinvitacionaccepted_Internalname ;
      private string subContactgrid_Class ;
      private string subContactgrid_Linesclass ;
      private string ROClassString ;
      private string edtavCtluserprivatename1_Jsonclick ;
      private string edtavCtlusername1_Jsonclick ;
      private string edtavCtlinvitationsent_Jsonclick ;
      private string edtavCtlinvitacionaccepted_Jsonclick ;
      private string sImgUrl ;
      private string edtavEditimage_Jsonclick ;
      private string edtavDeleteimage_Jsonclick ;
      private string edtavAcceptinvitation_Jsonclick ;
      private string edtavDeclineinvitation_Jsonclick ;
      private string edtavChatimage_Jsonclick ;
      private string subContactgrid_Header ;
      private string edtavEditimage_Link ;
      private string edtavDeleteimage_Link ;
      private string edtavChatimage_Link ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV20UserResponse ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_11_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV11 ;
      private bool AV37editImage_IsBlob ;
      private bool AV36deleteImage_IsBlob ;
      private bool AV38chatImage_IsBlob ;
      private string AV47Editimage_GXI ;
      private string AV46Deleteimage_GXI ;
      private string AV48Chatimage_GXI ;
      private string AV37editImage ;
      private string AV36deleteImage ;
      private string AV38chatImage ;
      private Guid AV21selectedClientId ;
      private Guid AV22ContactId ;
      private GXWebGrid ContactgridContainer ;
      private GXWebRow ContactgridRow ;
      private GXWebColumn ContactgridColumn ;
      private IGxSession AV13websession ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV17allContacts ;
      private GeneXus.Programs.distcrypt.SdtExternalUser AV27externalUser ;
      private GeneXus.Programs.nostr.SdtSDT_message AV26sdt_message ;
      private GeneXus.Programs.wallet.registered.SdtMessage_signature AV28message_signature ;
      private GeneXus.Programs.distcrypt.SdtExternalUser GXt_SdtExternalUser1 ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV24contact ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV30contact_temp ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV33allContacts_temp ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
