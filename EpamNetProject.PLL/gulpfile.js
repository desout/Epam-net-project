"use strict";

var gulp = require("gulp"),
	rimraf = require("rimraf"),
	sass = require("gulp-sass"); 

var paths = {
	webroot: "./wwwroot/"
};
gulp.task("sass", function () {
	return gulp.src('Utils/Styles/index.scss')
		.pipe(sass())
		.pipe(gulp.dest(paths.webroot + '/css'))
});
