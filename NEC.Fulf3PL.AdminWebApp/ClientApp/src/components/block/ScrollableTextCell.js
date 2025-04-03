import React from 'react';

const ScrollableTextCell = ({ text }) => {

const cellClass = text !== "" && text !== 'undefined' ? 'scrollable-textarea' : 'empty-scrollable-cell'

    return (
        <p>
            <textarea cols="30"
                readOnly
                value={text}
                className={`${cellClass}`}
            />
        </p>
    );
};

export default ScrollableTextCell;
