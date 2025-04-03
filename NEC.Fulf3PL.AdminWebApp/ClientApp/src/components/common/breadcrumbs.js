"use client";

import React from "react";
import { URL_BASE } from "../../constents/constants";
import { NavLink, useLocation } from 'react-router-dom';

const BreadCrumbs = ({ breadCrumbData = {} }) => {

  // Get the current URL path
  const location = useLocation();
  const pathname = location.pathname;
  let segmentsData = [];

  // let segments = breadCrumbData ?? [];
  if (Object.keys(breadCrumbData).length >= 1) {
    // segments = breadCrumbData;
    Object.values(breadCrumbData).map((urlData) => {
      segmentsData = [
        ...segmentsData,
        { link: urlData?.link, label: urlData?.label },
      ];
    });
  } else {

    // Split the current URL path into segments
    const segments = pathname
      ?.split(URL_BASE)[1]
      ?.split("/")
      .filter((segment) => segment !== "");

    if (segments) {
      segments.map((segment, index) => {
        let linkPath = `${URL_BASE}/${segments?.slice(0, index + 1).join("/")}`;
        segmentsData = [...segmentsData, { link: linkPath, label: segment }];
      });
    }
  }
  // Generate the breadcrumb links
  const breadcrumbs =
    segmentsData &&
    segmentsData.map((segment, index) => {
      let linkPath = segment?.link;
      let linkLabel = segment?.label;
      if (linkLabel) {
        linkLabel =
          linkLabel.replace("-", " ").charAt(0).toUpperCase() +
          linkLabel.slice(1);
      }

      return (
        <>
          <li>
            <span className="mx-2 text-neutral-500 dark:text-neutral-300">
              /
            </span>
          </li>
          <li>
            <NavLink
              href={linkPath}
              className="text-primary transition duration-150 ease-in-out hover:text-primary-600 focus:text-primary-600 active:text-primary-700 
            dark:text-neutral-400 dark:hover:text-neutral-500 dark:focus:text-neutral-500 dark:active:text-neutral-600"
            >
              {linkLabel}
            </NavLink>
          </li>
        </>
      );
    });

  // Add the "Home" link as the first breadcrumb
  breadcrumbs.unshift(
    <li key="home">
      <NavLink
        href={URL_BASE}
        className="text-primary transition duration-150 ease-in-out hover:text-primary-600 focus:text-primary-600 active:text-primary-700 dark:text-neutral-400 
        dark:hover:text-neutral-500 dark:focus:text-neutral-500 dark:active:text-neutral-600"
      >
        Home
      </NavLink>
    </li>
  );

  return (
    <nav className="w-full-rounded-md  px-3 pl-1 pt-2 bg-white">
      <ol className="list-reset flex ">{breadcrumbs}</ol>
    </nav>
  );
};

export default BreadCrumbs;
