// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  core:false
};

export const baseUrl="http://localhost:49270/";
export const mainUrl=baseUrl + "api/";
export const moviesUrl = mainUrl + "movies/";
export const usersUrl = mainUrl+"users/";
export const loginUrl=usersUrl+"check/";

export const imdbTitle=moviesUrl+"imdbTitle/";
export const imdbWord=moviesUrl+"imdbWord/";
export const imdbId=moviesUrl+"imdbId/";

export const favoriteTitle=moviesUrl+"favoriteTitle/";
export const favoriteWord=moviesUrl+"favoriteWord/";
export const favoriteId=moviesUrl+"favoriteId/";







export const apiKey="d474551d";
export const example="http://www.omdbapi.com/?i=tt3896198&apikey=d474551d";

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.