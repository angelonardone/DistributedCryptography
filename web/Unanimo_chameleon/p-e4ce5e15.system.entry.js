var __awaiter=this&&this.__awaiter||function(t,n,i,e){function r(t){return t instanceof i?t:new i((function(n){n(t)}))}return new(i||(i=Promise))((function(i,o){function s(t){try{u(e.next(t))}catch(t){o(t)}}function c(t){try{u(e["throw"](t))}catch(t){o(t)}}function u(t){t.done?i(t.value):r(t.value).then(s,c)}u((e=e.apply(t,n||[])).next())}))};var __generator=this&&this.__generator||function(t,n){var i={label:0,sent:function(){if(o[0]&1)throw o[1];return o[1]},trys:[],ops:[]},e,r,o,s;return s={next:c(0),throw:c(1),return:c(2)},typeof Symbol==="function"&&(s[Symbol.iterator]=function(){return this}),s;function c(t){return function(n){return u([t,n])}}function u(c){if(e)throw new TypeError("Generator is already executing.");while(s&&(s=0,c[0]&&(i=0)),i)try{if(e=1,r&&(o=c[0]&2?r["return"]:c[0]?r["throw"]||((o=r["return"])&&o.call(r),0):r.next)&&!(o=o.call(r,c[1])).done)return o;if(r=0,o)c=[c[0]&2,o.value];switch(c[0]){case 0:case 1:o=c;break;case 4:i.label++;return{value:c[1],done:false};case 5:i.label++;r=c[1];c=[0];continue;case 7:c=i.ops.pop();i.trys.pop();continue;default:if(!(o=i.trys,o=o.length>0&&o[o.length-1])&&(c[0]===6||c[0]===2)){i=0;continue}if(c[0]===3&&(!o||c[1]>o[0]&&c[1]<o[3])){i.label=c[1];break}if(c[0]===6&&i.label<o[1]){i.label=o[1];o=c;break}if(o&&i.label<o[2]){i.label=o[2];i.ops.push(c);break}if(o[2])i.ops.pop();i.trys.pop();continue}c=n.call(t,i)}catch(t){c=[6,t];r=0}finally{e=o=0}if(c[0]&5)throw c[1];return{value:c[0]?c[1]:void 0,done:true}}};System.register(["./p-62fc4e49.system.js"],(function(t){"use strict";var n,i,e,r;return{setters:[function(t){n=t.r;i=t.c;e=t.g;r=t.h}],execute:function(){var o=":host{display:-ms-flexbox;display:flex;grid-column-start:1;grid-column-end:-1}.loading:not([hidden]){display:contents}";var s=o;var c=t("ch_grid_infinite_scroll",function(){function t(t){var e=this;this.intersectionObserverHandler=function(t,n){var i=e.gridLayoutElement.scrollHeight!==e.gridLayoutElement.clientHeight;var r=n.root?i:!i;if(r&&t[0].isIntersecting&&e.status==="loaded"){e.status="loading";e.infinite.emit()}};n(this,t);this.infinite=i(this,"infinite",7);this.hasGridScroll=undefined;this.status="loaded"}Object.defineProperty(t.prototype,"el",{get:function(){return e(this)},enumerable:false,configurable:true});t.prototype.componentWillLoad=function(){this.gridLayoutElement=this.el.assignedSlot.closest("section.main");this.intersectionGridLayoutObserver=new IntersectionObserver(this.intersectionObserverHandler,{root:this.gridLayoutElement,rootMargin:"1px"});this.intersectionGridLayoutObserver.observe(this.el);this.intersectionDocumentObserver=new IntersectionObserver(this.intersectionObserverHandler,{rootMargin:"1px"});this.intersectionDocumentObserver.observe(this.el)};t.prototype.disconnectedCallback=function(){if(this.intersectionGridLayoutObserver){this.intersectionGridLayoutObserver.disconnect()}};t.prototype.complete=function(){return __awaiter(this,void 0,void 0,(function(){return __generator(this,(function(t){this.status="loaded";return[2]}))}))};t.prototype.render=function(){return r("div",{key:"2f0240c9a3cd4432fb883bce52927b72e2eea5b3",class:"loading",hidden:!(this.status==="loading")},r("slot",{key:"5574c773bb63ed7bd8ded5aaad649eb29f2f1d80"}))};return t}());c.style=s}}}));
//# sourceMappingURL=p-e4ce5e15.system.entry.js.map