<template>
<h1 
:class='["hello", applyblue(),{"font":addFontStyleClass }]' 
:style="[{'font-size':fontSize}]"
>Header1</h1>

<div>Add 'font' class:</div>
<button @click="onButtonClick()"> add class </button> <br/><br/>
<div>Change font:</div>
<button @click="changeFontStyle(30)">+30</button>
<button @click="changeFontStyle(70)">+70</button>
<button @click="changeFontStyle(80)">+80</button> 
</template>

<script>
export default {
  data() {
   return { 
     addFontStyleClass: false,
     fontSize:'50'
   }
  },
  name: "HelloWorld",
  props: {
    msg: String,
  },
  methods:{
    onButtonClick() {
      this.addFontStyleClass=true
    },
    
    changeFontStyle(fot){
       this.fontSize=fot+'px';
    },

    applyblue: function (){
      return "blue"
    },
    
  },
  computed:{
  
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.hello{
 border :3px solid red;
}
.blue{
  background-color: green;
}
.font{
 font-family: "Lucida Console", "Courier New", monospace;
}
</style>


