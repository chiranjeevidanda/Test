import React from "react";

const NoDataTableTr = ({ noDataMessage = "No data found for the entered criteria." }) => {
    return (
        <><div role="status"
            className="w-full  text-left justify-left">
            <div >
                {noDataMessage && <h2 className="message-box">{noDataMessage}</h2>}
            </div>
        </div>    </>
    );
};

export default NoDataTableTr;