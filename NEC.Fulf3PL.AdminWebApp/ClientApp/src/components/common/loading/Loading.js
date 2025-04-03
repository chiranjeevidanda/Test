// import { useEffect, useState } from "react";
// import { Image, Loader } from "semantic-ui-react";

// import style from "./loading.module.css";

// import { BASE_URL } from "../../../newEraConfig";

// export function Loading({
//   loading = true,
//   loadingMsg = "Loading...",
//   loadingProgress = true,
//   autoHide = true,
//   autoHideTimout = 5,
//   imgSrc = `${BASE_URL}/images/loading-cap.png`,
//   imageProps = {},
//   modelName = "59FIFTY"
// }) {
//   const [show, setShow] = useState(loading);


//   useEffect(() => {
//     let timeoutTimer;

//     if (autoHide) {
//       timeoutTimer = setTimeout(() => {
//         setShow(false);
//       }, autoHideTimout * 1000);
//     }

//     return () => {
//       clearTimeout(timeoutTimer);
//     };
//   }, [autoHide, autoHideTimout]);

//   useEffect(() => {
//     let timeoutTimer;
//     if (!loadingProgress) {
//       timeoutTimer = setTimeout(() => {
//         setShow(false);
//       }, 1000);
//     }

//     return () => {
//       clearTimeout(timeoutTimer);
//     };
//   }, [loadingProgress]);

//   if (show) {
//     const containerStyle = {
//       '--product-configuration-display-name': `"${modelName}"`
//     };
//     return (
//       <div className={style["loading-container"]}>
//         <div className={style["loading-content"]}>
//           <div className={style["image-container"]} style={containerStyle}>
//             <Image
//               className={`${style["loading-image"]} spinning`}
//               src={imgSrc}
//               size="large"
//               alt="loading img"
//               {...imageProps}
//             />
//           </div>

//           <div className={style["loading-text-container"]}>
//             <Loader
//               active
//               inline="centered"
//               size="small"
//               className="loader-spinner"
//             />
//             {loadingMsg && (
//               <div className={style["loading-msg"]}>{loadingMsg}</div>
//             )}
//           </div>
//         </div>
//       </div>
//     );
//   }

//   return null;
// }
