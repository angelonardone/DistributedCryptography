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
   public class approverestoredate : GXDataArea
   {
      public approverestoredate( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public approverestoredate( IGxContext context )
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
         PA2R2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START2R2( ) ;
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 2351240), false, true, false);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 2351240), false, true, false);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 2351240), false, true, false);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true, false);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 2351240), false, true, false);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 2351240), false, true, false);
         context.AddJavascriptSource("calendar-en.js", "?"+context.GetBuildNumber( 2351240), false, true, false);
         context.WriteHtmlText( Form.Headerrawhtml) ;
         context.CloseHtmlHeader();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         FormProcess = " data-HasEnter=\"true\" data-Skiponenter=\"false\"";
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.approverestoredate") +"\">") ;
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
         GxWebStd.gx_hidden_field( context, "vTODAY", context.localUtil.DToC( Gx_date, 0, "/"));
         GxWebStd.gx_hidden_field( context, "gxhash_vTODAY", GetSecureSignedToken( "", Gx_date, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vONETIMECONSTRAIN", AV25oneTimeConstrain);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vONETIMECONSTRAIN", AV25oneTimeConstrain);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vONETIMECONSTRAIN", GetSecureSignedToken( "", AV25oneTimeConstrain, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vGROUP_SDT", AV23group_sdt);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vGROUP_SDT", AV23group_sdt);
         }
         GxWebStd.gx_hidden_field( context, "vTODAY", context.localUtil.DToC( Gx_date, 0, "/"));
         GxWebStd.gx_hidden_field( context, "gxhash_vTODAY", GetSecureSignedToken( "", Gx_date, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vONETIMECONSTRAIN", AV25oneTimeConstrain);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vONETIMECONSTRAIN", AV25oneTimeConstrain);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vONETIMECONSTRAIN", GetSecureSignedToken( "", AV25oneTimeConstrain, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vNEWTIMECONSTRAIN", AV35newTimeConstrain);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vNEWTIMECONSTRAIN", AV35newTimeConstrain);
         }
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
            WE2R2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT2R2( ) ;
      }

      public override bool HasEnterEvent( )
      {
         return true ;
      }

      public override GXWebForm GetForm( )
      {
         return Form ;
      }

      public override string GetSelfLink( )
      {
         return formatLink("wallet.registered.approverestoredate")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.registered.ApproveRestoreDate" ;
      }

      public override string GetPgmdesc( )
      {
         return "Approve Restore Date" ;
      }

      protected void WB2R0( )
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            wb_table1_6_2R2( true) ;
         }
         else
         {
            wb_table1_6_2R2( false) ;
         }
         return  ;
      }

      protected void wb_table1_6_2R2e( bool wbgen )
      {
         if ( wbgen )
         {
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 100, "px", "col-xs-12", "start", "Bottom", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavPassword_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavPassword_Internalname, "Enter password", "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 16,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavPassword_Internalname, StringUtil.RTrim( AV12password), StringUtil.RTrim( context.localUtil.Format( AV12password, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,16);\""+" "+"data-gx-password-reveal"+" ", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavPassword_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavPassword_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, -1, 0, 0, 0, 0, -1, true, "", "start", true, "", "HLP_Wallet/registered/ApproveRestoreDate.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "Bottom", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 100, "px", "col-xs-12", "start", "Bottom", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttEnter_Internalname, "", "Confirm", bttEnter_Jsonclick, 5, "Confirm", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/ApproveRestoreDate.htm");
            GxWebStd.gx_div_end( context, "start", "Bottom", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START2R2( )
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
         Form.Meta.addItem("description", "Approve Restore Date", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP2R0( ) ;
      }

      protected void WS2R2( )
      {
         START2R2( ) ;
         EVT2R2( ) ;
      }

      protected void EVT2R2( )
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
                           else if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Start */
                              E112R2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                           {
                              context.wbHandled = 1;
                              if ( ! wbErr )
                              {
                                 Rfr0gs = false;
                                 if ( ! Rfr0gs )
                                 {
                                    /* Execute user event: Enter */
                                    E122R2 ();
                                 }
                                 dynload_actions( ) ;
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E132R2 ();
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
      }

      protected void WE2R2( )
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

      protected void PA2R2( )
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
               GX_FocusControl = edtavRestoredate_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
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
         RF2R2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         Gx_date = DateTimeUtil.Today( context);
      }

      protected void RF2R2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E132R2 ();
            WB2R0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes2R2( )
      {
         GxWebStd.gx_hidden_field( context, "vTODAY", context.localUtil.DToC( Gx_date, 0, "/"));
         GxWebStd.gx_hidden_field( context, "gxhash_vTODAY", GetSecureSignedToken( "", Gx_date, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vONETIMECONSTRAIN", AV25oneTimeConstrain);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vONETIMECONSTRAIN", AV25oneTimeConstrain);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vONETIMECONSTRAIN", GetSecureSignedToken( "", AV25oneTimeConstrain, context));
      }

      protected void before_start_formulas( )
      {
         Gx_date = DateTimeUtil.Today( context);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP2R0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E112R2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            /* Read variables values. */
            if ( context.localUtil.VCDate( cgiGet( edtavRestoredate_Internalname), 1) == 0 )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_faildate", new   object[]  {"restore Date"}), 1, "vRESTOREDATE");
               GX_FocusControl = edtavRestoredate_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV27restoreDate = DateTime.MinValue;
               AssignAttri("", false, "AV27restoreDate", context.localUtil.Format(AV27restoreDate, "99/99/99"));
            }
            else
            {
               AV27restoreDate = context.localUtil.CToD( cgiGet( edtavRestoredate_Internalname), 1);
               AssignAttri("", false, "AV27restoreDate", context.localUtil.Format(AV27restoreDate, "99/99/99"));
            }
            AV12password = cgiGet( edtavPassword_Internalname);
            AssignAttri("", false, "AV12password", AV12password);
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
         E112R2 ();
         if (returnInSub) return;
      }

      protected void E112R2( )
      {
         /* Start Routine */
         returnInSub = false;
         AV23group_sdt.FromJSonString(AV34websession.Get("Group_EDIT"), null);
         if ( AV23group_sdt.gxTpr_Isactive )
         {
            GXt_SdtGroup_SDT_TimeConstrainItem1 = AV25oneTimeConstrain;
            new GeneXus.Programs.wallet.registered.getnewesttimeconstrain(context ).execute(  AV23group_sdt.gxTpr_Timeconstrain, out  GXt_SdtGroup_SDT_TimeConstrainItem1) ;
            AV25oneTimeConstrain = GXt_SdtGroup_SDT_TimeConstrainItem1;
            edtavRestoredate_Visible = 1;
            AssignProp("", false, edtavRestoredate_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavRestoredate_Visible), 5, 0), true);
         }
         else
         {
            edtavRestoredate_Visible = 0;
            AssignProp("", false, edtavRestoredate_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavRestoredate_Visible), 5, 0), true);
         }
      }

      public void GXEnter( )
      {
         /* Execute user event: Enter */
         E122R2 ();
         if (returnInSub) return;
      }

      protected void E122R2( )
      {
         /* Enter Routine */
         returnInSub = false;
         if ( AV23group_sdt.gxTpr_Isactive )
         {
            if ( (DateTime.MinValue==AV27restoreDate) )
            {
               AV6error = "Please select the date when the backup became available to be restored.";
            }
            else
            {
               if ( DateTimeUtil.ResetTime ( AV27restoreDate ) <= DateTimeUtil.ResetTime ( Gx_date ) )
               {
                  AV6error = "The date must be in the future";
               }
               else
               {
                  if ( DateTimeUtil.ResetTime ( AV27restoreDate ) <= DateTimeUtil.ResetTime ( AV25oneTimeConstrain.gxTpr_Date ) )
                  {
                     AV6error = "The date must be after the previous configure date";
                  }
                  else
                  {
                     GXt_char2 = AV6error;
                     new GeneXus.Programs.wallet.approveactionsetextkey(context ).execute(  AV12password, out  GXt_char2) ;
                     AV6error = GXt_char2;
                     if ( String.IsNullOrEmpty(StringUtil.RTrim( AV6error)) )
                     {
                        AV35newTimeConstrain.gxTpr_Date = AV27restoreDate;
                        AV35newTimeConstrain.gxTpr_Sequence = (int)(AV25oneTimeConstrain.gxTpr_Sequence+1);
                        AV35newTimeConstrain.gxTpr_Address = "";
                        AV23group_sdt.gxTpr_Timeconstrain.Add(AV35newTimeConstrain, 0);
                        AV34websession.Set("Group_EDIT", AV23group_sdt.ToJSonString(false, true));
                        context.setWebReturnParms(new Object[] {});
                        context.setWebReturnParmsMetadata(new Object[] {});
                        context.wjLocDisableFrm = 1;
                        context.nUserReturn = 1;
                        returnInSub = true;
                        if (true) return;
                     }
                  }
               }
            }
         }
         else
         {
            GXt_char2 = AV6error;
            new GeneXus.Programs.wallet.approveactionsetextkey(context ).execute(  AV12password, out  GXt_char2) ;
            AV6error = GXt_char2;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV6error)) )
            {
               context.setWebReturnParms(new Object[] {});
               context.setWebReturnParmsMetadata(new Object[] {});
               context.wjLocDisableFrm = 1;
               context.nUserReturn = 1;
               returnInSub = true;
               if (true) return;
            }
         }
         GX_msglist.addItem(AV6error);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV35newTimeConstrain", AV35newTimeConstrain);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV23group_sdt", AV23group_sdt);
      }

      protected void nextLoad( )
      {
      }

      protected void E132R2( )
      {
         /* Load Routine */
         returnInSub = false;
      }

      protected void wb_table1_6_2R2( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblTable1_Internalname, tblTable1_Internalname, "", "Table", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td>") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavRestoredate_Visible, 0, "px", 0, "px", "form-group gx-form-group gx-default-form-group", "start", "top", ""+" data-gx-for=\""+edtavRestoredate_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavRestoredate_Internalname, "Select the date when the backup became available to be restored.", "gx-form-item AttributeLabel", 1, true, "width: 75%;");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 25, "%", 0, "px", "gx-form-item gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 11,'',false,'',0)\"";
            context.WriteHtmlText( "<div id=\""+edtavRestoredate_Internalname+"_dp_container\" class=\"dp_container\" style=\"white-space:nowrap;display:inline;\">") ;
            GxWebStd.gx_single_line_edit( context, edtavRestoredate_Internalname, context.localUtil.Format(AV27restoreDate, "99/99/99"), context.localUtil.Format( AV27restoreDate, "99/99/99"), TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',0,12,'eng',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',0,12,'eng',false,0);"+";gx.evt.onblur(this,11);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavRestoredate_Jsonclick, 0, "Attribute", "", "", "", "", edtavRestoredate_Visible, edtavRestoredate_Enabled, 0, "text", "", 8, "chr", 1, "row", 8, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Wallet/registered/ApproveRestoreDate.htm");
            GxWebStd.gx_bitmap( context, edtavRestoredate_Internalname+"_dp_trigger", context.GetImagePath( "61b9b5d3-dff6-4d59-9b00-da61bc2cbe93", "", context.GetTheme( )), "", "", "", "", ((edtavRestoredate_Visible==0)||(edtavRestoredate_Enabled==0) ? 0 : 1), 0, "Date selector", "Date selector", 0, 1, 0, "", 0, "", 0, 0, 0, "", "", "cursor: pointer;", "", "", "", "", "", "", "", "", 1, false, false, "", "none", "HLP_Wallet/registered/ApproveRestoreDate.htm");
            context.WriteHtmlTextNl( "</div>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table1_6_2R2e( true) ;
         }
         else
         {
            wb_table1_6_2R2e( false) ;
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
         PA2R2( ) ;
         WS2R2( ) ;
         WE2R2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202613016302670", true, true, false);
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
         context.AddJavascriptSource("messages.eng.js", "?"+GetCacheInvalidationToken( ), false, true, false);
         context.AddJavascriptSource("wallet/registered/approverestoredate.js", "?202613016302670", false, true, false);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         edtavRestoredate_Internalname = "vRESTOREDATE";
         tblTable1_Internalname = "TABLE1";
         edtavPassword_Internalname = "vPASSWORD";
         bttEnter_Internalname = "ENTER";
         divMaintable_Internalname = "MAINTABLE";
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
         edtavRestoredate_Jsonclick = "";
         edtavRestoredate_Enabled = 1;
         edtavRestoredate_Visible = 1;
         edtavPassword_Jsonclick = "";
         edtavPassword_Enabled = 1;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Approve Restore Date";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"Gx_date","fld":"vTODAY","hsh":true,"type":"date"},{"av":"AV25oneTimeConstrain","fld":"vONETIMECONSTRAIN","hsh":true,"type":""}]}""");
         setEventMetadata("ENTER","""{"handler":"E122R2","iparms":[{"av":"AV23group_sdt","fld":"vGROUP_SDT","type":""},{"av":"AV27restoreDate","fld":"vRESTOREDATE","type":"date"},{"av":"Gx_date","fld":"vTODAY","hsh":true,"type":"date"},{"av":"AV25oneTimeConstrain","fld":"vONETIMECONSTRAIN","hsh":true,"type":""},{"av":"AV12password","fld":"vPASSWORD","type":"char"},{"av":"AV35newTimeConstrain","fld":"vNEWTIMECONSTRAIN","type":""}]""");
         setEventMetadata("ENTER",""","oparms":[{"av":"AV35newTimeConstrain","fld":"vNEWTIMECONSTRAIN","type":""},{"av":"AV23group_sdt","fld":"vGROUP_SDT","type":""}]}""");
         setEventMetadata("VALIDV_RESTOREDATE","""{"handler":"Validv_Restoredate","iparms":[]}""");
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
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         Gx_date = DateTime.MinValue;
         AV25oneTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         GXKey = "";
         AV23group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
         AV35newTimeConstrain = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         TempTags = "";
         AV12password = "";
         ClassString = "";
         StyleString = "";
         bttEnter_Jsonclick = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV27restoreDate = DateTime.MinValue;
         AV34websession = context.GetSession();
         GXt_SdtGroup_SDT_TimeConstrainItem1 = new GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem(context);
         AV6error = "";
         GXt_char2 = "";
         sStyleString = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         Gx_date = DateTimeUtil.Today( context);
         /* GeneXus formulas. */
         Gx_date = DateTimeUtil.Today( context);
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short nGXWrapped ;
      private int edtavPassword_Enabled ;
      private int edtavRestoredate_Visible ;
      private int edtavRestoredate_Enabled ;
      private int idxLst ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string edtavPassword_Internalname ;
      private string TempTags ;
      private string AV12password ;
      private string edtavPassword_Jsonclick ;
      private string ClassString ;
      private string StyleString ;
      private string bttEnter_Internalname ;
      private string bttEnter_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string edtavRestoredate_Internalname ;
      private string AV6error ;
      private string GXt_char2 ;
      private string sStyleString ;
      private string tblTable1_Internalname ;
      private string edtavRestoredate_Jsonclick ;
      private DateTime Gx_date ;
      private DateTime AV27restoreDate ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private IGxSession AV34websession ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem AV25oneTimeConstrain ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT AV23group_sdt ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem AV35newTimeConstrain ;
      private GeneXus.Programs.wallet.registered.SdtGroup_SDT_TimeConstrainItem GXt_SdtGroup_SDT_TimeConstrainItem1 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
