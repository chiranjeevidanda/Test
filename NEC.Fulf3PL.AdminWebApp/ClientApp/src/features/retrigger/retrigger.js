import React from "react";
import RetriggerForm from "../../components/module/retrigger/retriggerForm";

export function Retrigger() {

    return (
        <div className="bg-white">
            <div className="p-2 bg-gray-100 flex-1">
                <h1 className="bg-white rounded p-4 text-2xl font-bold mb-4 pl-5">Retrigger</h1>
            </div>
            <div className="p-2 bg-gray-100 flex-1">
                <h1 className="text-2xl font-bold mb-4 pl-5"> Event</h1>
                <div className="row">
                    <RetriggerForm formType="event" />
                </div>
            </div >

            <div className="p-2 bg-gray-100 flex-1">
                <h1 className="text-2xl font-bold mb-4 pl-5"> Date</h1>
                <div className="row">
                    <RetriggerForm formType="date" />
                </div>
            </div>

            <div className="p-2 bg-gray-100 flex-1">

                <h1 className="text-2xl font-bold mb-4 pl-5"> Modify Inbound payload</h1>
                <div className="row">
                    <RetriggerForm formType="modify" />
                </div>
            </div >
        </div>
    );
}
