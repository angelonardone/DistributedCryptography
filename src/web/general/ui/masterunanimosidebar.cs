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
namespace GeneXus.Programs.general.ui {
   public class masterunanimosidebar : GXMasterPage
   {
      public masterunanimosidebar( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
      }

      public masterunanimosidebar( IGxContext context )
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
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            PA0W2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               WS0W2( ) ;
               if ( ! isAjaxCallMode( ) )
               {
                  WE0W2( ) ;
               }
            }
         }
         cleanup();
      }

      protected void RenderHtmlHeaders( )
      {
         if ( ! isFullAjaxMode( ) )
         {
            GXWebForm.AddResponsiveMetaHeaders((getDataAreaObject() == null ? Form : getDataAreaObject().GetForm()).Meta);
            getDataAreaObject().RenderHtmlHeaders();
         }
      }

      protected void RenderHtmlOpenForm( )
      {
         if ( ! isFullAjaxMode( ) )
         {
            getDataAreaObject().RenderHtmlOpenForm();
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", true, "vSIDEBARITEMS_MPAGE", AV5sidebarItems);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vSIDEBARITEMS_MPAGE", AV5sidebarItems);
         }
         GxWebStd.gx_hidden_field( context, "vTEXT_MPAGE", AV18text);
         GxWebStd.gx_hidden_field( context, "vTITLE_MPAGE", AV17title);
         GxWebStd.gx_hidden_field( context, "vTOASTRTYPE_MPAGE", AV16toastrType);
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", true, "vNOTIFICATIONINFO_MPAGE", AV9notificationInfo);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vNOTIFICATIONINFO_MPAGE", AV9notificationInfo);
         }
         GxWebStd.gx_hidden_field( context, "vTARGET_MPAGE", AV6target);
         GxWebStd.gx_hidden_field( context, "ICON1_MPAGE_Source", StringUtil.RTrim( Icon1_Source));
         GxWebStd.gx_hidden_field( context, "ICON1_MPAGE_Size", StringUtil.RTrim( Icon1_Size));
         GxWebStd.gx_hidden_field( context, "ICON1_MPAGE_Color", StringUtil.RTrim( Icon1_Color));
         GxWebStd.gx_hidden_field( context, "ICON1_MPAGE_Visible", StringUtil.BoolToStr( Icon1_Visible));
         GxWebStd.gx_hidden_field( context, "SIDEBARMENU_MPAGE_Footertext", StringUtil.RTrim( Sidebarmenu_Footertext));
         GxWebStd.gx_hidden_field( context, "SIDEBARMENU_MPAGE_Distancetotop", StringUtil.LTrim( StringUtil.NToC( (decimal)(Sidebarmenu_Distancetotop), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "SIDEBARMENU_MPAGE_Selecteditemtarget", StringUtil.RTrim( Sidebarmenu_Selecteditemtarget));
         GxWebStd.gx_hidden_field( context, "SIDEBARMENU_MPAGE_Iscollapsed", StringUtil.BoolToStr( Sidebarmenu_Iscollapsed));
         GxWebStd.gx_hidden_field( context, "ICON1_MPAGE_Source", StringUtil.RTrim( Icon1_Source));
         GxWebStd.gx_hidden_field( context, "ICON1_MPAGE_Source", StringUtil.RTrim( Icon1_Source));
      }

      protected void RenderHtmlCloseForm0W2( )
      {
         SendCloseFormHiddens( ) ;
         SendSecurityToken((string)(sPrefix));
         if ( ! isFullAjaxMode( ) )
         {
            getDataAreaObject().RenderHtmlCloseForm();
         }
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         if ( ! ( WebComp_Comp_message_dispatcher == null ) )
         {
            WebComp_Comp_message_dispatcher.componentjscripts();
         }
         context.AddJavascriptSource("Unanimo_chameleon/chameleon-loader.js", "", false, true);
         context.AddJavascriptSource("UserControls/GeneXusUnanimo.IconRender.js", "", false, true);
         context.AddJavascriptSource("Toastr/toastr.min.js", "", false, true);
         context.AddJavascriptSource("Toastr/ToastrRender.js", "", false, true);
         context.AddJavascriptSource("Unanimo_chameleon/chameleon-loader.js", "", false, true);
         context.AddJavascriptSource("UserControls/GeneXusUnanimo.SidebarRender.js", "", false, true);
         context.AddJavascriptSource("general/ui/masterunanimosidebar.js", "?202531412514334", false, true);
         context.WriteHtmlTextNl( "</body>") ;
         context.WriteHtmlTextNl( "</html>") ;
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
      }

      public override string GetPgmname( )
      {
         return "General.UI.MasterUnanimoSidebar" ;
      }

      public override string GetPgmdesc( )
      {
         return "Master Unanimo Sidebar" ;
      }

      protected void WB0W0( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            RenderHtmlHeaders( ) ;
            RenderHtmlOpenForm( ) ;
            if ( ! ShowMPWhenPopUp( ) && context.isPopUpObject( ) )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableOutput();
               }
               if ( context.isSpaRequest( ) )
               {
                  disableJsOutput();
               }
               /* Content placeholder */
               context.WriteHtmlText( "<div") ;
               GxWebStd.ClassAttribute( context, "gx-content-placeholder");
               context.WriteHtmlText( ">") ;
               if ( ! isFullAjaxMode( ) )
               {
                  getDataAreaObject().RenderHtmlContent();
               }
               context.WriteHtmlText( "</div>") ;
               if ( context.isSpaRequest( ) )
               {
                  disableOutput();
               }
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
               wbLoad = true;
               return  ;
            }
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "MainContainer", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divHeader_Internalname, 1, 0, "px", 0, "px", "HeaderContainer ContainerFluid", "start", "top", " "+"data-gx-flex"+" ", "align-items:center;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 24, "px", "", "Center", "top", "", "", "div");
            /* Static images/pictures */
            ClassString = "sidebar__toggle-ico" + " " + ((StringUtil.StrCmp(imgImage2_gximage, "")==0) ? "GX_Image_GeneXusUnanimo_list_view_white_Class" : "GX_Image_"+imgImage2_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "ef73725b-c186-4f57-94bc-867e0a183c3e", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgImage2_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" ", "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_General/UI/MasterUnanimoSidebar.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "min-height:20px;", "div");
            /* Active images/pictures */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 10,'',true,'',0)\"";
            ClassString = "header__logo" + " " + ((StringUtil.StrCmp(imgImage1_gximage, "")==0) ? "GX_Image_DClogohorizontal_Class" : "GX_Image_"+imgImage1_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "d1cbde72-6a80-44eb-8b1b-132677480e7f", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgImage1_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 7, imgImage1_Jsonclick, "'"+""+"'"+",true,"+"'"+"e110w1_client"+"'", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" "+TempTags, "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_General/UI/MasterUnanimoSidebar.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "Center", "top", "", "flex-grow:1;min-height:20px;", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblWalletname_Internalname, lblWalletname_Caption, "", "", lblWalletname_Jsonclick, "'"+""+"'"+",true,"+"'"+"E_MPAGE."+"'", "", "header__title", 0, "", 1, 1, 0, 0, "HLP_General/UI/MasterUnanimoSidebar.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            /* User Defined Control */
            ucIcon1.SetProperty("size", Icon1_Size);
            ucIcon1.SetProperty("color", Icon1_Color);
            ucIcon1.Render(context, "genexusunanimo.icon", Icon1_Internalname, "ICON1_MPAGEContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            /* User Defined Control */
            ucToastr1.Render(context, "toastr", Toastr1_Internalname, "TOASTR1_MPAGEContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-2", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablesidebarcontainer_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* User Defined Control */
            ucSidebarmenu.SetProperty("Title", Sidebarmenu_Title);
            ucSidebarmenu.SetProperty("SidebarItems", AV5sidebarItems);
            ucSidebarmenu.Render(context, "genexusunanimo.sidebar", Sidebarmenu_Internalname, "SIDEBARMENU_MPAGEContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divContent_Internalname, 1, 0, "px", 0, "px", divContent_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            /* Content placeholder */
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gx-content-placeholder");
            context.WriteHtmlText( ">") ;
            if ( ! isFullAjaxMode( ) )
            {
               getDataAreaObject().RenderHtmlContent();
            }
            context.WriteHtmlText( "</div>") ;
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
            if ( context.isSpaRequest( ) )
            {
               enableJsOutput();
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 hidden-xs", "start", "top", "", "", "div");
            if ( ! isFullAjaxMode( ) )
            {
               /* WebComponent */
               GxWebStd.gx_hidden_field( context, "MPW0030"+"", StringUtil.RTrim( WebComp_Comp_message_dispatcher_Component));
               context.WriteHtmlText( "<div") ;
               GxWebStd.ClassAttribute( context, "gxwebcomponent");
               context.WriteHtmlText( " id=\""+"gxHTMLWrpMPW0030"+""+"\""+"") ;
               context.WriteHtmlText( ">") ;
               if ( StringUtil.Len( WebComp_Comp_message_dispatcher_Component) != 0 )
               {
                  if ( StringUtil.StrCmp(StringUtil.Lower( OldComp_message_dispatcher), StringUtil.Lower( WebComp_Comp_message_dispatcher_Component)) != 0 )
                  {
                     context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpMPW0030"+"");
                  }
                  WebComp_Comp_message_dispatcher.componentdraw();
                  if ( StringUtil.StrCmp(StringUtil.Lower( OldComp_message_dispatcher), StringUtil.Lower( WebComp_Comp_message_dispatcher_Component)) != 0 )
                  {
                     context.httpAjaxContext.ajax_rspEndCmp();
                  }
               }
               context.WriteHtmlText( "</div>") ;
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START0W2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP0W0( ) ;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            if ( getDataAreaObject().ExecuteStartEvent() != 0 )
            {
               setAjaxCallMode();
            }
            if ( context.isSpaRequest( ) )
            {
               enableJsOutput();
            }
         }
      }

      protected void WS0W2( )
      {
         START0W2( ) ;
         EVT0W2( ) ;
      }

      protected void EVT0W2( )
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
                  if ( StringUtil.StrCmp(sEvtType, "E") == 0 )
                  {
                     sEvtType = StringUtil.Right( sEvt, 1);
                     if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                     {
                        sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                        if ( StringUtil.StrCmp(sEvt, "RFR_MPAGE") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                        }
                        else if ( StringUtil.StrCmp(sEvt, "ICON1_MPAGE.CLICK_MPAGE") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: Icon1.Click */
                           E120W2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "LOAD_MPAGE") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: Load */
                           E130W2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "GLOBALEVENTS_MPAGE.REFRESHMASTERPAGE_MPAGE") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           E140W2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "ENTER_MPAGE") == 0 )
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
                           dynload_actions( ) ;
                        }
                     }
                     else
                     {
                     }
                  }
                  else if ( StringUtil.StrCmp(sEvtType, "M") == 0 )
                  {
                     sEvtType = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-2));
                     sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-6));
                     nCmpId = (short)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                     if ( nCmpId == 30 )
                     {
                        OldComp_message_dispatcher = cgiGet( "MPW0030");
                        if ( ( StringUtil.Len( OldComp_message_dispatcher) == 0 ) || ( StringUtil.StrCmp(OldComp_message_dispatcher, WebComp_Comp_message_dispatcher_Component) != 0 ) )
                        {
                           WebComp_Comp_message_dispatcher = getWebComponent(GetType(), "GeneXus.Programs", OldComp_message_dispatcher, new Object[] {context} );
                           WebComp_Comp_message_dispatcher.ComponentInit();
                           WebComp_Comp_message_dispatcher.Name = "OldComp_message_dispatcher";
                           WebComp_Comp_message_dispatcher_Component = OldComp_message_dispatcher;
                        }
                        if ( StringUtil.Len( WebComp_Comp_message_dispatcher_Component) != 0 )
                        {
                           WebComp_Comp_message_dispatcher.componentprocess("MPW0030", "", sEvt);
                        }
                        WebComp_Comp_message_dispatcher_Component = OldComp_message_dispatcher;
                     }
                  }
                  if ( context.wbHandled == 0 )
                  {
                     getDataAreaObject().DispatchEvents();
                  }
                  context.wbHandled = 1;
               }
            }
         }
      }

      protected void WE0W2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm0W2( ) ;
            }
         }
      }

      protected void PA0W2( )
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
         RF0W2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
      }

      protected void RF0W2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( ShowMPWhenPopUp( ) || ! context.isPopUpObject( ) )
         {
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
            {
               if ( 1 != 0 )
               {
                  if ( StringUtil.Len( WebComp_Comp_message_dispatcher_Component) != 0 )
                  {
                     WebComp_Comp_message_dispatcher.componentstart();
                  }
               }
            }
            gxdyncontrolsrefreshing = true;
            fix_multi_value_controls( ) ;
            gxdyncontrolsrefreshing = false;
         }
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E130W2 ();
            WB0W0( ) ;
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
         }
      }

      protected void send_integrity_lvl_hashes0W2( )
      {
      }

      protected void before_start_formulas( )
      {
         fix_multi_value_controls( ) ;
      }

      protected void STRUP0W0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "vSIDEBARITEMS_MPAGE"), AV5sidebarItems);
            ajax_req_read_hidden_sdt(cgiGet( "vNOTIFICATIONINFO_MPAGE"), AV9notificationInfo);
            /* Read saved values. */
            AV17title = cgiGet( "vTITLE_MPAGE");
            AV18text = cgiGet( "vTEXT_MPAGE");
            AV16toastrType = cgiGet( "vTOASTRTYPE_MPAGE");
            AV6target = cgiGet( "vTARGET_MPAGE");
            Icon1_Source = cgiGet( "ICON1_MPAGE_Source");
            Icon1_Size = cgiGet( "ICON1_MPAGE_Size");
            Icon1_Color = cgiGet( "ICON1_MPAGE_Color");
            Icon1_Visible = StringUtil.StrToBool( cgiGet( "ICON1_MPAGE_Visible"));
            Sidebarmenu_Footertext = cgiGet( "SIDEBARMENU_MPAGE_Footertext");
            Sidebarmenu_Distancetotop = (int)(Math.Round(context.localUtil.CToN( cgiGet( "SIDEBARMENU_MPAGE_Distancetotop"), ".", ","), 18, MidpointRounding.ToEven));
            Icon1_Source = cgiGet( "ICON1_MPAGE_Source");
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

      protected void nextLoad( )
      {
      }

      protected void E130W2( )
      {
         /* Load Routine */
         returnInSub = false;
         GXt_SdtWallet1 = AV14wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV14wallet = GXt_SdtWallet1;
         Sidebarmenu_Footertext = "version 0.917 - © 2022-2025 Distributed Cryptography";
         ucSidebarmenu.SendProperty(context, "", true, Sidebarmenu_Internalname, "FooterText", Sidebarmenu_Footertext);
         Sidebarmenu_Distancetotop = 60;
         ucSidebarmenu.SendProperty(context, "", true, Sidebarmenu_Internalname, "DistanceToTop", StringUtil.LTrimStr( (decimal)(Sidebarmenu_Distancetotop), 9, 0));
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14wallet.gxTpr_Walletname)) )
         {
            Icon1_Visible = true;
            ucIcon1.SendProperty(context, "", true, Icon1_Internalname, "Visible", StringUtil.BoolToStr( Icon1_Visible));
            AV20designColorSchema = AV19websession.Get("DESIGN_COLOR_SCHEMA");
            if ( StringUtil.StrCmp(AV20designColorSchema, "DESIGN_COLOR_SCHEMA_DARK") == 0 )
            {
               Icon1_Source = context.convertURL( (string)(context.GetImagePath( "7ece5bb1-6e7a-4624-ad0e-af27758acc23", "", context.GetTheme( ))));
               ucIcon1.SendProperty(context, "", true, Icon1_Internalname, "source", Icon1_Source);
               Icon1_Color = "#a9acf8";
               ucIcon1.SendProperty(context, "", true, Icon1_Internalname, "color", Icon1_Color);
               this.executeExternalObjectMethod("", true, "gx.core.ds", "setOption", new Object[] {(string)"color-scheme",(string)"dark"}, false);
            }
            else
            {
               Icon1_Source = context.convertURL( (string)(context.GetImagePath( "7c119e68-c5e6-4feb-9da2-9216d99724bb", "", context.GetTheme( ))));
               ucIcon1.SendProperty(context, "", true, Icon1_Internalname, "source", Icon1_Source);
               Icon1_Color = "#696ef2";
               ucIcon1.SendProperty(context, "", true, Icon1_Internalname, "color", Icon1_Color);
               this.executeExternalObjectMethod("", true, "gx.core.ds", "setOption", new Object[] {(string)"color-scheme",(string)"light"}, false);
            }
            lblWalletname_Caption = AV14wallet.gxTpr_Walletname+" on "+AV14wallet.gxTpr_Networktype;
            AssignProp("", true, lblWalletname_Internalname, "Caption", lblWalletname_Caption, true);
            if ( StringUtil.StrCmp(AV14wallet.gxTpr_Wallettype, "BIP86") == 0 )
            {
               GXt_objcol_SdtSidebarItems_SidebarItem2 = AV5sidebarItems;
               new GeneXus.Programs.general.ui.sidebarmenu(context ).execute( out  GXt_objcol_SdtSidebarItems_SidebarItem2) ;
               AV5sidebarItems = GXt_objcol_SdtSidebarItems_SidebarItem2;
            }
            else
            {
               GXt_objcol_SdtSidebarItems_SidebarItem2 = AV5sidebarItems;
               new GeneXus.Programs.general.ui.sidebaritemsoneaddressdp(context ).execute( out  GXt_objcol_SdtSidebarItems_SidebarItem2) ;
               AV5sidebarItems = GXt_objcol_SdtSidebarItems_SidebarItem2;
            }
            /* Object Property */
            if ( true )
            {
               bDynCreated_Comp_message_dispatcher = true;
            }
            if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Comp_message_dispatcher_Component), StringUtil.Lower( "Wallet.registered.dispatchIncommingMessage")) != 0 )
            {
               WebComp_Comp_message_dispatcher = getWebComponent(GetType(), "GeneXus.Programs", "wallet.registered.dispatchincommingmessage", new Object[] {context} );
               WebComp_Comp_message_dispatcher.ComponentInit();
               WebComp_Comp_message_dispatcher.Name = "Wallet.registered.dispatchIncommingMessage";
               WebComp_Comp_message_dispatcher_Component = "Wallet.registered.dispatchIncommingMessage";
            }
            if ( StringUtil.Len( WebComp_Comp_message_dispatcher_Component) != 0 )
            {
               WebComp_Comp_message_dispatcher.setjustcreated();
               WebComp_Comp_message_dispatcher.componentprepare(new Object[] {(string)"MPW0030",(string)"",Contentholder.Pgmname});
               WebComp_Comp_message_dispatcher.componentbind(new Object[] {(string)""});
            }
            if ( isFullAjaxMode( ) || isAjaxCallMode( ) && bDynCreated_Comp_message_dispatcher )
            {
               context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpMPW0030"+"");
               WebComp_Comp_message_dispatcher.componentdraw();
               context.httpAjaxContext.ajax_rspEndCmp();
            }
         }
         else
         {
            lblWalletname_Caption = "";
            AssignProp("", true, lblWalletname_Internalname, "Caption", lblWalletname_Caption, true);
            Icon1_Visible = false;
            ucIcon1.SendProperty(context, "", true, Icon1_Internalname, "Visible", StringUtil.BoolToStr( Icon1_Visible));
         }
      }

      protected void E140W2( )
      {
         /* GlobalEvents_Refreshmasterpage Routine */
         returnInSub = false;
         context.DoAjaxRefresh();
      }

      protected void E120W2( )
      {
         /* Icon1_Click Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(Icon1_Source, context.convertURL( (string)(context.GetImagePath( "7c119e68-c5e6-4feb-9da2-9216d99724bb", "", context.GetTheme( ))))) == 0 )
         {
            Icon1_Source = context.convertURL( (string)(context.GetImagePath( "7ece5bb1-6e7a-4624-ad0e-af27758acc23", "", context.GetTheme( ))));
            ucIcon1.SendProperty(context, "", true, Icon1_Internalname, "source", Icon1_Source);
            Icon1_Color = "#a9acf8";
            ucIcon1.SendProperty(context, "", true, Icon1_Internalname, "color", Icon1_Color);
            this.executeExternalObjectMethod("", true, "gx.core.ds", "setOption", new Object[] {(string)"color-scheme",(string)"dark"}, false);
            AV19websession.Set("DESIGN_COLOR_SCHEMA", "DESIGN_COLOR_SCHEMA_DARK");
         }
         else
         {
            Icon1_Source = context.convertURL( (string)(context.GetImagePath( "7c119e68-c5e6-4feb-9da2-9216d99724bb", "", context.GetTheme( ))));
            ucIcon1.SendProperty(context, "", true, Icon1_Internalname, "source", Icon1_Source);
            Icon1_Color = "#696ef2";
            ucIcon1.SendProperty(context, "", true, Icon1_Internalname, "color", Icon1_Color);
            this.executeExternalObjectMethod("", true, "gx.core.ds", "setOption", new Object[] {(string)"color-scheme",(string)"light"}, false);
            AV19websession.Set("DESIGN_COLOR_SCHEMA", "");
         }
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
         PA0W2( ) ;
         WS0W2( ) ;
         WE0W2( ) ;
         cleanup();
         context.SetWrapped(false);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      public override void master_styles( )
      {
         define_styles( ) ;
      }

      protected void define_styles( )
      {
         AddStyleSheetFile("Toastr/toastr.min.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         if ( ! ( WebComp_Comp_message_dispatcher == null ) )
         {
            if ( StringUtil.Len( WebComp_Comp_message_dispatcher_Component) != 0 )
            {
               WebComp_Comp_message_dispatcher.componentthemes();
            }
         }
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= (getDataAreaObject() == null ? Form : getDataAreaObject().GetForm()).Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)(getDataAreaObject() == null ? Form : getDataAreaObject().GetForm()).Jscriptsrc.Item(idxLst))), "?202531412514358", true, true);
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
         context.AddJavascriptSource("general/ui/masterunanimosidebar.js", "?202531412514358", false, true);
         context.AddJavascriptSource("Unanimo_chameleon/chameleon-loader.js", "", false, true);
         context.AddJavascriptSource("UserControls/GeneXusUnanimo.IconRender.js", "", false, true);
         context.AddJavascriptSource("Toastr/toastr.min.js", "", false, true);
         context.AddJavascriptSource("Toastr/ToastrRender.js", "", false, true);
         context.AddJavascriptSource("Unanimo_chameleon/chameleon-loader.js", "", false, true);
         context.AddJavascriptSource("UserControls/GeneXusUnanimo.SidebarRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         imgImage2_Internalname = "IMAGE2_MPAGE";
         imgImage1_Internalname = "IMAGE1_MPAGE";
         lblWalletname_Internalname = "WALLETNAME_MPAGE";
         Icon1_Internalname = "ICON1_MPAGE";
         Toastr1_Internalname = "TOASTR1_MPAGE";
         divHeader_Internalname = "HEADER_MPAGE";
         Sidebarmenu_Internalname = "SIDEBARMENU_MPAGE";
         divTablesidebarcontainer_Internalname = "TABLESIDEBARCONTAINER_MPAGE";
         divContent_Internalname = "CONTENT_MPAGE";
         divMaintable_Internalname = "MAINTABLE_MPAGE";
         (getDataAreaObject() == null ? Form : getDataAreaObject().GetForm()).Internalname = "FORM_MPAGE";
      }

      public override void initialize_properties( )
      {
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         divContent_Class = "expandible-container";
         Sidebarmenu_Title = "";
         lblWalletname_Caption = "Wallet Name";
         Sidebarmenu_Iscollapsed = Convert.ToBoolean( 0);
         Sidebarmenu_Selecteditemtarget = "";
         Sidebarmenu_Distancetotop = 0;
         Sidebarmenu_Footertext = "";
         Icon1_Visible = Convert.ToBoolean( -1);
         Icon1_Color = "#696ef2";
         Icon1_Size = "24px";
         Icon1_Source = "";
         Contentholder.setDataArea(getDataAreaObject());
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
         setEventMetadata("REFRESH_MPAGE","""{"handler":"Refresh","iparms":[]}""");
         setEventMetadata("GLOBALEVENTS_MPAGE.REFRESHMASTERPAGE_MPAGE","""{"handler":"E140W2","iparms":[]}""");
         setEventMetadata("GO HOME_MPAGE","""{"handler":"E110W1","iparms":[]}""");
         setEventMetadata("ICON1_MPAGE.CLICK_MPAGE","""{"handler":"E120W2","iparms":[{"av":"Icon1_Source","ctrl":"ICON1_MPAGE","prop":"source"}]""");
         setEventMetadata("ICON1_MPAGE.CLICK_MPAGE",""","oparms":[{"av":"Icon1_Source","ctrl":"ICON1_MPAGE","prop":"source"},{"av":"Icon1_Color","ctrl":"ICON1_MPAGE","prop":"color"}]}""");
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
         Contentholder = new GXDataAreaControl();
         GXKey = "";
         AV5sidebarItems = new GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem>( context, "SidebarItem", "GeneXusUnanimo");
         AV18text = "";
         AV17title = "";
         AV16toastrType = "";
         AV9notificationInfo = new GeneXus.Core.genexus.server.SdtNotificationInfo(context);
         AV6target = "";
         sPrefix = "";
         ClassString = "";
         imgImage2_gximage = "";
         StyleString = "";
         sImgUrl = "";
         TempTags = "";
         imgImage1_gximage = "";
         imgImage1_Jsonclick = "";
         lblWalletname_Jsonclick = "";
         ucIcon1 = new GXUserControl();
         ucToastr1 = new GXUserControl();
         ucSidebarmenu = new GXUserControl();
         WebComp_Comp_message_dispatcher_Component = "";
         OldComp_message_dispatcher = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV14wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV20designColorSchema = "";
         AV19websession = context.GetSession();
         GXt_objcol_SdtSidebarItems_SidebarItem2 = new GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem>( context, "SidebarItem", "GeneXusUnanimo");
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sDynURL = "";
         Form = new GXWebForm();
         WebComp_Comp_message_dispatcher = new GeneXus.Http.GXNullWebComponent();
         /* GeneXus formulas. */
      }

      private short GxWebError ;
      private short wbEnd ;
      private short wbStart ;
      private short nCmpId ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short nGotPars ;
      private short nGXWrapped ;
      private int Sidebarmenu_Distancetotop ;
      private int idxLst ;
      private string Sidebarmenu_Selecteditemtarget ;
      private string Icon1_Source ;
      private string GXKey ;
      private string Icon1_Size ;
      private string Icon1_Color ;
      private string Sidebarmenu_Footertext ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string divHeader_Internalname ;
      private string ClassString ;
      private string imgImage2_gximage ;
      private string StyleString ;
      private string sImgUrl ;
      private string imgImage2_Internalname ;
      private string TempTags ;
      private string imgImage1_gximage ;
      private string imgImage1_Internalname ;
      private string imgImage1_Jsonclick ;
      private string lblWalletname_Internalname ;
      private string lblWalletname_Caption ;
      private string lblWalletname_Jsonclick ;
      private string Icon1_Internalname ;
      private string Toastr1_Internalname ;
      private string divTablesidebarcontainer_Internalname ;
      private string Sidebarmenu_Title ;
      private string Sidebarmenu_Internalname ;
      private string divContent_Internalname ;
      private string divContent_Class ;
      private string WebComp_Comp_message_dispatcher_Component ;
      private string OldComp_message_dispatcher ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV20designColorSchema ;
      private string sDynURL ;
      private bool Sidebarmenu_Iscollapsed ;
      private bool Icon1_Visible ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool toggleJsOutput ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool bDynCreated_Comp_message_dispatcher ;
      private string AV18text ;
      private string AV17title ;
      private string AV16toastrType ;
      private string AV6target ;
      private IGxSession AV19websession ;
      private GXWebComponent WebComp_Comp_message_dispatcher ;
      private GXUserControl ucIcon1 ;
      private GXUserControl ucToastr1 ;
      private GXUserControl ucSidebarmenu ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXDataAreaControl Contentholder ;
      private GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem> AV5sidebarItems ;
      private GeneXus.Core.genexus.server.SdtNotificationInfo AV9notificationInfo ;
      private GeneXus.Programs.wallet.SdtWallet AV14wallet ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GXBaseCollection<GeneXus.Programs.genexusunanimo.SdtSidebarItems_SidebarItem> GXt_objcol_SdtSidebarItems_SidebarItem2 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
