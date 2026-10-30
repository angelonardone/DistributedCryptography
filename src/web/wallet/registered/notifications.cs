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
   public class notifications : GXDataArea
   {
      public notifications( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public notifications( IGxContext context )
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
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Notificationsgrid") == 0 )
            {
               gxnrNotificationsgrid_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Notificationsgrid") == 0 )
            {
               gxgrNotificationsgrid_refresh_invoke( ) ;
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

      protected void gxnrNotificationsgrid_newrow_invoke( )
      {
         nRC_GXsfl_13 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_13"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_13_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_13_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_13_idx = GetPar( "sGXsfl_13_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrNotificationsgrid_newrow( ) ;
         /* End function gxnrNotificationsgrid_newrow_invoke */
      }

      protected void gxgrNotificationsgrid_refresh_invoke( )
      {
         subNotificationsgrid_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subNotificationsgrid_Rows"), "."), 18, MidpointRounding.ToEven));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV11targets);
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrNotificationsgrid_refresh( subNotificationsgrid_Rows, AV11targets) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrNotificationsgrid_refresh_invoke */
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
         PA3Q2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START3Q2( ) ;
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.notifications") +"\">") ;
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
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Targets", AV11targets);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Targets", AV11targets);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_13", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_13), 8, 0, ".", "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vTARGETS", AV11targets);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vTARGETS", AV11targets);
         }
         GxWebStd.gx_hidden_field( context, "NOTIFICATIONSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(NOTIFICATIONSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "NOTIFICATIONSGRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(NOTIFICATIONSGRID_nEOF), 1, 0, ".", "")));
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
            WE3Q2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT3Q2( ) ;
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
         return formatLink("wallet.registered.notifications")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.registered.Notifications" ;
      }

      public override string GetPgmdesc( )
      {
         return "Notifications" ;
      }

      protected void WB3Q0( )
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbsummary_Internalname, lblTbsummary_Caption, "", "", lblTbsummary_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Wallet/registered/Notifications.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttMarkallread_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(13), 2, 0)+","+"null"+");", "Mark all as read", bttMarkallread_Jsonclick, 5, "Mark all as read", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'MARK ALL READ\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/Notifications.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-4", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 10,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttDeleteread_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(13), 2, 0)+","+"null"+");", "Delete the read ones", bttDeleteread_Jsonclick, 5, "Delete the read ones", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DELETE READ\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/Notifications.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            NotificationsgridContainer.SetWrapped(nGXWrapped);
            StartGridControl13( ) ;
         }
         if ( wbEnd == 13 )
         {
            wbEnd = 0;
            nRC_GXsfl_13 = (int)(nGXsfl_13_idx-1);
            if ( NotificationsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               NotificationsgridContainer.AddObjectProperty("NOTIFICATIONSGRID_nEOF", NOTIFICATIONSGRID_nEOF);
               NotificationsgridContainer.AddObjectProperty("NOTIFICATIONSGRID_nFirstRecordOnPage", NOTIFICATIONSGRID_nFirstRecordOnPage);
               AV14GXV1 = nGXsfl_13_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"NotificationsgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Notificationsgrid", NotificationsgridContainer, subNotificationsgrid_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "NotificationsgridContainerData", NotificationsgridContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "NotificationsgridContainerData"+"V", NotificationsgridContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"NotificationsgridContainerData"+"V"+"\" value='"+NotificationsgridContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 13 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( NotificationsgridContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  NotificationsgridContainer.AddObjectProperty("NOTIFICATIONSGRID_nEOF", NOTIFICATIONSGRID_nEOF);
                  NotificationsgridContainer.AddObjectProperty("NOTIFICATIONSGRID_nFirstRecordOnPage", NOTIFICATIONSGRID_nFirstRecordOnPage);
                  AV14GXV1 = nGXsfl_13_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"NotificationsgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Notificationsgrid", NotificationsgridContainer, subNotificationsgrid_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "NotificationsgridContainerData", NotificationsgridContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "NotificationsgridContainerData"+"V", NotificationsgridContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"NotificationsgridContainerData"+"V"+"\" value='"+NotificationsgridContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START3Q2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_16-189595", 0) ;
            }
         }
         Form.Meta.addItem("description", "Notifications", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP3Q0( ) ;
      }

      protected void WS3Q2( )
      {
         START3Q2( ) ;
         EVT3Q2( ) ;
      }

      protected void EVT3Q2( )
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
                           else if ( StringUtil.StrCmp(sEvt, "'MARK ALL READ'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Mark All Read' */
                              E113Q2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DELETE READ'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Delete Read' */
                              E123Q2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                           }
                           else if ( StringUtil.StrCmp(sEvt, "NOTIFICATIONSGRIDPAGING") == 0 )
                           {
                              context.wbHandled = 1;
                              sEvt = cgiGet( "NOTIFICATIONSGRIDPAGING");
                              if ( StringUtil.StrCmp(sEvt, "FIRST") == 0 )
                              {
                                 subnotificationsgrid_firstpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "PREV") == 0 )
                              {
                                 subnotificationsgrid_previouspage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "NEXT") == 0 )
                              {
                                 subnotificationsgrid_nextpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "LAST") == 0 )
                              {
                                 subnotificationsgrid_lastpage( ) ;
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 22), "NOTIFICATIONSGRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 19), "'OPEN NOTIFICATION'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 19), "'OPEN NOTIFICATION'") == 0 ) )
                           {
                              nGXsfl_13_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_13_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_13_idx), 4, 0), 4, "0");
                              SubsflControlProps_132( ) ;
                              AV14GXV1 = (int)(nGXsfl_13_idx+NOTIFICATIONSGRID_nFirstRecordOnPage);
                              if ( ( AV11targets.Count >= AV14GXV1 ) && ( AV14GXV1 > 0 ) )
                              {
                                 AV11targets.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtNotificationTarget)AV11targets.Item(AV14GXV1));
                                 AV7gridStatus = cgiGet( edtavGridstatus_Internalname);
                                 AssignAttri("", false, edtavGridstatus_Internalname, AV7gridStatus);
                                 AV13gridTitle = cgiGet( edtavGridtitle_Internalname);
                                 AssignAttri("", false, edtavGridtitle_Internalname, AV13gridTitle);
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Refresh */
                                    E133Q2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "NOTIFICATIONSGRID.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Notificationsgrid.Load */
                                    E143Q2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'OPEN NOTIFICATION'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Open Notification' */
                                    E153Q2 ();
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

      protected void WE3Q2( )
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

      protected void PA3Q2( )
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

      protected void gxnrNotificationsgrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_132( ) ;
         while ( nGXsfl_13_idx <= nRC_GXsfl_13 )
         {
            sendrow_132( ) ;
            nGXsfl_13_idx = ((subNotificationsgrid_Islastpage==1)&&(nGXsfl_13_idx+1>subNotificationsgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_13_idx+1);
            sGXsfl_13_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_13_idx), 4, 0), 4, "0");
            SubsflControlProps_132( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( NotificationsgridContainer)) ;
         /* End function gxnrNotificationsgrid_newrow */
      }

      protected void gxgrNotificationsgrid_refresh( int subNotificationsgrid_Rows ,
                                                    GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget> AV11targets )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         NOTIFICATIONSGRID_nCurrentRecord = 0;
         RF3Q2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrNotificationsgrid_refresh */
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
         RF3Q2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavGridstatus_Enabled = 0;
         edtavCtldatetime_Enabled = 0;
         edtavGridtitle_Enabled = 0;
         edtavCtltext_Enabled = 0;
         edtavCtlindex_Enabled = 0;
         edtavCtlkind_Enabled = 0;
      }

      protected void RF3Q2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            NotificationsgridContainer.ClearRows();
         }
         wbStart = 13;
         /* Execute user event: Refresh */
         E133Q2 ();
         nGXsfl_13_idx = 1;
         sGXsfl_13_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_13_idx), 4, 0), 4, "0");
         SubsflControlProps_132( ) ;
         bGXsfl_13_Refreshing = true;
         NotificationsgridContainer.AddObjectProperty("GridName", "Notificationsgrid");
         NotificationsgridContainer.AddObjectProperty("CmpContext", "");
         NotificationsgridContainer.AddObjectProperty("InMasterPage", "false");
         NotificationsgridContainer.AddObjectProperty("Class", "Grid");
         NotificationsgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         NotificationsgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         NotificationsgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subNotificationsgrid_Backcolorstyle), 1, 0, ".", "")));
         NotificationsgridContainer.PageSize = subNotificationsgrid_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_132( ) ;
            /* Execute user event: Notificationsgrid.Load */
            E143Q2 ();
            if ( ( subNotificationsgrid_Islastpage == 0 ) && ( NOTIFICATIONSGRID_nCurrentRecord > 0 ) && ( NOTIFICATIONSGRID_nGridOutOfScope == 0 ) && ( nGXsfl_13_idx == 1 ) )
            {
               NOTIFICATIONSGRID_nCurrentRecord = 0;
               NOTIFICATIONSGRID_nGridOutOfScope = 1;
               subnotificationsgrid_firstpage( ) ;
               /* Execute user event: Notificationsgrid.Load */
               E143Q2 ();
            }
            wbEnd = 13;
            WB3Q0( ) ;
         }
         bGXsfl_13_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes3Q2( )
      {
      }

      protected int subNotificationsgrid_fnc_Pagecount( )
      {
         NOTIFICATIONSGRID_nRecordCount = subNotificationsgrid_fnc_Recordcount( );
         if ( ((long)((NOTIFICATIONSGRID_nRecordCount) % (subNotificationsgrid_fnc_Recordsperpage( )))) == 0 )
         {
            return (int)(NumberUtil.Int( (long)(Math.Round(NOTIFICATIONSGRID_nRecordCount/ (decimal)(subNotificationsgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))) ;
         }
         return (int)(NumberUtil.Int( (long)(Math.Round(NOTIFICATIONSGRID_nRecordCount/ (decimal)(subNotificationsgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected int subNotificationsgrid_fnc_Recordcount( )
      {
         return AV11targets.Count ;
      }

      protected int subNotificationsgrid_fnc_Recordsperpage( )
      {
         return (int)(50*1) ;
      }

      protected int subNotificationsgrid_fnc_Currentpage( )
      {
         return (int)(NumberUtil.Int( (long)(Math.Round(NOTIFICATIONSGRID_nFirstRecordOnPage/ (decimal)(subNotificationsgrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected short subnotificationsgrid_firstpage( )
      {
         NOTIFICATIONSGRID_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, "NOTIFICATIONSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(NOTIFICATIONSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrNotificationsgrid_refresh( subNotificationsgrid_Rows, AV11targets) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subnotificationsgrid_nextpage( )
      {
         NOTIFICATIONSGRID_nRecordCount = subNotificationsgrid_fnc_Recordcount( );
         if ( ( NOTIFICATIONSGRID_nRecordCount >= subNotificationsgrid_fnc_Recordsperpage( ) ) && ( NOTIFICATIONSGRID_nEOF == 0 ) )
         {
            NOTIFICATIONSGRID_nFirstRecordOnPage = (long)(NOTIFICATIONSGRID_nFirstRecordOnPage+subNotificationsgrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, "NOTIFICATIONSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(NOTIFICATIONSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         NotificationsgridContainer.AddObjectProperty("NOTIFICATIONSGRID_nFirstRecordOnPage", NOTIFICATIONSGRID_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrNotificationsgrid_refresh( subNotificationsgrid_Rows, AV11targets) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((NOTIFICATIONSGRID_nEOF==0) ? 0 : 2)) ;
      }

      protected short subnotificationsgrid_previouspage( )
      {
         if ( NOTIFICATIONSGRID_nFirstRecordOnPage >= subNotificationsgrid_fnc_Recordsperpage( ) )
         {
            NOTIFICATIONSGRID_nFirstRecordOnPage = (long)(NOTIFICATIONSGRID_nFirstRecordOnPage-subNotificationsgrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, "NOTIFICATIONSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(NOTIFICATIONSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrNotificationsgrid_refresh( subNotificationsgrid_Rows, AV11targets) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subnotificationsgrid_lastpage( )
      {
         NOTIFICATIONSGRID_nRecordCount = subNotificationsgrid_fnc_Recordcount( );
         if ( NOTIFICATIONSGRID_nRecordCount > subNotificationsgrid_fnc_Recordsperpage( ) )
         {
            if ( ((long)((NOTIFICATIONSGRID_nRecordCount) % (subNotificationsgrid_fnc_Recordsperpage( )))) == 0 )
            {
               NOTIFICATIONSGRID_nFirstRecordOnPage = (long)(NOTIFICATIONSGRID_nRecordCount-subNotificationsgrid_fnc_Recordsperpage( ));
            }
            else
            {
               NOTIFICATIONSGRID_nFirstRecordOnPage = (long)(NOTIFICATIONSGRID_nRecordCount-((long)((NOTIFICATIONSGRID_nRecordCount) % (subNotificationsgrid_fnc_Recordsperpage( )))));
            }
         }
         else
         {
            NOTIFICATIONSGRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, "NOTIFICATIONSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(NOTIFICATIONSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrNotificationsgrid_refresh( subNotificationsgrid_Rows, AV11targets) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subnotificationsgrid_gotopage( int nPageNo )
      {
         if ( nPageNo > 0 )
         {
            NOTIFICATIONSGRID_nFirstRecordOnPage = (long)(subNotificationsgrid_fnc_Recordsperpage( )*(nPageNo-1));
         }
         else
         {
            NOTIFICATIONSGRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, "NOTIFICATIONSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(NOTIFICATIONSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrNotificationsgrid_refresh( subNotificationsgrid_Rows, AV11targets) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         edtavGridstatus_Enabled = 0;
         edtavCtldatetime_Enabled = 0;
         edtavGridtitle_Enabled = 0;
         edtavCtltext_Enabled = 0;
         edtavCtlindex_Enabled = 0;
         edtavCtlkind_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3Q0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "Targets"), AV11targets);
            ajax_req_read_hidden_sdt(cgiGet( "vTARGETS"), AV11targets);
            /* Read saved values. */
            nRC_GXsfl_13 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_13"), ".", ","), 18, MidpointRounding.ToEven));
            NOTIFICATIONSGRID_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( "NOTIFICATIONSGRID_nFirstRecordOnPage"), ".", ","), 18, MidpointRounding.ToEven));
            NOTIFICATIONSGRID_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( "NOTIFICATIONSGRID_nEOF"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_13 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_13"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_13_fel_idx = 0;
            while ( nGXsfl_13_fel_idx < nRC_GXsfl_13 )
            {
               nGXsfl_13_fel_idx = ((subNotificationsgrid_Islastpage==1)&&(nGXsfl_13_fel_idx+1>subNotificationsgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_13_fel_idx+1);
               sGXsfl_13_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_13_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_132( ) ;
               AV14GXV1 = (int)(nGXsfl_13_fel_idx+NOTIFICATIONSGRID_nFirstRecordOnPage);
               if ( ( AV11targets.Count >= AV14GXV1 ) && ( AV14GXV1 > 0 ) )
               {
                  AV11targets.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtNotificationTarget)AV11targets.Item(AV14GXV1));
                  AV7gridStatus = cgiGet( edtavGridstatus_Internalname);
                  AV13gridTitle = cgiGet( edtavGridtitle_Internalname);
               }
            }
            if ( nGXsfl_13_fel_idx == 0 )
            {
               nGXsfl_13_idx = 1;
               sGXsfl_13_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_13_idx), 4, 0), 4, "0");
               SubsflControlProps_132( ) ;
            }
            nGXsfl_13_fel_idx = 1;
            /* Read variables values. */
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

      protected void E133Q2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.registered.getnotifications(context ).execute( out  AV11targets, out  AV12unread) ;
         gx_BV13 = true;
         if ( AV12unread > 0 )
         {
            lblTbsummary_Caption = StringUtil.Format( "%1 unread", StringUtil.Trim( StringUtil.Str( (decimal)(AV12unread), 4, 0)), "", "", "", "", "", "", "", "");
            AssignProp("", false, lblTbsummary_Internalname, "Caption", lblTbsummary_Caption, true);
         }
         else
         {
            lblTbsummary_Caption = "No unread notifications";
            AssignProp("", false, lblTbsummary_Internalname, "Caption", lblTbsummary_Caption, true);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV11targets", AV11targets);
      }

      private void E143Q2( )
      {
         /* Notificationsgrid_Load Routine */
         returnInSub = false;
         AV14GXV1 = 1;
         while ( AV14GXV1 <= AV11targets.Count )
         {
            AV11targets.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtNotificationTarget)AV11targets.Item(AV14GXV1));
            if ( ((GeneXus.Programs.wallet.registered.SdtNotificationTarget)(AV11targets.CurrentItem)).gxTpr_Isread )
            {
               AV7gridStatus = "";
               AssignAttri("", false, edtavGridstatus_Internalname, AV7gridStatus);
            }
            else
            {
               AV7gridStatus = "New";
               AssignAttri("", false, edtavGridstatus_Internalname, AV7gridStatus);
            }
            AV13gridTitle = StringUtil.Trim( ((GeneXus.Programs.wallet.registered.SdtNotificationTarget)(AV11targets.CurrentItem)).gxTpr_Title);
            AssignAttri("", false, edtavGridtitle_Internalname, AV13gridTitle);
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 13;
            }
            if ( ( subNotificationsgrid_Islastpage == 1 ) || ( 50 == 0 ) || ( ( NOTIFICATIONSGRID_nCurrentRecord >= NOTIFICATIONSGRID_nFirstRecordOnPage ) && ( NOTIFICATIONSGRID_nCurrentRecord < NOTIFICATIONSGRID_nFirstRecordOnPage + subNotificationsgrid_fnc_Recordsperpage( ) ) ) )
            {
               sendrow_132( ) ;
            }
            NOTIFICATIONSGRID_nEOF = (short)(((subNotificationsgrid_Rows==0)||(NOTIFICATIONSGRID_nCurrentRecord<NOTIFICATIONSGRID_nFirstRecordOnPage+subNotificationsgrid_fnc_Recordsperpage( )) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, "NOTIFICATIONSGRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(NOTIFICATIONSGRID_nEOF), 1, 0, ".", "")));
            NOTIFICATIONSGRID_nCurrentRecord = (long)(NOTIFICATIONSGRID_nCurrentRecord+1);
            if ( isFullAjaxMode( ) && ! bGXsfl_13_Refreshing )
            {
               DoAjaxLoad(13, NotificationsgridRow);
            }
            AV14GXV1 = (int)(AV14GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E153Q2( )
      {
         AV14GXV1 = (int)(nGXsfl_13_idx+NOTIFICATIONSGRID_nFirstRecordOnPage);
         if ( ( AV14GXV1 > 0 ) && ( AV11targets.Count >= AV14GXV1 ) )
         {
            AV11targets.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtNotificationTarget)AV11targets.Item(AV14GXV1));
         }
         /* 'Open Notification' Routine */
         returnInSub = false;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( ((GeneXus.Programs.wallet.registered.SdtNotificationTarget)(AV11targets.CurrentItem)).gxTpr_Kind))) )
         {
            CallWebObject(formatLink("wallet.registered.gotonotification", new object[] {UrlEncode(StringUtil.LTrimStr(((GeneXus.Programs.wallet.registered.SdtNotificationTarget)(AV11targets.CurrentItem)).gxTpr_Index,8,0))}, new string[] {"index"}) );
            context.wjLocDisableFrm = 1;
         }
      }

      protected void E113Q2( )
      {
         /* 'Mark All Read' Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.registered.updatenotifications(context ).execute(  "markAllRead", out  AV5error) ;
         context.DoAjaxRefresh();
         /*  Sending Event outputs  */
         if ( gx_BV13 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV11targets", AV11targets);
            nGXsfl_13_bak_idx = nGXsfl_13_idx;
            gxgrNotificationsgrid_refresh( subNotificationsgrid_Rows, AV11targets) ;
            nGXsfl_13_idx = nGXsfl_13_bak_idx;
            sGXsfl_13_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_13_idx), 4, 0), 4, "0");
            SubsflControlProps_132( ) ;
         }
      }

      protected void E123Q2( )
      {
         /* 'Delete Read' Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.registered.updatenotifications(context ).execute(  "deleteRead", out  AV5error) ;
         context.DoAjaxRefresh();
         /*  Sending Event outputs  */
         if ( gx_BV13 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV11targets", AV11targets);
            nGXsfl_13_bak_idx = nGXsfl_13_idx;
            gxgrNotificationsgrid_refresh( subNotificationsgrid_Rows, AV11targets) ;
            nGXsfl_13_idx = nGXsfl_13_bak_idx;
            sGXsfl_13_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_13_idx), 4, 0), 4, "0");
            SubsflControlProps_132( ) ;
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
         PA3Q2( ) ;
         WS3Q2( ) ;
         WE3Q2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202610301327442", true, true, false);
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
         context.AddJavascriptSource("wallet/registered/notifications.js", "?202610301327442", false, true, false);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_132( )
      {
         edtavGridstatus_Internalname = "vGRIDSTATUS_"+sGXsfl_13_idx;
         edtavCtldatetime_Internalname = "CTLDATETIME_"+sGXsfl_13_idx;
         edtavGridtitle_Internalname = "vGRIDTITLE_"+sGXsfl_13_idx;
         edtavCtltext_Internalname = "CTLTEXT_"+sGXsfl_13_idx;
         edtavCtlindex_Internalname = "CTLINDEX_"+sGXsfl_13_idx;
         edtavCtlkind_Internalname = "CTLKIND_"+sGXsfl_13_idx;
      }

      protected void SubsflControlProps_fel_132( )
      {
         edtavGridstatus_Internalname = "vGRIDSTATUS_"+sGXsfl_13_fel_idx;
         edtavCtldatetime_Internalname = "CTLDATETIME_"+sGXsfl_13_fel_idx;
         edtavGridtitle_Internalname = "vGRIDTITLE_"+sGXsfl_13_fel_idx;
         edtavCtltext_Internalname = "CTLTEXT_"+sGXsfl_13_fel_idx;
         edtavCtlindex_Internalname = "CTLINDEX_"+sGXsfl_13_fel_idx;
         edtavCtlkind_Internalname = "CTLKIND_"+sGXsfl_13_fel_idx;
      }

      protected void sendrow_132( )
      {
         sGXsfl_13_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_13_idx), 4, 0), 4, "0");
         SubsflControlProps_132( ) ;
         WB3Q0( ) ;
         if ( ( 50 * 1 == 0 ) || ( nGXsfl_13_idx <= subNotificationsgrid_fnc_Recordsperpage( ) * 1 ) )
         {
            NotificationsgridRow = GXWebRow.GetNew(context,NotificationsgridContainer);
            if ( subNotificationsgrid_Backcolorstyle == 0 )
            {
               /* None style subfile background logic. */
               subNotificationsgrid_Backstyle = 0;
               if ( StringUtil.StrCmp(subNotificationsgrid_Class, "") != 0 )
               {
                  subNotificationsgrid_Linesclass = subNotificationsgrid_Class+"Odd";
               }
            }
            else if ( subNotificationsgrid_Backcolorstyle == 1 )
            {
               /* Uniform style subfile background logic. */
               subNotificationsgrid_Backstyle = 0;
               subNotificationsgrid_Backcolor = subNotificationsgrid_Allbackcolor;
               if ( StringUtil.StrCmp(subNotificationsgrid_Class, "") != 0 )
               {
                  subNotificationsgrid_Linesclass = subNotificationsgrid_Class+"Uniform";
               }
            }
            else if ( subNotificationsgrid_Backcolorstyle == 2 )
            {
               /* Header style subfile background logic. */
               subNotificationsgrid_Backstyle = 1;
               if ( StringUtil.StrCmp(subNotificationsgrid_Class, "") != 0 )
               {
                  subNotificationsgrid_Linesclass = subNotificationsgrid_Class+"Odd";
               }
               subNotificationsgrid_Backcolor = (int)(0x0);
            }
            else if ( subNotificationsgrid_Backcolorstyle == 3 )
            {
               /* Report style subfile background logic. */
               subNotificationsgrid_Backstyle = 1;
               if ( ((int)((nGXsfl_13_idx) % (2))) == 0 )
               {
                  subNotificationsgrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subNotificationsgrid_Class, "") != 0 )
                  {
                     subNotificationsgrid_Linesclass = subNotificationsgrid_Class+"Even";
                  }
               }
               else
               {
                  subNotificationsgrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subNotificationsgrid_Class, "") != 0 )
                  {
                     subNotificationsgrid_Linesclass = subNotificationsgrid_Class+"Odd";
                  }
               }
            }
            if ( NotificationsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<tr ") ;
               context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
               context.WriteHtmlText( " gxrow=\""+sGXsfl_13_idx+"\">") ;
            }
            /* Subfile cell */
            if ( NotificationsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 14,'',false,'" + sGXsfl_13_idx + "',13)\"";
            ROClassString = "Attribute";
            NotificationsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavGridstatus_Internalname,(string)AV7gridStatus,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,14);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavGridstatus_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"no-wrap",(string)"",(short)-1,(int)edtavGridstatus_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)13,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( NotificationsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 15,'',false,'" + sGXsfl_13_idx + "',13)\"";
            ROClassString = "Attribute";
            NotificationsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtldatetime_Internalname,context.localUtil.TToC( ((GeneXus.Programs.wallet.registered.SdtNotificationTarget)AV11targets.Item(AV14GXV1)).gxTpr_Datetime, 10, 8, 1, 2, "/", ":", " "),context.localUtil.Format( ((GeneXus.Programs.wallet.registered.SdtNotificationTarget)AV11targets.Item(AV14GXV1)).gxTpr_Datetime, "99/99/99 99:99"),TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,1);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'MDY',5,12,'eng',false,1);"+";gx.evt.onblur(this,15);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtldatetime_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"no-wrap",(string)"",(short)-1,(int)edtavCtldatetime_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)17,(short)0,(short)0,(short)13,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( NotificationsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 16,'',false,'" + sGXsfl_13_idx + "',13)\"";
            ROClassString = "Attribute";
            NotificationsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavGridtitle_Internalname,(string)AV13gridTitle,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,16);\"","'"+""+"'"+",false,"+"'"+"E\\'OPEN NOTIFICATION\\'."+sGXsfl_13_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavGridtitle_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"no-wrap",(string)"",(short)-1,(int)edtavGridtitle_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)100,(short)0,(short)0,(short)13,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( NotificationsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'',false,'" + sGXsfl_13_idx + "',13)\"";
            ROClassString = "Attribute";
            NotificationsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtltext_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtNotificationTarget)AV11targets.Item(AV14GXV1)).gxTpr_Text),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,17);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtltext_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtltext_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)500,(short)0,(short)0,(short)13,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( NotificationsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+"display:none;"+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            NotificationsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlindex_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(((GeneXus.Programs.wallet.registered.SdtNotificationTarget)AV11targets.Item(AV14GXV1)).gxTpr_Index), 8, 0, ".", "")),StringUtil.LTrim( ((edtavCtlindex_Enabled!=0) ? context.localUtil.Format( (decimal)(((GeneXus.Programs.wallet.registered.SdtNotificationTarget)AV11targets.Item(AV14GXV1)).gxTpr_Index), "ZZZZZZZ9") : context.localUtil.Format( (decimal)(((GeneXus.Programs.wallet.registered.SdtNotificationTarget)AV11targets.Item(AV14GXV1)).gxTpr_Index), "ZZZZZZZ9")))," dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+""+" onchange=\""+"gx.num.valid_integer( this,',');"+";gx.evt.onchange(this, event)\" ",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlindex_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtlindex_Enabled,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)8,(short)0,(short)0,(short)13,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( NotificationsgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+"display:none;"+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            NotificationsgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlkind_Internalname,((GeneXus.Programs.wallet.registered.SdtNotificationTarget)AV11targets.Item(AV14GXV1)).gxTpr_Kind,(string)"",""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlkind_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtlkind_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)13,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            send_integrity_lvl_hashes3Q2( ) ;
            NotificationsgridContainer.AddRow(NotificationsgridRow);
            nGXsfl_13_idx = ((subNotificationsgrid_Islastpage==1)&&(nGXsfl_13_idx+1>subNotificationsgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_13_idx+1);
            sGXsfl_13_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_13_idx), 4, 0), 4, "0");
            SubsflControlProps_132( ) ;
         }
         /* End function sendrow_132 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl13( )
      {
         if ( NotificationsgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"NotificationsgridContainer"+"DivS\" data-gxgridid=\"13\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subNotificationsgrid_Internalname, subNotificationsgrid_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subNotificationsgrid_Backcolorstyle == 0 )
            {
               subNotificationsgrid_Titlebackstyle = 0;
               if ( StringUtil.Len( subNotificationsgrid_Class) > 0 )
               {
                  subNotificationsgrid_Linesclass = subNotificationsgrid_Class+"Title";
               }
            }
            else
            {
               subNotificationsgrid_Titlebackstyle = 1;
               if ( subNotificationsgrid_Backcolorstyle == 1 )
               {
                  subNotificationsgrid_Titlebackcolor = subNotificationsgrid_Allbackcolor;
                  if ( StringUtil.Len( subNotificationsgrid_Class) > 0 )
                  {
                     subNotificationsgrid_Linesclass = subNotificationsgrid_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subNotificationsgrid_Class) > 0 )
                  {
                     subNotificationsgrid_Linesclass = subNotificationsgrid_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Date") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Notification") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "index") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "kind") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            NotificationsgridContainer.AddObjectProperty("GridName", "Notificationsgrid");
         }
         else
         {
            NotificationsgridContainer.AddObjectProperty("GridName", "Notificationsgrid");
            NotificationsgridContainer.AddObjectProperty("Header", subNotificationsgrid_Header);
            NotificationsgridContainer.AddObjectProperty("Class", "Grid");
            NotificationsgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            NotificationsgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            NotificationsgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subNotificationsgrid_Backcolorstyle), 1, 0, ".", "")));
            NotificationsgridContainer.AddObjectProperty("CmpContext", "");
            NotificationsgridContainer.AddObjectProperty("InMasterPage", "false");
            NotificationsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            NotificationsgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV7gridStatus));
            NotificationsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavGridstatus_Enabled), 5, 0, ".", "")));
            NotificationsgridContainer.AddColumnProperties(NotificationsgridColumn);
            NotificationsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            NotificationsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtldatetime_Enabled), 5, 0, ".", "")));
            NotificationsgridContainer.AddColumnProperties(NotificationsgridColumn);
            NotificationsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            NotificationsgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV13gridTitle));
            NotificationsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavGridtitle_Enabled), 5, 0, ".", "")));
            NotificationsgridContainer.AddColumnProperties(NotificationsgridColumn);
            NotificationsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            NotificationsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtltext_Enabled), 5, 0, ".", "")));
            NotificationsgridContainer.AddColumnProperties(NotificationsgridColumn);
            NotificationsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            NotificationsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlindex_Enabled), 5, 0, ".", "")));
            NotificationsgridContainer.AddColumnProperties(NotificationsgridColumn);
            NotificationsgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            NotificationsgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlkind_Enabled), 5, 0, ".", "")));
            NotificationsgridContainer.AddColumnProperties(NotificationsgridColumn);
            NotificationsgridContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subNotificationsgrid_Selectedindex), 4, 0, ".", "")));
            NotificationsgridContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subNotificationsgrid_Allowselection), 1, 0, ".", "")));
            NotificationsgridContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subNotificationsgrid_Selectioncolor), 9, 0, ".", "")));
            NotificationsgridContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subNotificationsgrid_Allowhovering), 1, 0, ".", "")));
            NotificationsgridContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subNotificationsgrid_Hoveringcolor), 9, 0, ".", "")));
            NotificationsgridContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subNotificationsgrid_Allowcollapsing), 1, 0, ".", "")));
            NotificationsgridContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subNotificationsgrid_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         lblTbsummary_Internalname = "TBSUMMARY";
         bttMarkallread_Internalname = "MARKALLREAD";
         bttDeleteread_Internalname = "DELETEREAD";
         edtavGridstatus_Internalname = "vGRIDSTATUS";
         edtavCtldatetime_Internalname = "CTLDATETIME";
         edtavGridtitle_Internalname = "vGRIDTITLE";
         edtavCtltext_Internalname = "CTLTEXT";
         edtavCtlindex_Internalname = "CTLINDEX";
         edtavCtlkind_Internalname = "CTLKIND";
         divMaintable_Internalname = "MAINTABLE";
         Form.Internalname = "FORM";
         subNotificationsgrid_Internalname = "NOTIFICATIONSGRID";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subNotificationsgrid_Allowcollapsing = 0;
         subNotificationsgrid_Allowselection = 0;
         subNotificationsgrid_Header = "";
         edtavCtlkind_Jsonclick = "";
         edtavCtlkind_Enabled = 0;
         edtavCtlindex_Jsonclick = "";
         edtavCtlindex_Enabled = 0;
         edtavCtltext_Jsonclick = "";
         edtavCtltext_Enabled = 0;
         edtavGridtitle_Jsonclick = "";
         edtavGridtitle_Enabled = 1;
         edtavCtldatetime_Jsonclick = "";
         edtavCtldatetime_Enabled = 0;
         edtavGridstatus_Jsonclick = "";
         edtavGridstatus_Enabled = 1;
         subNotificationsgrid_Class = "Grid";
         subNotificationsgrid_Backcolorstyle = 0;
         edtavCtlkind_Enabled = -1;
         edtavCtlindex_Enabled = -1;
         edtavCtltext_Enabled = -1;
         edtavCtldatetime_Enabled = -1;
         lblTbsummary_Caption = "TBSummary";
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Notifications";
         subNotificationsgrid_Rows = 50;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"NOTIFICATIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"NOTIFICATIONSGRID_nEOF","type":"int"},{"av":"subNotificationsgrid_Rows","ctrl":"NOTIFICATIONSGRID","prop":"Rows"},{"av":"AV11targets","fld":"vTARGETS","grid":13,"type":""},{"av":"nGXsfl_13_idx","ctrl":"GRID","prop":"GridCurrRow","grid":13},{"av":"nRC_GXsfl_13","ctrl":"NOTIFICATIONSGRID","prop":"GridRC","grid":13,"type":"int"}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV11targets","fld":"vTARGETS","grid":13,"type":""},{"av":"nGXsfl_13_idx","ctrl":"GRID","prop":"GridCurrRow","grid":13},{"av":"NOTIFICATIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_13","ctrl":"NOTIFICATIONSGRID","prop":"GridRC","grid":13,"type":"int"},{"av":"lblTbsummary_Caption","ctrl":"TBSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("NOTIFICATIONSGRID.LOAD","""{"handler":"E143Q2","iparms":[{"av":"AV11targets","fld":"vTARGETS","grid":13,"type":""},{"av":"nGXsfl_13_idx","ctrl":"GRID","prop":"GridCurrRow","grid":13},{"av":"NOTIFICATIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_13","ctrl":"NOTIFICATIONSGRID","prop":"GridRC","grid":13,"type":"int"}]""");
         setEventMetadata("NOTIFICATIONSGRID.LOAD",""","oparms":[{"av":"AV7gridStatus","fld":"vGRIDSTATUS","type":"svchar"},{"av":"AV13gridTitle","fld":"vGRIDTITLE","type":"svchar"}]}""");
         setEventMetadata("'OPEN NOTIFICATION'","""{"handler":"E153Q2","iparms":[{"av":"AV11targets","fld":"vTARGETS","grid":13,"type":""},{"av":"nGXsfl_13_idx","ctrl":"GRID","prop":"GridCurrRow","grid":13},{"av":"NOTIFICATIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_13","ctrl":"NOTIFICATIONSGRID","prop":"GridRC","grid":13,"type":"int"}]}""");
         setEventMetadata("'MARK ALL READ'","""{"handler":"E113Q2","iparms":[{"av":"NOTIFICATIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"NOTIFICATIONSGRID_nEOF","type":"int"},{"av":"subNotificationsgrid_Rows","ctrl":"NOTIFICATIONSGRID","prop":"Rows"},{"av":"AV11targets","fld":"vTARGETS","grid":13,"type":""},{"av":"nGXsfl_13_idx","ctrl":"GRID","prop":"GridCurrRow","grid":13},{"av":"nRC_GXsfl_13","ctrl":"NOTIFICATIONSGRID","prop":"GridRC","grid":13,"type":"int"}]""");
         setEventMetadata("'MARK ALL READ'",""","oparms":[{"av":"AV11targets","fld":"vTARGETS","grid":13,"type":""},{"av":"nGXsfl_13_idx","ctrl":"GRID","prop":"GridCurrRow","grid":13},{"av":"NOTIFICATIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_13","ctrl":"NOTIFICATIONSGRID","prop":"GridRC","grid":13,"type":"int"},{"av":"lblTbsummary_Caption","ctrl":"TBSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("'DELETE READ'","""{"handler":"E123Q2","iparms":[{"av":"NOTIFICATIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"NOTIFICATIONSGRID_nEOF","type":"int"},{"av":"subNotificationsgrid_Rows","ctrl":"NOTIFICATIONSGRID","prop":"Rows"},{"av":"AV11targets","fld":"vTARGETS","grid":13,"type":""},{"av":"nGXsfl_13_idx","ctrl":"GRID","prop":"GridCurrRow","grid":13},{"av":"nRC_GXsfl_13","ctrl":"NOTIFICATIONSGRID","prop":"GridRC","grid":13,"type":"int"}]""");
         setEventMetadata("'DELETE READ'",""","oparms":[{"av":"AV11targets","fld":"vTARGETS","grid":13,"type":""},{"av":"nGXsfl_13_idx","ctrl":"GRID","prop":"GridCurrRow","grid":13},{"av":"NOTIFICATIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_13","ctrl":"NOTIFICATIONSGRID","prop":"GridRC","grid":13,"type":"int"},{"av":"lblTbsummary_Caption","ctrl":"TBSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("NOTIFICATIONSGRID_FIRSTPAGE","""{"handler":"subnotificationsgrid_firstpage","iparms":[{"av":"NOTIFICATIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"NOTIFICATIONSGRID_nEOF","type":"int"},{"av":"subNotificationsgrid_Rows","ctrl":"NOTIFICATIONSGRID","prop":"Rows"},{"av":"AV11targets","fld":"vTARGETS","grid":13,"type":""},{"av":"nGXsfl_13_idx","ctrl":"GRID","prop":"GridCurrRow","grid":13},{"av":"nRC_GXsfl_13","ctrl":"NOTIFICATIONSGRID","prop":"GridRC","grid":13,"type":"int"}]""");
         setEventMetadata("NOTIFICATIONSGRID_FIRSTPAGE",""","oparms":[{"av":"AV11targets","fld":"vTARGETS","grid":13,"type":""},{"av":"nGXsfl_13_idx","ctrl":"GRID","prop":"GridCurrRow","grid":13},{"av":"NOTIFICATIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_13","ctrl":"NOTIFICATIONSGRID","prop":"GridRC","grid":13,"type":"int"},{"av":"lblTbsummary_Caption","ctrl":"TBSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("NOTIFICATIONSGRID_PREVPAGE","""{"handler":"subnotificationsgrid_previouspage","iparms":[{"av":"NOTIFICATIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"NOTIFICATIONSGRID_nEOF","type":"int"},{"av":"subNotificationsgrid_Rows","ctrl":"NOTIFICATIONSGRID","prop":"Rows"},{"av":"AV11targets","fld":"vTARGETS","grid":13,"type":""},{"av":"nGXsfl_13_idx","ctrl":"GRID","prop":"GridCurrRow","grid":13},{"av":"nRC_GXsfl_13","ctrl":"NOTIFICATIONSGRID","prop":"GridRC","grid":13,"type":"int"}]""");
         setEventMetadata("NOTIFICATIONSGRID_PREVPAGE",""","oparms":[{"av":"AV11targets","fld":"vTARGETS","grid":13,"type":""},{"av":"nGXsfl_13_idx","ctrl":"GRID","prop":"GridCurrRow","grid":13},{"av":"NOTIFICATIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_13","ctrl":"NOTIFICATIONSGRID","prop":"GridRC","grid":13,"type":"int"},{"av":"lblTbsummary_Caption","ctrl":"TBSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("NOTIFICATIONSGRID_NEXTPAGE","""{"handler":"subnotificationsgrid_nextpage","iparms":[{"av":"NOTIFICATIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"NOTIFICATIONSGRID_nEOF","type":"int"},{"av":"subNotificationsgrid_Rows","ctrl":"NOTIFICATIONSGRID","prop":"Rows"},{"av":"AV11targets","fld":"vTARGETS","grid":13,"type":""},{"av":"nGXsfl_13_idx","ctrl":"GRID","prop":"GridCurrRow","grid":13},{"av":"nRC_GXsfl_13","ctrl":"NOTIFICATIONSGRID","prop":"GridRC","grid":13,"type":"int"}]""");
         setEventMetadata("NOTIFICATIONSGRID_NEXTPAGE",""","oparms":[{"av":"AV11targets","fld":"vTARGETS","grid":13,"type":""},{"av":"nGXsfl_13_idx","ctrl":"GRID","prop":"GridCurrRow","grid":13},{"av":"NOTIFICATIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_13","ctrl":"NOTIFICATIONSGRID","prop":"GridRC","grid":13,"type":"int"},{"av":"lblTbsummary_Caption","ctrl":"TBSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("NOTIFICATIONSGRID_LASTPAGE","""{"handler":"subnotificationsgrid_lastpage","iparms":[{"av":"NOTIFICATIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"NOTIFICATIONSGRID_nEOF","type":"int"},{"av":"subNotificationsgrid_Rows","ctrl":"NOTIFICATIONSGRID","prop":"Rows"},{"av":"AV11targets","fld":"vTARGETS","grid":13,"type":""},{"av":"nGXsfl_13_idx","ctrl":"GRID","prop":"GridCurrRow","grid":13},{"av":"nRC_GXsfl_13","ctrl":"NOTIFICATIONSGRID","prop":"GridRC","grid":13,"type":"int"}]""");
         setEventMetadata("NOTIFICATIONSGRID_LASTPAGE",""","oparms":[{"av":"AV11targets","fld":"vTARGETS","grid":13,"type":""},{"av":"nGXsfl_13_idx","ctrl":"GRID","prop":"GridCurrRow","grid":13},{"av":"NOTIFICATIONSGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_13","ctrl":"NOTIFICATIONSGRID","prop":"GridRC","grid":13,"type":"int"},{"av":"lblTbsummary_Caption","ctrl":"TBSUMMARY","prop":"Caption"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gxv5","iparms":[]}""");
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
         AV11targets = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget>( context, "NotificationTarget", "distributedcryptography");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         lblTbsummary_Jsonclick = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttMarkallread_Jsonclick = "";
         bttDeleteread_Jsonclick = "";
         NotificationsgridContainer = new GXWebGrid( context);
         sStyleString = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV7gridStatus = "";
         AV13gridTitle = "";
         NotificationsgridRow = new GXWebRow();
         AV5error = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subNotificationsgrid_Linesclass = "";
         ROClassString = "";
         NotificationsgridColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavGridstatus_Enabled = 0;
         edtavCtldatetime_Enabled = 0;
         edtavGridtitle_Enabled = 0;
         edtavCtltext_Enabled = 0;
         edtavCtlindex_Enabled = 0;
         edtavCtlkind_Enabled = 0;
      }

      private short NOTIFICATIONSGRID_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subNotificationsgrid_Backcolorstyle ;
      private short AV12unread ;
      private short nGXWrapped ;
      private short subNotificationsgrid_Backstyle ;
      private short subNotificationsgrid_Titlebackstyle ;
      private short subNotificationsgrid_Allowselection ;
      private short subNotificationsgrid_Allowhovering ;
      private short subNotificationsgrid_Allowcollapsing ;
      private short subNotificationsgrid_Collapsed ;
      private int nRC_GXsfl_13 ;
      private int subNotificationsgrid_Rows ;
      private int nGXsfl_13_idx=1 ;
      private int AV14GXV1 ;
      private int subNotificationsgrid_Islastpage ;
      private int edtavGridstatus_Enabled ;
      private int edtavCtldatetime_Enabled ;
      private int edtavGridtitle_Enabled ;
      private int edtavCtltext_Enabled ;
      private int edtavCtlindex_Enabled ;
      private int edtavCtlkind_Enabled ;
      private int NOTIFICATIONSGRID_nGridOutOfScope ;
      private int nGXsfl_13_fel_idx=1 ;
      private int nGXsfl_13_bak_idx=1 ;
      private int idxLst ;
      private int subNotificationsgrid_Backcolor ;
      private int subNotificationsgrid_Allbackcolor ;
      private int subNotificationsgrid_Titlebackcolor ;
      private int subNotificationsgrid_Selectedindex ;
      private int subNotificationsgrid_Selectioncolor ;
      private int subNotificationsgrid_Hoveringcolor ;
      private long NOTIFICATIONSGRID_nFirstRecordOnPage ;
      private long NOTIFICATIONSGRID_nCurrentRecord ;
      private long NOTIFICATIONSGRID_nRecordCount ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_13_idx="0001" ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string lblTbsummary_Internalname ;
      private string lblTbsummary_Caption ;
      private string lblTbsummary_Jsonclick ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttMarkallread_Internalname ;
      private string bttMarkallread_Jsonclick ;
      private string bttDeleteread_Internalname ;
      private string bttDeleteread_Jsonclick ;
      private string sStyleString ;
      private string subNotificationsgrid_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string edtavGridstatus_Internalname ;
      private string edtavGridtitle_Internalname ;
      private string sGXsfl_13_fel_idx="0001" ;
      private string AV5error ;
      private string edtavCtldatetime_Internalname ;
      private string edtavCtltext_Internalname ;
      private string edtavCtlindex_Internalname ;
      private string edtavCtlkind_Internalname ;
      private string subNotificationsgrid_Class ;
      private string subNotificationsgrid_Linesclass ;
      private string ROClassString ;
      private string edtavGridstatus_Jsonclick ;
      private string edtavCtldatetime_Jsonclick ;
      private string edtavGridtitle_Jsonclick ;
      private string edtavCtltext_Jsonclick ;
      private string edtavCtlindex_Jsonclick ;
      private string edtavCtlkind_Jsonclick ;
      private string subNotificationsgrid_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_13_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool gx_refresh_fired ;
      private bool returnInSub ;
      private bool gx_BV13 ;
      private string AV7gridStatus ;
      private string AV13gridTitle ;
      private GXWebGrid NotificationsgridContainer ;
      private GXWebRow NotificationsgridRow ;
      private GXWebColumn NotificationsgridColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtNotificationTarget> AV11targets ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
