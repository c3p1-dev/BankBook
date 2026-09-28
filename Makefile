CXX=		c++
CXXFLAGS=	-O2 -Wall -std=c++20 -Iinclude -MMD -MP

OUT=		bankbook
SRCDIR=		src
OBJDIR=		build

SRCS!=		ls ${SRCDIR}/*.cpp
OBJS=		${SRCS:T:R:@f@${OBJDIR}/${f}.o@}

all: ${OUT}

${OUT}: ${OBJS}
	${CXX} ${CXXFLAGS} -o ${.TARGET} ${OBJS}

.for f in ${SRCS}
${OBJDIR}/${f:T:R}.o: ${f}
	@mkdir -p ${.TARGET:H}
	${CXX} ${CXXFLAGS} -c ${f} -o ${.TARGET}
.endfor

.for o in ${OBJS}
.sinclude "${o:R}.d"
.endfor

clean:
	rm -rf ${OBJDIR} ${OUT}

.PHONY: all clean