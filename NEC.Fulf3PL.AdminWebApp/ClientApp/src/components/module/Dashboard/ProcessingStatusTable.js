import React from 'react';

const ProcessingStatusTable = ({ statuses }) => {
    return (
        <div className="max-h-[600px] sm:max-h-[72vh] md:max-h-[72vh] rounded-md border shadow-sm m-5 border-collapse bg-white text-left text-sm text-gray-500">
            <table className="min-w-full divide-y divide-gray-200">
                <thead className="sticky top-0 bg-gray-50">
                    <tr>
                        <th
                            scope="col"
                            className="w-3 min-w-[6vw] px-3 py-3 font-medium text-gray-900 whitespace-nowrap"
                        >
                            Current Processing Status
                        </th>
                    </tr>
                </thead>
                <tbody className="divide-y divide-gray-100 border-t border-gray-100">
                    {
                        statuses !== undefined ?   (statuses?.map((status) => (
                            <tr key={status.id} className="hover:bg-gray-50">
                                <td className="px-3 py-3 pl-6">
                                    <b> {status.displayName} </b> Currently {status.pendingMessageCount} in queue to process
                                </td>
                            </tr>
                        ))) : <tr className="hover:bg-gray-50">
                            <td className="px-3 py-3 pl-6">
                               No records found
                            </td>
                        </tr>
                    }
                </tbody>
            </table>
        </div>
    );
};

export default ProcessingStatusTable;
