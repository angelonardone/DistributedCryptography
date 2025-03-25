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
   public class wallets : GXDataArea
   {
      public wallets( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public wallets( IGxContext context )
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
         cmbavCtlnetworktype = new GXCombobox();
         cmbavCtlwallettype = new GXCombobox();
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
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Grid1") == 0 )
            {
               gxnrGrid1_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Grid1") == 0 )
            {
               gxgrGrid1_refresh_invoke( ) ;
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

      protected void gxnrGrid1_newrow_invoke( )
      {
         nRC_GXsfl_14 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_14"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_14_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_14_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_14_idx = GetPar( "sGXsfl_14_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGrid1_newrow( ) ;
         /* End function gxnrGrid1_newrow_invoke */
      }

      protected void gxgrGrid1_refresh_invoke( )
      {
         ajax_req_read_hidden_sdt(GetNextPar( ), AV6wallets);
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGrid1_refresh( AV6wallets) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGrid1_refresh_invoke */
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
         PA0I2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START0I2( ) ;
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1218140), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1218140), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1218140), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.wallets.aspx") +"\">") ;
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLETS", AV6wallets);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLETS", AV6wallets);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLETS", GetSecureSignedToken( "", AV6wallets, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Wallets", AV6wallets);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Wallets", AV6wallets);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_Wallets", GetSecureSignedToken( "", AV6wallets, context));
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_14", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_14), 8, 0, ".", "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLETS", AV6wallets);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLETS", AV6wallets);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLETS", GetSecureSignedToken( "", AV6wallets, context));
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
            WE0I2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT0I2( ) ;
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
         return formatLink("wallet.wallets.aspx")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.Wallets" ;
      }

      public override string GetPgmdesc( )
      {
         return "Wallets" ;
      }

      protected void WB0I0( )
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 6,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCreateanewwallet_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(14), 2, 0)+","+"null"+");", "Create a new Wallet", bttCreateanewwallet_Jsonclick, 7, "Create a new Wallet", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"e110i1_client"+"'", TempTags, "", 2, "HLP_Wallet/Wallets.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttRestorewallet_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(14), 2, 0)+","+"null"+");", "Restore Wallet", bttRestorewallet_Jsonclick, 7, "Restore Wallet", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"e120i1_client"+"'", TempTags, "", 2, "HLP_Wallet/Wallets.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbdirectory_Internalname, lblTbdirectory_Caption, "", "", lblTbdirectory_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_Wallet/Wallets.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            Grid1Container.SetWrapped(nGXWrapped);
            StartGridControl14( ) ;
         }
         if ( wbEnd == 14 )
         {
            wbEnd = 0;
            nRC_GXsfl_14 = (int)(nGXsfl_14_idx-1);
            if ( Grid1Container.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV9GXV1 = nGXsfl_14_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"Grid1Container"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Grid1", Grid1Container, subGrid1_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "Grid1ContainerData", Grid1Container.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "Grid1ContainerData"+"V", Grid1Container.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"Grid1ContainerData"+"V"+"\" value='"+Grid1Container.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 20,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttConfiguration_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(14), 2, 0)+","+"null"+");", "Configuration", bttConfiguration_Jsonclick, 7, "Configuration", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"e130i1_client"+"'", TempTags, "", 2, "HLP_Wallet/Wallets.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 14 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( Grid1Container.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV9GXV1 = nGXsfl_14_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"Grid1Container"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Grid1", Grid1Container, subGrid1_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "Grid1ContainerData", Grid1Container.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "Grid1ContainerData"+"V", Grid1Container.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"Grid1ContainerData"+"V"+"\" value='"+Grid1Container.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START0I2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_10-184260", 0) ;
            }
         }
         Form.Meta.addItem("description", "Wallets", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP0I0( ) ;
      }

      protected void WS0I2( )
      {
         START0I2( ) ;
         EVT0I2( ) ;
      }

      protected void EVT0I2( )
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
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 22), "'OPEN SELECTED WALLET'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 10), "GRID1.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 22), "'OPEN SELECTED WALLET'") == 0 ) )
                           {
                              nGXsfl_14_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
                              SubsflControlProps_142( ) ;
                              AV9GXV1 = nGXsfl_14_idx;
                              if ( ( AV6wallets.Count >= AV9GXV1 ) && ( AV9GXV1 > 0 ) )
                              {
                                 AV6wallets.CurrentItem = ((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1));
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
                                    E140I2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'OPEN SELECTED WALLET'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Open Selected Wallet' */
                                    E150I2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRID1.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Grid1.Load */
                                    E160I2 ();
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

      protected void WE0I2( )
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

      protected void PA0I2( )
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

      protected void gxnrGrid1_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_142( ) ;
         while ( nGXsfl_14_idx <= nRC_GXsfl_14 )
         {
            sendrow_142( ) ;
            nGXsfl_14_idx = ((subGrid1_Islastpage==1)&&(nGXsfl_14_idx+1>subGrid1_fnc_Recordsperpage( )) ? 1 : nGXsfl_14_idx+1);
            sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
            SubsflControlProps_142( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( Grid1Container)) ;
         /* End function gxnrGrid1_newrow */
      }

      protected void gxgrGrid1_refresh( GXBaseCollection<GeneXus.Programs.wallet.SdtWallet> AV6wallets )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRID1_nCurrentRecord = 0;
         RF0I2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGrid1_refresh */
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
         RF0I2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtlwalletname_Enabled = 0;
         cmbavCtlnetworktype.Enabled = 0;
         cmbavCtlwallettype.Enabled = 0;
      }

      protected void RF0I2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            Grid1Container.ClearRows();
         }
         wbStart = 14;
         nGXsfl_14_idx = 1;
         sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
         SubsflControlProps_142( ) ;
         bGXsfl_14_Refreshing = true;
         Grid1Container.AddObjectProperty("GridName", "Grid1");
         Grid1Container.AddObjectProperty("CmpContext", "");
         Grid1Container.AddObjectProperty("InMasterPage", "false");
         Grid1Container.AddObjectProperty("Class", "Grid");
         Grid1Container.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         Grid1Container.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         Grid1Container.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Backcolorstyle), 1, 0, ".", "")));
         Grid1Container.PageSize = subGrid1_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_142( ) ;
            /* Execute user event: Grid1.Load */
            E160I2 ();
            wbEnd = 14;
            WB0I0( ) ;
         }
         bGXsfl_14_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes0I2( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLETS", AV6wallets);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLETS", AV6wallets);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLETS", GetSecureSignedToken( "", AV6wallets, context));
      }

      protected int subGrid1_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGrid1_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subGrid1_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subGrid1_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtlwalletname_Enabled = 0;
         cmbavCtlnetworktype.Enabled = 0;
         cmbavCtlwallettype.Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP0I0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E140I2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "Wallets"), AV6wallets);
            ajax_req_read_hidden_sdt(cgiGet( "vWALLETS"), AV6wallets);
            /* Read saved values. */
            nRC_GXsfl_14 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_14"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_14 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_14"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_14_fel_idx = 0;
            while ( nGXsfl_14_fel_idx < nRC_GXsfl_14 )
            {
               nGXsfl_14_fel_idx = ((subGrid1_Islastpage==1)&&(nGXsfl_14_fel_idx+1>subGrid1_fnc_Recordsperpage( )) ? 1 : nGXsfl_14_fel_idx+1);
               sGXsfl_14_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_142( ) ;
               AV9GXV1 = nGXsfl_14_fel_idx;
               if ( ( AV6wallets.Count >= AV9GXV1 ) && ( AV9GXV1 > 0 ) )
               {
                  AV6wallets.CurrentItem = ((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1));
               }
            }
            if ( nGXsfl_14_fel_idx == 0 )
            {
               nGXsfl_14_idx = 1;
               sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
               SubsflControlProps_142( ) ;
            }
            nGXsfl_14_fel_idx = 1;
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
         E140I2 ();
         if (returnInSub) return;
      }

      protected void E140I2( )
      {
         /* Start Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.initempstorages(context ).execute( ) ;
         AV7webSession.Destroy();
         GXt_objcol_SdtWallet1 = AV6wallets;
         new GeneXus.Programs.wallet.readallwallets(context ).execute( out  GXt_objcol_SdtWallet1) ;
         AV6wallets = GXt_objcol_SdtWallet1;
         gx_BV14 = true;
         AV8directory.Source = "Wallets";
         lblTbdirectory_Caption = "<h4>Please, backup all the files under the following directory : <b>"+AV8directory.GetAbsoluteName()+"</b></h4>";
         AssignProp("", false, lblTbdirectory_Internalname, "Caption", lblTbdirectory_Caption, true);
      }

      protected void E150I2( )
      {
         AV9GXV1 = nGXsfl_14_idx;
         if ( ( AV9GXV1 > 0 ) && ( AV6wallets.Count >= AV9GXV1 ) )
         {
            AV6wallets.CurrentItem = ((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1));
         }
         /* 'Open Selected Wallet' Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.setwallet(context ).execute(  ((GeneXus.Programs.wallet.SdtWallet)(AV6wallets.CurrentItem))) ;
         CallWebObject(formatLink("wallet.enterpassword.aspx", new object[] {UrlEncode(StringUtil.RTrim(((GeneXus.Programs.wallet.SdtWallet)(AV6wallets.CurrentItem)).gxTpr_Walletname))}, new string[] {"walletName"}) );
         context.wjLocDisableFrm = 1;
      }

      private void E160I2( )
      {
         /* Grid1_Load Routine */
         returnInSub = false;
         AV9GXV1 = 1;
         while ( AV9GXV1 <= AV6wallets.Count )
         {
            AV6wallets.CurrentItem = ((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1));
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 14;
            }
            sendrow_142( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_14_Refreshing )
            {
               DoAjaxLoad(14, Grid1Row);
            }
            AV9GXV1 = (int)(AV9GXV1+1);
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
         PA0I2( ) ;
         WS0I2( ) ;
         WE0I2( ) ;
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
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202532417465898", true, true);
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
         context.AddJavascriptSource("wallet/wallets.js", "?202532417465898", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_142( )
      {
         edtavCtlwalletname_Internalname = "CTLWALLETNAME_"+sGXsfl_14_idx;
         cmbavCtlnetworktype_Internalname = "CTLNETWORKTYPE_"+sGXsfl_14_idx;
         cmbavCtlwallettype_Internalname = "CTLWALLETTYPE_"+sGXsfl_14_idx;
      }

      protected void SubsflControlProps_fel_142( )
      {
         edtavCtlwalletname_Internalname = "CTLWALLETNAME_"+sGXsfl_14_fel_idx;
         cmbavCtlnetworktype_Internalname = "CTLNETWORKTYPE_"+sGXsfl_14_fel_idx;
         cmbavCtlwallettype_Internalname = "CTLWALLETTYPE_"+sGXsfl_14_fel_idx;
      }

      protected void sendrow_142( )
      {
         sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
         SubsflControlProps_142( ) ;
         WB0I0( ) ;
         Grid1Row = GXWebRow.GetNew(context,Grid1Container);
         if ( subGrid1_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subGrid1_Backstyle = 0;
            if ( StringUtil.StrCmp(subGrid1_Class, "") != 0 )
            {
               subGrid1_Linesclass = subGrid1_Class+"Odd";
            }
         }
         else if ( subGrid1_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subGrid1_Backstyle = 0;
            subGrid1_Backcolor = subGrid1_Allbackcolor;
            if ( StringUtil.StrCmp(subGrid1_Class, "") != 0 )
            {
               subGrid1_Linesclass = subGrid1_Class+"Uniform";
            }
         }
         else if ( subGrid1_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subGrid1_Backstyle = 1;
            if ( StringUtil.StrCmp(subGrid1_Class, "") != 0 )
            {
               subGrid1_Linesclass = subGrid1_Class+"Odd";
            }
            subGrid1_Backcolor = (int)(0x0);
         }
         else if ( subGrid1_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subGrid1_Backstyle = 1;
            if ( ((int)((nGXsfl_14_idx) % (2))) == 0 )
            {
               subGrid1_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGrid1_Class, "") != 0 )
               {
                  subGrid1_Linesclass = subGrid1_Class+"Even";
               }
            }
            else
            {
               subGrid1_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGrid1_Class, "") != 0 )
               {
                  subGrid1_Linesclass = subGrid1_Class+"Odd";
               }
            }
         }
         if ( Grid1Container.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr ") ;
            context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
            context.WriteHtmlText( " gxrow=\""+sGXsfl_14_idx+"\">") ;
         }
         /* Subfile cell */
         if ( Grid1Container.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 15,'',false,'" + sGXsfl_14_idx + "',14)\"";
         ROClassString = "Attribute";
         edtavCtlwalletname_Link = "";
         AssignProp("", false, edtavCtlwalletname_Internalname, "Link", edtavCtlwalletname_Link, !bGXsfl_14_Refreshing);
         Grid1Row.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlwalletname_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1)).gxTpr_Walletname),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,15);\"","'"+""+"'"+",false,"+"'"+"E\\'OPEN SELECTED WALLET\\'."+sGXsfl_14_idx+"'",(string)edtavCtlwalletname_Link,(string)"",(string)"",(string)"",(string)edtavCtlwalletname_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlwalletname_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)200,(short)0,(short)0,(short)14,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( Grid1Container.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 16,'',false,'" + sGXsfl_14_idx + "',14)\"";
         if ( ( cmbavCtlnetworktype.ItemCount == 0 ) && isAjaxCallMode( ) )
         {
            GXCCtl = "CTLNETWORKTYPE_" + sGXsfl_14_idx;
            cmbavCtlnetworktype.Name = GXCCtl;
            cmbavCtlnetworktype.WebTags = "";
            cmbavCtlnetworktype.addItem("MainNet", "MainNet", 0);
            cmbavCtlnetworktype.addItem("TestNet", "TestNet (for testing only)", 0);
            cmbavCtlnetworktype.addItem("RegTest", "RegTest (for testing only)", 0);
            if ( cmbavCtlnetworktype.ItemCount > 0 )
            {
               if ( ( AV9GXV1 > 0 ) && ( AV6wallets.Count >= AV9GXV1 ) && String.IsNullOrEmpty(StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1)).gxTpr_Networktype)) )
               {
                  ((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1)).gxTpr_Networktype = cmbavCtlnetworktype.getValidValue(((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1)).gxTpr_Networktype);
               }
            }
         }
         /* ComboBox */
         Grid1Row.AddColumnProperties("combobox", 2, isAjaxCallMode( ), new Object[] {(GXCombobox)cmbavCtlnetworktype,(string)cmbavCtlnetworktype_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1)).gxTpr_Networktype),(short)1,(string)cmbavCtlnetworktype_Jsonclick,(short)0,(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"char",(string)"",(short)-1,cmbavCtlnetworktype.Enabled,(short)0,(short)0,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)"Attribute",(string)"",(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,16);\"",(string)"",(bool)true,(short)0});
         cmbavCtlnetworktype.CurrentValue = StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1)).gxTpr_Networktype);
         AssignProp("", false, cmbavCtlnetworktype_Internalname, "Values", (string)(cmbavCtlnetworktype.ToJavascriptSource()), !bGXsfl_14_Refreshing);
         /* Subfile cell */
         if ( Grid1Container.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'',false,'" + sGXsfl_14_idx + "',14)\"";
         if ( ( cmbavCtlwallettype.ItemCount == 0 ) && isAjaxCallMode( ) )
         {
            GXCCtl = "CTLWALLETTYPE_" + sGXsfl_14_idx;
            cmbavCtlwallettype.Name = GXCCtl;
            cmbavCtlwallettype.WebTags = "";
            cmbavCtlwallettype.addItem("SelectWalletType", "Select Wallet Type", 0);
            cmbavCtlwallettype.addItem("ImportedWIF", "Imported WIF", 0);
            cmbavCtlwallettype.addItem("BrainWallet", "Brain Wallet", 0);
            cmbavCtlwallettype.addItem("BIP44", "BIP44 (Legacy)", 0);
            cmbavCtlwallettype.addItem("BIP49", "BIP49 (SegwitP2SH)", 0);
            cmbavCtlwallettype.addItem("BIP84", "BIP84 (native Segwit)", 0);
            cmbavCtlwallettype.addItem("BIP86", "BIP86 (Taproot P2TR)", 0);
            if ( cmbavCtlwallettype.ItemCount > 0 )
            {
               if ( ( AV9GXV1 > 0 ) && ( AV6wallets.Count >= AV9GXV1 ) && String.IsNullOrEmpty(StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1)).gxTpr_Wallettype)) )
               {
                  ((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1)).gxTpr_Wallettype = cmbavCtlwallettype.getValidValue(((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1)).gxTpr_Wallettype);
               }
            }
         }
         /* ComboBox */
         Grid1Row.AddColumnProperties("combobox", 2, isAjaxCallMode( ), new Object[] {(GXCombobox)cmbavCtlwallettype,(string)cmbavCtlwallettype_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1)).gxTpr_Wallettype),(short)1,(string)cmbavCtlwallettype_Jsonclick,(short)0,(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"char",(string)"",(short)-1,cmbavCtlwallettype.Enabled,(short)0,(short)0,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)"Attribute",(string)"",(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,17);\"",(string)"",(bool)true,(short)0});
         cmbavCtlwallettype.CurrentValue = StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1)).gxTpr_Wallettype);
         AssignProp("", false, cmbavCtlwallettype_Internalname, "Values", (string)(cmbavCtlwallettype.ToJavascriptSource()), !bGXsfl_14_Refreshing);
         send_integrity_lvl_hashes0I2( ) ;
         Grid1Container.AddRow(Grid1Row);
         nGXsfl_14_idx = ((subGrid1_Islastpage==1)&&(nGXsfl_14_idx+1>subGrid1_fnc_Recordsperpage( )) ? 1 : nGXsfl_14_idx+1);
         sGXsfl_14_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_14_idx), 4, 0), 4, "0");
         SubsflControlProps_142( ) ;
         /* End function sendrow_142 */
      }

      protected void init_web_controls( )
      {
         GXCCtl = "CTLNETWORKTYPE_" + sGXsfl_14_idx;
         cmbavCtlnetworktype.Name = GXCCtl;
         cmbavCtlnetworktype.WebTags = "";
         cmbavCtlnetworktype.addItem("MainNet", "MainNet", 0);
         cmbavCtlnetworktype.addItem("TestNet", "TestNet (for testing only)", 0);
         cmbavCtlnetworktype.addItem("RegTest", "RegTest (for testing only)", 0);
         if ( cmbavCtlnetworktype.ItemCount > 0 )
         {
            if ( ( AV9GXV1 > 0 ) && ( AV6wallets.Count >= AV9GXV1 ) && String.IsNullOrEmpty(StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1)).gxTpr_Networktype)) )
            {
               ((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1)).gxTpr_Networktype = cmbavCtlnetworktype.getValidValue(((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1)).gxTpr_Networktype);
            }
         }
         GXCCtl = "CTLWALLETTYPE_" + sGXsfl_14_idx;
         cmbavCtlwallettype.Name = GXCCtl;
         cmbavCtlwallettype.WebTags = "";
         cmbavCtlwallettype.addItem("SelectWalletType", "Select Wallet Type", 0);
         cmbavCtlwallettype.addItem("ImportedWIF", "Imported WIF", 0);
         cmbavCtlwallettype.addItem("BrainWallet", "Brain Wallet", 0);
         cmbavCtlwallettype.addItem("BIP44", "BIP44 (Legacy)", 0);
         cmbavCtlwallettype.addItem("BIP49", "BIP49 (SegwitP2SH)", 0);
         cmbavCtlwallettype.addItem("BIP84", "BIP84 (native Segwit)", 0);
         cmbavCtlwallettype.addItem("BIP86", "BIP86 (Taproot P2TR)", 0);
         if ( cmbavCtlwallettype.ItemCount > 0 )
         {
            if ( ( AV9GXV1 > 0 ) && ( AV6wallets.Count >= AV9GXV1 ) && String.IsNullOrEmpty(StringUtil.RTrim( ((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1)).gxTpr_Wallettype)) )
            {
               ((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1)).gxTpr_Wallettype = cmbavCtlwallettype.getValidValue(((GeneXus.Programs.wallet.SdtWallet)AV6wallets.Item(AV9GXV1)).gxTpr_Wallettype);
            }
         }
         /* End function init_web_controls */
      }

      protected void StartGridControl14( )
      {
         if ( Grid1Container.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"Grid1Container"+"DivS\" data-gxgridid=\"14\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGrid1_Internalname, subGrid1_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGrid1_Backcolorstyle == 0 )
            {
               subGrid1_Titlebackstyle = 0;
               if ( StringUtil.Len( subGrid1_Class) > 0 )
               {
                  subGrid1_Linesclass = subGrid1_Class+"Title";
               }
            }
            else
            {
               subGrid1_Titlebackstyle = 1;
               if ( subGrid1_Backcolorstyle == 1 )
               {
                  subGrid1_Titlebackcolor = subGrid1_Allbackcolor;
                  if ( StringUtil.Len( subGrid1_Class) > 0 )
                  {
                     subGrid1_Linesclass = subGrid1_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGrid1_Class) > 0 )
                  {
                     subGrid1_Linesclass = subGrid1_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Wallet Name") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Network Type") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Wallet Type") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            Grid1Container.AddObjectProperty("GridName", "Grid1");
         }
         else
         {
            Grid1Container.AddObjectProperty("GridName", "Grid1");
            Grid1Container.AddObjectProperty("Header", subGrid1_Header);
            Grid1Container.AddObjectProperty("Class", "Grid");
            Grid1Container.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            Grid1Container.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            Grid1Container.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Backcolorstyle), 1, 0, ".", "")));
            Grid1Container.AddObjectProperty("CmpContext", "");
            Grid1Container.AddObjectProperty("InMasterPage", "false");
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Column.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlwalletname_Enabled), 5, 0, ".", "")));
            Grid1Column.AddObjectProperty("Link", StringUtil.RTrim( edtavCtlwalletname_Link));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Column.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(cmbavCtlnetworktype.Enabled), 5, 0, ".", "")));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Column = GXWebColumn.GetNew(isAjaxCallMode( ));
            Grid1Column.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(cmbavCtlwallettype.Enabled), 5, 0, ".", "")));
            Grid1Container.AddColumnProperties(Grid1Column);
            Grid1Container.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Selectedindex), 4, 0, ".", "")));
            Grid1Container.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Allowselection), 1, 0, ".", "")));
            Grid1Container.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Selectioncolor), 9, 0, ".", "")));
            Grid1Container.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Allowhovering), 1, 0, ".", "")));
            Grid1Container.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Hoveringcolor), 9, 0, ".", "")));
            Grid1Container.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Allowcollapsing), 1, 0, ".", "")));
            Grid1Container.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid1_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         bttCreateanewwallet_Internalname = "CREATEANEWWALLET";
         bttRestorewallet_Internalname = "RESTOREWALLET";
         lblTbdirectory_Internalname = "TBDIRECTORY";
         edtavCtlwalletname_Internalname = "CTLWALLETNAME";
         cmbavCtlnetworktype_Internalname = "CTLNETWORKTYPE";
         cmbavCtlwallettype_Internalname = "CTLWALLETTYPE";
         bttConfiguration_Internalname = "CONFIGURATION";
         divMaintable_Internalname = "MAINTABLE";
         Form.Internalname = "FORM";
         subGrid1_Internalname = "GRID1";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subGrid1_Allowcollapsing = 0;
         subGrid1_Allowselection = 0;
         subGrid1_Header = "";
         cmbavCtlwallettype_Jsonclick = "";
         cmbavCtlwallettype.Enabled = 0;
         cmbavCtlnetworktype_Jsonclick = "";
         cmbavCtlnetworktype.Enabled = 0;
         edtavCtlwalletname_Jsonclick = "";
         edtavCtlwalletname_Link = "";
         edtavCtlwalletname_Enabled = 0;
         subGrid1_Class = "Grid";
         subGrid1_Backcolorstyle = 0;
         cmbavCtlwallettype.Enabled = -1;
         cmbavCtlnetworktype.Enabled = -1;
         edtavCtlwalletname_Enabled = -1;
         lblTbdirectory_Caption = "TbDirectory";
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Wallets";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRID1_nFirstRecordOnPage"},{"av":"GRID1_nEOF"},{"av":"AV6wallets","fld":"vWALLETS","grid":14,"hsh":true},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"nRC_GXsfl_14","ctrl":"GRID1","prop":"GridRC","grid":14}]}""");
         setEventMetadata("'OPEN SELECTED WALLET'","""{"handler":"E150I2","iparms":[{"av":"AV6wallets","fld":"vWALLETS","grid":14,"hsh":true},{"av":"nGXsfl_14_idx","ctrl":"GRID","prop":"GridCurrRow","grid":14},{"av":"GRID1_nFirstRecordOnPage"},{"av":"nRC_GXsfl_14","ctrl":"GRID1","prop":"GridRC","grid":14}]}""");
         setEventMetadata("'CREATE A NEW WALLET'","""{"handler":"E110I1","iparms":[]}""");
         setEventMetadata("'RESTORE WALLET'","""{"handler":"E120I1","iparms":[]}""");
         setEventMetadata("'CONFIGURATION'","""{"handler":"E130I1","iparms":[]}""");
         setEventMetadata("VALIDV_GXV3","""{"handler":"Validv_Gxv3","iparms":[]}""");
         setEventMetadata("VALIDV_GXV4","""{"handler":"Validv_Gxv4","iparms":[]}""");
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
         AV6wallets = new GXBaseCollection<GeneXus.Programs.wallet.SdtWallet>( context, "Wallet", "distributedcryptography");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttCreateanewwallet_Jsonclick = "";
         bttRestorewallet_Jsonclick = "";
         lblTbdirectory_Jsonclick = "";
         Grid1Container = new GXWebGrid( context);
         sStyleString = "";
         bttConfiguration_Jsonclick = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV7webSession = context.GetSession();
         GXt_objcol_SdtWallet1 = new GXBaseCollection<GeneXus.Programs.wallet.SdtWallet>( context, "Wallet", "distributedcryptography");
         AV8directory = new GxDirectory(context.GetPhysicalPath());
         Grid1Row = new GXWebRow();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGrid1_Linesclass = "";
         ROClassString = "";
         GXCCtl = "";
         Grid1Column = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtlwalletname_Enabled = 0;
         cmbavCtlnetworktype.Enabled = 0;
         cmbavCtlwallettype.Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGrid1_Backcolorstyle ;
      private short nGXWrapped ;
      private short subGrid1_Backstyle ;
      private short subGrid1_Titlebackstyle ;
      private short subGrid1_Allowselection ;
      private short subGrid1_Allowhovering ;
      private short subGrid1_Allowcollapsing ;
      private short subGrid1_Collapsed ;
      private short GRID1_nEOF ;
      private int nRC_GXsfl_14 ;
      private int nGXsfl_14_idx=1 ;
      private int AV9GXV1 ;
      private int subGrid1_Islastpage ;
      private int edtavCtlwalletname_Enabled ;
      private int nGXsfl_14_fel_idx=1 ;
      private int idxLst ;
      private int subGrid1_Backcolor ;
      private int subGrid1_Allbackcolor ;
      private int subGrid1_Titlebackcolor ;
      private int subGrid1_Selectedindex ;
      private int subGrid1_Selectioncolor ;
      private int subGrid1_Hoveringcolor ;
      private long GRID1_nCurrentRecord ;
      private long GRID1_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_14_idx="0001" ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttCreateanewwallet_Internalname ;
      private string bttCreateanewwallet_Jsonclick ;
      private string bttRestorewallet_Internalname ;
      private string bttRestorewallet_Jsonclick ;
      private string lblTbdirectory_Internalname ;
      private string lblTbdirectory_Caption ;
      private string lblTbdirectory_Jsonclick ;
      private string sStyleString ;
      private string subGrid1_Internalname ;
      private string bttConfiguration_Internalname ;
      private string bttConfiguration_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string sGXsfl_14_fel_idx="0001" ;
      private string edtavCtlwalletname_Internalname ;
      private string cmbavCtlnetworktype_Internalname ;
      private string cmbavCtlwallettype_Internalname ;
      private string subGrid1_Class ;
      private string subGrid1_Linesclass ;
      private string ROClassString ;
      private string edtavCtlwalletname_Link ;
      private string edtavCtlwalletname_Jsonclick ;
      private string GXCCtl ;
      private string cmbavCtlnetworktype_Jsonclick ;
      private string cmbavCtlwallettype_Jsonclick ;
      private string subGrid1_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_14_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV14 ;
      private GXWebGrid Grid1Container ;
      private GXWebRow Grid1Row ;
      private GXWebColumn Grid1Column ;
      private IGxSession AV7webSession ;
      private GxDirectory AV8directory ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavCtlnetworktype ;
      private GXCombobox cmbavCtlwallettype ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWallet> AV6wallets ;
      private GXBaseCollection<GeneXus.Programs.wallet.SdtWallet> GXt_objcol_SdtWallet1 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
