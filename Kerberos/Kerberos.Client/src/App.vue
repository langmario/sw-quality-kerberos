<template>
  <div id="app">
    <b-navbar>
      <b-navbar-brand href="/">Kerberos Contact Splitter</b-navbar-brand>
      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav>
          <b-nav-item to="/">Startseite</b-nav-item>
          <b-nav-item to="/language">Sprache</b-nav-item>
          <b-nav-item to="/title">Titel</b-nav-item>
          <b-nav-item to="/salutation">Anrede</b-nav-item>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
    <div class="container">
      <router-view/>
    </div>
  </div>
</template>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  /* text-align: center; */
  color: #2c3e50;
}

h1, h2, h3, h4, h5 {
  text-align: center;
}

.text-center {
  text-align: center;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
