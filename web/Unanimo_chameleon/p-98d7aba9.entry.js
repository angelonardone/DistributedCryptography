import{r as e,c as t,g as s,h as i,H as n}from"./p-7af91c05.js";import{S as r,a}from"./p-d031f0c4.js";const o=':where(button,a){all:unset;display:-ms-inline-flexbox;display:inline-flex;-ms-flex-align:center;align-items:center;-webkit-user-select:none;-moz-user-select:none;-ms-user-select:none;user-select:none;-ms-touch-action:manipulation;touch-action:manipulation;cursor:pointer}button:disabled{pointer-events:none}*,::after,::before{-webkit-box-sizing:border-box;box-sizing:border-box}:host{--ch-segmented-control-item__image-size:0.875em;--ch-segmented-control-item__background-image-size:100%;display:contents}.img--start::before,.img--end::after{content:"";display:block;inline-size:var(--ch-segmented-control-item__image-size);block-size:var(--ch-segmented-control-item__image-size);min-inline-size:var(--ch-segmented-control-item__image-size)}.img--start{--ch-img:var(--ch-start-img)}.img--end{--ch-img:var(--ch-end-img)}.start-img-type--background::before,.end-img-type--background::after{background:no-repeat center/var(--ch-segmented-control-item__background-image-size) var(--ch-img)}.start-img-type--mask::before,.end-img-type--mask::after{-webkit-mask:var(--ch-img) 50% 50%/var(--ch-segmented-control-item__background-image-size) var(--ch-segmented-control-item__background-image-size) no-repeat;background-color:currentColor}';const c=o;const d=class{constructor(s){e(this,s);this.selectedChange=t(this,"selectedChange",7);this.accessibleName=undefined;this.between=false;this.caption=undefined;this.disabled=false;this.endImgSrc=undefined;this.endImgType="background";this.first=false;this.last=false;this.selected=undefined;this.startImgSrc=undefined;this.startImgType="background"}get el(){return s(this)}selectedChange;#e=()=>`action${this.disabled?` ${r.DISABLED}`:""} ${this.selected?r.SELECTED:r.UNSELECTED}${this.first?` ${r.FIRST}`:""}${this.last?` ${r.LAST}`:""}${this.between?` ${r.BETWEEN}`:""}`;#t=e=>{e.stopPropagation();this.selectedChange.emit(this.el.id)};render(){const e=!!this.startImgSrc;const t=!!this.endImgSrc;const s=e||t;return i(n,{key:"983216a686bb6445352755eba174eb755f21ae40",role:"listitem"},i("button",{key:"9d19a6c9aca7d74e15144dfe0460354f8ed542d1","aria-label":this.accessibleName||null,"aria-selected":this.selected?"true":null,class:s?{[`start-img-type--${this.startImgType??"background"} img--start`]:e,[`end-img-type--${this.endImgType??"background"} img--end`]:t}:undefined,part:this.#e(),style:s?{"--ch-start-img":e?`url("${this.startImgSrc}")`:undefined,"--ch-end-img":t?`url("${this.endImgSrc}")`:undefined}:undefined,type:"button",onClick:this.#t},this.caption))}};d.style=c;const m="ch-segmented-control-render{display:inline-grid;grid-auto-flow:column;grid-auto-columns:-webkit-max-content;grid-auto-columns:max-content}";const l=m;const h=class{constructor(s){e(this,s);this.selectedItemChange=t(this,"selectedItemChange",7);this.exportParts=a;this.itemCssClass="segmented-control-item";this.model=undefined;this.selectedId=undefined}selectedItemChange;handleSelectedChange(e){e.stopPropagation();this.selectedId=e.detail;this.selectedItemChange.emit(e.detail)}#s=(e,t)=>{const s=t===0;const n=t===this.model.length-1;const r=!s&&!n;return i("ch-segmented-control-item",{id:e.id,accessibleName:e.accessibleName,between:r,caption:e.caption,class:e.class||this.itemCssClass,disabled:e.disabled,exportparts:this.exportParts,endImgSrc:e.endImgSrc,endImgType:e.endImgType,first:s,last:n,selected:this.selectedId===e.id,startImgSrc:e.startImgSrc,startImgType:e.startImgType})};render(){return i(n,{key:"cfc7259f3807840447f8771a967949a42702ea38",role:"list"},this.model?.map(this.#s))}};h.style=l;export{d as ch_segmented_control_item,h as ch_segmented_control_render};
//# sourceMappingURL=p-98d7aba9.entry.js.map